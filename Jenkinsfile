@Library('CoreLogicDevopsUtils@APPNET-7990')
@Library('CoreLogicPipelineUtils')
@Library('CoreLogicOrgPipelineConfig')

import com.corelogic.devops.utils.*
import com.corelogic.task.*
import com.corelogic.Config
import groovy.util.XmlSlurper

slaveTemplates = new PodTemplates()

def containerName = "dotnet-build-${UUID.randomUUID()}".take(35) - ~'-$'
def label = "windows-container-${containerName}"
def dImage = "us-docker.pkg.dev/clgx-artregistry-mgt-prd-3b27/edg-legacy-us-diabloplus-docker-local/windows-dotnet-thor:latest"
def location = "us-central1"

slaveTemplates.getPodTemplate(label, containerName, dImage) {  
    node(label){
        container(containerName) {
            def pipeline = pipelineManager.config()
            def branchName = env.BRANCH_NAME

            if (branchName == 'feature/develop') {
                flowName = 'developflowGCP'
                veraCodeSwitch = false
                deploySwitch = true
                recycleSwitch = false
                buildModeConfig = 'Debug'
            }
            else if (branchName == 'main') {
                flowName = 'releaseflowGCP'
                veraCodeSwitch = false
                deploySwitch = true
                recycleSwitch = false
                buildModeConfig = 'Release'
            }       
            else if (branchName.startsWith('hotfix/')) {
                flowName = 'hotfixflowGCP'
                veraCodeSwitch = false
                deploySwitch = true
                recycleSwitch = false
                buildModeConfig = 'Release'
            }

def buildType = new com.corelogic.pipeline.DeployableType(
            appName: 'cmas-xml',
            ecosystemName: 'diabloplus',
            deployToIIS: false,
            pipelineFlowName: 'CD',
            enableVeracodeForLibs: true,
            builder: new com.corelogic.builder.DotnetBuilder(
            postBuildStage:['app-process-filestozip':{processFiles()}],
            versiontobuild: '1.1.0',
            useMSBuild: true,
            skipStashAssets: true,
	    buildCommand: "msbuild -nologo -maxCpuCount -verbosity:diag clgx-demo-app.sln /t:ReBuild /property:Configuration=Debug",
	    buildFilePath:['clgx-demo-app.sln'],
            projectDirectory: '.',
            nugetRestore: true,
            solutionDirectory: "."
        )
            )
            pipeline.execute(buildType)
	    }
    }
}