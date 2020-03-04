using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Azure.Commands.Aks.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.ContainerService;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Microsoft.Azure.Commands.Aks.Commands
{

    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + Constants.AgentPool, DefaultParameterSetName = Constants.IdParameterSet)]
    [OutputType(typeof(PSAgentPool))]
    public class GetAzureRmAksAgentPool : KubeCmdletBase
    {
        [Parameter(Mandatory = true,
            ParameterSetName = Constants.IdParameterSet,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Id of an agent pool in managed Kubernetes cluster")]
        [ValidateNotNullOrEmpty]
        [Alias("ResourceId")]
        public string Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = Constants.ParentNameParameterSet, HelpMessage = "The name of the resource group.")]
        [Parameter(Mandatory = true, ParameterSetName = Constants.NameParameterSet, HelpMessage = "The name of the resource group.")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = Constants.ParentNameParameterSet, HelpMessage = "The name of the managed cluster resource.")]
        [Parameter(Mandatory = true, ParameterSetName = Constants.NameParameterSet, HelpMessage = "The name of the managed cluster resource.")]
        [ValidateNotNullOrEmpty]
        public string ClusterName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = Constants.ParentNameParameterSet, HelpMessage = "The name of the agent pool.")]
        [Parameter(Mandatory = true, ParameterSetName = Constants.NameParameterSet, HelpMessage = "The name of the agent pool.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            RunCmdLet(() =>
            {
                switch(ParameterSetName)
                {
                    case Constants.IdParameterSet:
                        var resource = new ResourceIdentifier(Id);
                        var parentName = Utilities.GetParentResourceName(resource.ParentResource);
                        var pool = Client.AgentPools.Get(resource.ResourceGroupName, parentName, resource.ResourceName);
                        WriteObject(PSMapper.Instance.Map<PSAgentPool>(pool));
                        break;
                    case Constants.ParentNameParameterSet:
                        var pools = Client.AgentPools.List(ResourceGroupName, ClusterName);
                        WriteObject(pools.Select(PSMapper.Instance.Map<PSAgentPool>), true);
                        break;
                    case Constants.NameParameterSet:
                        pool = Client.AgentPools.Get(ResourceGroupName, ClusterName, Name);
                        WriteObject(PSMapper.Instance.Map<PSAgentPool>(pool));
                        break;
                }
            });
        }
    }
}
