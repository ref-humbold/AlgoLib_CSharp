pipeline {
  agent {
    label "windows"
  }

  options {
    skipDefaultCheckout true
    timeout(time: 15, unit: 'MINUTES')
    buildDiscarder logRotator(numToKeepStr: '10', artifactNumToKeepStr: '10')
  }

  environment {
    APP_VERSION = getAppVersion(0, 0)
  }

  stages {
    stage("Preparation") {
      steps {
        script {
          def scmEnv = checkout scm
          currentBuild.displayName = "${env.APP_VERSION}.${env.BUILD_NUMBER}+${scmEnv.GIT_COMMIT.take(8)}"
          echo "#INFO: Version is ${env.APP_VERSION}"
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
    cleanup {
      cleanWs()
    }
  }
}

def getAppVersion(int major, int minor) {
  def date = java.time.LocalDate.now().format(java.time.format.DateTimeFormatter.ofPattern("YYMMdd"))
  return "${major}.${minor}.${date}"
}
