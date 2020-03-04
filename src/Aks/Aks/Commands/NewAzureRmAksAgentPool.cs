using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Azure.Commands.Aks.Models;
using Microsoft.Azure.Commands.Aks.Properties;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.ContainerService;
using Microsoft.Azure.Management.ContainerService.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Aks
{
    [Cmdlet("New", AzureRMConstants.AzureRMPrefix + Constants.AgentPool, SupportsShouldProcess = true)]
    [OutputType(typeof(PSAgentPool))]
    public class NewAzureRmAksAgentPool : NewOrUpdateAgentPoolBase
    {
        [Parameter(Mandatory = false, HelpMessage = "The default number of nodes for the node pools.")]
        public int Count { get; set; } = 3;

        [Parameter(Mandatory = false, HelpMessage = "The default number of nodes for the node pools.")]
        public int OsDiskSize { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The size of the Virtual Machine.")]
        public string VmSize { get; set; } = "Standard_D2_v2";

        [Parameter(Mandatory = false, HelpMessage = "VNet SubnetID specifies the VNet's subnet identifier.")]
        public string VnetSubnetID { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Maximum number of pods that can run on node.")]
        public int MaxPodCount { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "OsType to be used to specify os type. Choose from Linux and Windows. Default to Linux.")]
        [PSArgumentCompleter("Linux", "Windows")]
        public string OsType { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Whether to enable public IP for nodes")]
        public SwitchParameter EnableNodePublicIp { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "ScaleSetPriority to be used to specify virtual machine scale set priority. Default to regular.")]
        [PSArgumentCompleter("Low", "Regular")]
        public string ScaleSetPriority { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "ScaleSetEvictionPolicy to be used to specify eviction policy for low priority virtual machine scale set. Default to Delete.")]
        [PSArgumentCompleter("Delete", "Deallocate")]
        public string ScaleSetEvictionPolicy { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "AgentPoolType represents types of an agent pool. Possible values include: 'VirtualMachineScaleSets', 'AvailabilitySet'")]
        [PSArgumentCompleter("AvailabilitySet", "VirtualMachineScaleSets")]
        public string VmSetType { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Create agent pool even if it already exists")]
        public SwitchParameter Force { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            Action action = () =>
            {
                var agentPool = GetAgentPool();
                var pool = Client.AgentPools.CreateOrUpdate(ResourceGroupName, ClusterName, Name, agentPool);
                var psPool = PSMapper.Instance.Map<PSAgentPool>(pool);
                WriteObject(psPool);
            };

            var msg = $"{Name} for {ClusterName} in {ResourceGroupName}";

            if(GetAgentPoolObject() != null)
            {
                WriteVerbose(Resources.AgentPoolAlreadyExistsConfirmAction);
                ConfirmAction(
                    Force,
                    Resources.DoYouWantToCreateClusterAgentPool,
                    Resources.CreatingClusterAgentPool,
                    msg,
                    action
                    );
            }
            else
            {
                WriteVerbose(Resources.AgentPoolIsNew);
                if(ShouldProcess(msg, Resources.CreatingClusterAgentPool))
                {
                    RunCmdLet(action);
                }
            }
        }

        private AgentPool GetAgentPool()
        {
            var agentPool = new AgentPool(
                name: Name,
                count: Count,
                vmSize: VmSize,
                osDiskSizeGB: OsDiskSize,
                type: VmSetType ?? "AvailabilitySet",
                vnetSubnetID: VnetSubnetID);

            if (this.IsParameterBound(c => c.KubernetesVersion))
            {
                agentPool.OrchestratorVersion = KubernetesVersion;
            }

            if (this.IsParameterBound(c => c.OsType))
            {
                agentPool.OsType = OsType;
            }
            if (this.IsParameterBound(c => c.MaxPodCount))
            {
                agentPool.MaxPods = MaxPodCount;
            }
            if (this.IsParameterBound(c => c.MinCount))
            {
                agentPool.MinCount = MinCount;
            }
            if (this.IsParameterBound(c => c.MaxCount))
            {
                agentPool.MaxCount = MaxCount;
            }
            if (EnableAutoScaling.IsPresent)
            {
                agentPool.EnableAutoScaling = EnableAutoScaling.ToBool();
            }
            if(EnableNodePublicIp.IsPresent)
            {
                agentPool.EnableNodePublicIP = EnableNodePublicIp.ToBool();
            }
            if(this.IsParameterBound(c => c.ScaleSetEvictionPolicy))
            {
                agentPool.ScaleSetEvictionPolicy = ScaleSetEvictionPolicy;
            }
            if(this.IsParameterBound(c => c.ScaleSetPriority))
            {
                agentPool.ScaleSetPriority = ScaleSetPriority;
            }

            return agentPool;
        }

    }
}
