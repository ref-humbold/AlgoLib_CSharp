pipeline {
  agent {
    label "windows"
  }

  options {
    skipDefaultCheckout(true)
    timeout(time: 20, unit: 'MINUTES')
    buildDiscarder(logRotator(numToKeepStr: '10', artifactNumToKeepStr: '10'))
    ansiColor('xterm')
  }

  stages {
    stage("Preparation") {
      steps {
        script {
          def scmEnv = checkout(scm)
          currentBuild.displayName = "${env.BUILD_NUMBER} ${scmEnv.GIT_COMMIT.take(8)}"
        }
      }
    }

    stage("Build") {
      steps {
        echo "#INFO: Building project"
        powershell "dotnet build AlgoLib_CSharp.sln -c Release --nologo"
      }
    }

    stage("Unit tests") {
      environment {
        NUNIT_RESULTS_DIR = "nunitResults"
        NUNIT_RESULTS_PATH = "${env.WORKSPACE}/${env.NUNIT_RESULTS_DIR}"
      }

      steps {
        echo "#INFO: Running unit tests"
        powershell "dotnet test AlgoLib_CSharp.sln -c Release --no-build --nologo -- NUnit.TestOutputXml=${env.NUNIT_RESULTS_PATH}"
      }
      
      post {
        always {
          nunit(
            testResultsPattern: "${env.NUNIT_RESULTS_DIR}/*.xml",
            failedTestsFailBuild: true
          )
        }
      }
    }
  }

  post {
    always {
      chuckNorris()
    }

    cleanup {
      cleanWs()
    }
  }
}
