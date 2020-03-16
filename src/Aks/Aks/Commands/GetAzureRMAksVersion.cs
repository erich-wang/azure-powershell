using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Aks.Models;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.ContainerService;

namespace Microsoft.Azure.Commands.Aks.Commands
{
    [Cmdlet("Get", AzureRMConstants.AzureRMPrefix + "AksVersion")]
    [OutputType(typeof(PSOrchestratorVersionProfile))]
    public class GetAzureRMAksVersion : KubeCmdletBase
    {
        [Parameter(Mandatory = true,
            HelpMessage = "Azure location for the cluster.")]
        [LocationCompleter("Microsoft.ContainerService/managedClusters")]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            RunCmdLet(() =>
            {
                var profileList = Client.ContainerServices.ListOrchestrators(Location, "managedClusters");

                WriteObject(profileList.Orchestrators.Select(
                    item => PSMapper.Instance.Map<PSOrchestratorVersionProfile>(item)), true);
            });
        }
    }
}
