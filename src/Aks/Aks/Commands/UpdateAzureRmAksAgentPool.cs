using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Azure.Commands.Aks.Models;
using Microsoft.Azure.Commands.Aks.Properties;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.ContainerService;
using Microsoft.Azure.Management.ContainerService.Models;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Aks.Commands
{
    [Cmdlet("Update", AzureRMConstants.AzureRMPrefix + Constants.AgentPool, DefaultParameterSetName = Constants.GroupNameParameterSet, SupportsShouldProcess = true)]
    [OutputType(typeof(PSAgentPool))]
    public class UpdateAzureRmAksAgentPool : NewOrUpdateAgentPoolBase
    {
        [Parameter(Mandatory = true,
            ParameterSetName = Constants.InputObjectParameterSet,
            ValueFromPipeline = true,
            HelpMessage = "A PSAgentPool object, normally passed through the pipeline.")]
        [ValidateNotNullOrEmpty]
        public PSAgentPool InputObject { get; set; }

        [Parameter(Mandatory = true,
            ParameterSetName = Constants.IdParameterSet,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Id of an agent pool in managed Kubernetes cluster")]
        [ValidateNotNullOrEmpty]
        [Alias("ResourceId")]
        public string Id { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Update agent pool without prompt")]
        public SwitchParameter Force { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            AgentPool pool = null;
            ResourceIdentifier resource = null;
            switch(ParameterSetName)
            {
                case Constants.IdParameterSet:
                    resource = new ResourceIdentifier(Id);
                    ResourceGroupName = resource.ResourceGroupName;
                    ClusterName = Utilities.GetParentResourceName(resource.ParentResource);
                    Name = resource.ResourceName;
                    break;
                case Constants.InputObjectParameterSet:
                    WriteVerbose(Resources.UsingAgentPoolFromPipeline);
                    pool = PSMapper.Instance.Map<AgentPool>(InputObject);
                    resource = new ResourceIdentifier(pool.Id);
                    ResourceGroupName = resource.ResourceGroupName;
                    ClusterName = Utilities.GetParentResourceName(resource.ParentResource);
                    Name = resource.ResourceName;
                    break;
            }

            var msg = $"{Name} for {ClusterName} in {ResourceGroupName}";
            if(ShouldProcess(msg, Resources.UpdateAgentPool))
            {
                RunCmdLet(() =>
                {
                    {
                        //Put agentPool in the block to avoid referencing it incorrectly.
                        var agentPool = GetAgentPoolObject();
                        if (agentPool == null)
                        {
                            WriteErrorWithTimestamp(Resources.AgentPoolDoesNotExist);
                            return;
                        }

                        if (pool == null)
                        {
                            pool = agentPool;
                        }
                    }

                    if(this.IsParameterBound(c => c.KubernetesVersion))
                    {
                        pool.OrchestratorVersion = KubernetesVersion;
                    }
                    if (this.IsParameterBound(c => c.MinCount))
                    {
                        pool.MinCount = MinCount;
                    }
                    if (this.IsParameterBound(c => c.MaxCount))
                    {
                        pool.MaxCount = MaxCount;
                    }
                    if (EnableAutoScaling.IsPresent)
                    {
                        pool.EnableAutoScaling = EnableAutoScaling.ToBool();
                    }

                    var updatedPool = Client.AgentPools.CreateOrUpdate(ResourceGroupName, ClusterName, Name, pool);
                    WriteObject(PSMapper.Instance.Map<PSAgentPool>(updatedPool));
                });
            }
        }
    }
}
