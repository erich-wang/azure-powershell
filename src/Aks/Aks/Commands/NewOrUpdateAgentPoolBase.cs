using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Azure.Commands.Aks.Properties;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.ContainerService;
using Microsoft.Azure.Management.ContainerService.Models;

namespace Microsoft.Azure.Commands.Aks
{
    public class NewOrUpdateAgentPoolBase : KubeCmdletBase
    {
        [Parameter(Mandatory = true, ParameterSetName = Constants.DefaultParameterSet, HelpMessage = "The name of the resource group.")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = Constants.DefaultParameterSet, HelpMessage = "The name of the managed cluster resource.")]
        [ValidateNotNullOrEmpty]
        public string ClusterName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = Constants.DefaultParameterSet, HelpMessage = "The name of the agent pool.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The version of Kubernetes to use for creating the cluster.")]
        public string KubernetesVersion { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Minimum number of nodes for auto-scaling.")]
        public int MinCount { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Maximum number of nodes for auto-scaling")]
        public int MaxCount { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Whether to enable auto-scaler")]
        public SwitchParameter EnableAutoScaling { get; set; }

        protected AgentPool GetAgentPoolObject()
        {
            AgentPool pool = null;
            try
            {
                pool = Client.AgentPools.Get(ResourceGroupName, ClusterName, Name);
                WriteVerbose(string.Format(Resources.AgentPoolExists, pool != null));
            }
            catch (Exception)
            {
                WriteVerbose(Resources.AgentPoolDoesNotExist);
            }
            return pool;
        }

    }
}
