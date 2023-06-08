pipeline {
  agent {
    label "local"
  }

  options {
    skipDefaultCheckout(true)
    timeout(time: 20, unit: 'MINUTES')
    buildDiscarder(logRotator(numToKeepStr: '10', artifactNumToKeepStr: '10'))
    timestamps()
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
        dotnetBuild(project: "AlgoLib_CSharp.sln", configuration: "Release", nologo: true)
      }
    }

    stage("Unit tests") {
      environment {
        NUNIT_RESULTS_DIR = "nunitResults"
        NUNIT_RESULTS_PATH = "${env.WORKSPACE}/${env.NUNIT_RESULTS_DIR}"
      }

      steps {
        echo "#INFO: Running unit tests"
        dotnetTest(
          project: "AlgoLib_CSharp.sln",
          configuration: "Release",
          noBuild: true,
          nologo: true,
          runSettings: ['NUnit.TestOutputXml': "${env.NUNIT_RESULTS_PATH}"]
        )
      }
      
      post {
        always {
          nunit(
            testResultsPattern: "${env.NUNIT_RESULTS_DIR}/*.xml",
            failedTestsFailBuild: true,
            healthScaleFactor: 1.0
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
