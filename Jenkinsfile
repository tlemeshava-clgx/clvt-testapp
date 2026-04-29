@Library('CoreLogicDevopsUtils@APPNET-7990')
@Library('CoreLogicPipelineUtils')
@Library('CoreLogicOrgPipelineConfig')

import com.corelogic.devops.utils.*
import com.corelogic.task.*
import com.corelogic.Config
import groovy.util.XmlSlurper

slaveTemplates = new PodTemplates()

def containerName = "net10-${UUID.randomUUID()}".take(35) - ~'-$'
def label = "clvt-us-cctest-ltsc2022-${containerName}"
def dImage = ""

slaveTemplates.getPodTemplate(label, containerName, dImage) {  
    node(label){
        container(containerName) {
            def pipeline = pipelineManager.config()
            def branchName = env.BRANCH_NAME
            def mercuryAppName = 'clvt-cctest'

            if (branchName == 'develop') {
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

            def dotnetCoreBuilder = mercury.createDotnetCoreBuilder(branchName, mercuryAppName, true)
            def iisDotnetDeployable = mercury.createIISDotnetDeployable(flowName, mercuryAppName)
            iisDotnetDeployable.builder = dotnetCoreBuilder
            pipeline.execute(iisDotnetDeployable)
	    }
    }
}