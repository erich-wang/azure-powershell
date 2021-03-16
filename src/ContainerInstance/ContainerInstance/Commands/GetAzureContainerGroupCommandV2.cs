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
using System.Management.Automation;
using AutoMapper;

using Azure.ResourceManager.ContainerInstance;
using Azure.ResourceManager.ContainerInstance.Models;

using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.ResourceManager.Common;
//using Microsoft.Azure.Commands.ContainerInstance.Models;
//using Microsoft.Azure.Management.ContainerInstance;
//using Microsoft.Azure.Management.ContainerInstance.Models;
//using Microsoft.Azure.Management.Internal.Resources;
//using Microsoft.Rest.Azure;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.PowerShell.Authenticators;

namespace Microsoft.Azure.Commands.ContainerInstance
{
    /// <summary>
    /// Get-AzContainerGroup.
    /// </summary>
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "ContainerGroupV2", DefaultParameterSetName = ListContainerGroupParamSet)]
    [OutputType(typeof(ContainerGroup))]
    public class GetAzureContainerGroupCommandV2 : AzureRMCmdlet
    {
        protected const string GetContainerGroupInResourceGroupParamSet = "GetContainerGroupInResourceGroupParamSet";
        protected const string GetContainerGroupByResourceIdParamSet = "GetContainerGroupByResourceIdParamSet";
        protected const string ListContainerGroupParamSet = "ListContainerGroupParamSet";

        [Parameter(
            Position = 0,
            Mandatory = true,
            ParameterSetName = GetContainerGroupInResourceGroupParamSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The resource Group Name.")]
        [Parameter(
            Position = 0,
            Mandatory = false,
            ParameterSetName = ListContainerGroupParamSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The resource Group Name.")]
        [ResourceGroupCompleter()]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = GetContainerGroupInResourceGroupParamSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The container group Name.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = GetContainerGroupByResourceIdParamSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The resource id.")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        public override void ExecuteCmdlet()
        {
            //if (!string.IsNullOrEmpty(this.ResourceGroupName) && !string.IsNullOrEmpty(this.Name))
            {
                var token =  AzureSession.Instance.AuthenticationFactory.Authenticate(
                                DefaultContext.Account,
                                DefaultContext.Environment,
                                DefaultContext.Tenant.Id,
                                null,
                                ShowDialog.Never,
                                null);
                //TODO: Should not reference project Authenticators directly
                var tokenCredential = (token as MsalAccessToken).TokenCredential;
                var client = new ContainerInstanceManagementClient(this.DefaultContext.Subscription.Id, tokenCredential);
                var containerGroups = new List<ContainerGroup>();
                foreach (var group in client.ContainerGroups.List())
                {
                    containerGroups.Add(group);
                }
                this.WriteObject(containerGroups, true);
            }
        }
    }
}
