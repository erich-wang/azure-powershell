// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Management.ContainerService;
using Microsoft.Azure.Management.ContainerService.Models;
using Microsoft.Azure.Commands.Aks.Models;
using Microsoft.Azure.Commands.Aks.Properties;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Aks
{
    [Cmdlet("Set", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "Aks", DefaultParameterSetName = DefaultParamSet, SupportsShouldProcess = true)]
    [OutputType(typeof(PSKubernetesCluster))]
    public class SetAzureRmAks : SetKubeBase
    {
        private const string IdParameterSet = "IdParameterSet";
        private const string InputObjectParameterSet = "InputObjectParameterSet";

        [Parameter(Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            HelpMessage = "A PSKubernetesCluster object, normally passed through the pipeline.")]
        [ValidateNotNullOrEmpty]
        public PSKubernetesCluster InputObject { get; set; }

        /// <summary>
        /// Cluster name
        /// </summary>
        [Parameter(Mandatory = true,
            ParameterSetName = IdParameterSet,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Id of a managed Kubernetes cluster")]
        [ValidateNotNullOrEmpty]
        [Alias("ResourceId")]
        public string Id { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            ManagedCluster cluster = null;
            switch (ParameterSetName)
            {
                case IdParameterSet:
                {
                    var resource = new ResourceIdentifier(Id);
                    ResourceGroupName = resource.ResourceGroupName;
                    Name = resource.ResourceName;
                    break;
                }
                case InputObjectParameterSet:
                {
                    WriteVerbose(Resources.UsingClusterFromPipeline);
                    cluster = PSMapper.Instance.Map<ManagedCluster>(InputObject);
                    var resource = new ResourceIdentifier(cluster.Id);
                    ResourceGroupName = resource.ResourceGroupName;
                    Name = resource.ResourceName;
                    break;
                }
            }

            var msg = $"{Name} in {ResourceGroupName}";

            if (ShouldProcess(msg, Resources.UpdateOrCreateAManagedKubernetesCluster))
            {
                RunCmdLet(() =>
                {
                    if (Exists())
                    {
                        if (cluster == null)
                        {
                            cluster = Client.ManagedClusters.Get(ResourceGroupName, Name);
                        }

                        if (MyInvocation.BoundParameters.ContainsKey("Location"))
                        {
                            throw new CmdletInvocationException(Resources.LocationCannotBeUpdateForExistingCluster);
                        }

                        if (MyInvocation.BoundParameters.ContainsKey("DnsNamePrefix"))
                        {
                            WriteVerbose(Resources.UpdatingDnsNamePrefix);
                            cluster.DnsPrefix = DnsNamePrefix;
                        }

                        if (MyInvocation.BoundParameters.ContainsKey("SshKeyValue"))
                        {
                            WriteVerbose(Resources.UpdatingSshKeyValue);
                            cluster.LinuxProfile.Ssh.PublicKeys = new List<ContainerServiceSshPublicKey>
                            {
                                new ContainerServiceSshPublicKey(GetSshKey(SshKeyValue))
                            };
                        }

                        if (ParameterSetName == SpParamSet)
                        {
                            WriteVerbose(Resources.UpdatingServicePrincipal);
                            var acsServicePrincipal = EnsureServicePrincipal(ClientIdAndSecret.UserName, ClientIdAndSecret.Password.ToString());

                            var spProfile = new ManagedClusterServicePrincipalProfile(
                                acsServicePrincipal.SpId,
                                acsServicePrincipal.ClientSecret);
                            cluster.ServicePrincipalProfile = spProfile;
                        }

                        if (MyInvocation.BoundParameters.ContainsKey("AdminUserName"))
                        {
                            WriteVerbose(Resources.UpdatingAdminUsername);
                            cluster.LinuxProfile.AdminUsername = AdminUserName;
                        }

                        if (NeedUpdateNodeAgentPool())
                        {
                            ManagedClusterAgentPoolProfile defaultAgentPoolProfile;

                            string nodePoolName = "default";
                            if (this.IsParameterBound(c => c.NodeName))
                            {
                                nodePoolName = NodeName;
                            }

                            if (cluster.AgentPoolProfiles.Any(x => x.Name == nodePoolName))
                            {
                                defaultAgentPoolProfile = cluster.AgentPoolProfiles.First(x => x.Name == nodePoolName);
                            }
                            else
                            {
                                throw new PSArgumentException(Resources.SpecifiedAgentPoolDoesNotExist);
                            }

                            if (this.IsParameterBound(c => c.NodeMinCount))
                            {
                                defaultAgentPoolProfile.MinCount = NodeMinCount;
                            }
                            if (this.IsParameterBound(c => c.NodeMaxCount))
                            {
                                defaultAgentPoolProfile.MaxCount = NodeMaxCount;
                            }
                            if (NodeEnableAutoScaling.IsPresent)
                            {
                                defaultAgentPoolProfile.EnableAutoScaling = NodeEnableAutoScaling.ToBool();
                            }
                            if (MyInvocation.BoundParameters.ContainsKey("NodeVmSize"))
                            {
                                WriteVerbose(Resources.UpdatingNodeVmSize);
                                defaultAgentPoolProfile.VmSize = NodeVmSize;
                            }

                            if (MyInvocation.BoundParameters.ContainsKey("NodeCount"))
                            {
                                WriteVerbose(Resources.UpdatingNodeCount);
                                defaultAgentPoolProfile.Count = NodeCount;
                            }

                            if (MyInvocation.BoundParameters.ContainsKey("NodeOsDiskSize"))
                            {
                                WriteVerbose(Resources.UpdatingNodeOsDiskSize);
                                defaultAgentPoolProfile.OsDiskSizeGB = NodeOsDiskSize;
                            }
                        }

                        if (MyInvocation.BoundParameters.ContainsKey("KubernetesVersion"))
                        {
                            WriteVerbose(Resources.UpdatingKubernetesVersion);
                            cluster.KubernetesVersion = KubernetesVersion;
                        }

                        if (MyInvocation.BoundParameters.ContainsKey("Tag"))
                        {
                            WriteVerbose(Resources.UpdatingTags);
                            cluster.Tags = TagsConversionHelper.CreateTagDictionary(Tag, true);
                        }

                        //To avoid server error: for agentPoolProfiles.availabilityZones, server will expect
                        //$null instead of empty collection, otherwise it will throw error.
                        foreach(var profile in cluster.AgentPoolProfiles)
                        {
                            if(profile.AvailabilityZones?.Count == 0)
                            {
                                profile.AvailabilityZones = null;
                            }
                        }

                        WriteVerbose(Resources.UpdatingYourManagedKubernetesCluster);
                    }
                    else
                    {
                        WriteVerbose(Resources.PreparingForDeploymentOfYourNewManagedKubernetesCluster);
                        cluster = BuildNewCluster();
                    }

                    var kubeCluster = Client.ManagedClusters.CreateOrUpdate(ResourceGroupName, Name, cluster);
                    WriteObject(PSMapper.Instance.Map<PSKubernetesCluster>(kubeCluster));
                });
            }
        }

        private bool NeedUpdateNodeAgentPool()
        {
            return this.IsParameterBound(c => c.NodeCount) || this.IsParameterBound(c => c.NodeOsDiskSize) ||
                this.IsParameterBound(c => c.NodeVmSize) || this.IsParameterBound(c => c.NodeEnableAutoScaling) ||
                this.IsParameterBound(c => c.NodeMinCount) || this.IsParameterBound(c => c.NodeMaxCount);
        }
    }
}
