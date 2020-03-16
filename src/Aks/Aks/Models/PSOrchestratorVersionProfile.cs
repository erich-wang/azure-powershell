using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Aks.Models
{
    public class PSOrchestratorVersionProfile
    {
        /// <summary>
        /// Gets or sets orchestrator type.
        /// </summary>
        public string OrchestratorType { get; set; }

        /// <summary>
        /// Gets or sets orchestrator version (major, minor, patch).
        /// </summary>
        public string OrchestratorVersion { get; set; }

        /// <summary>
        /// Gets or sets installed by default if version is not specified.
        /// </summary>
        public bool? DefaultProperty { get; set; }

        /// <summary>
        /// Gets or sets whether Kubernetes version is currently in preview.
        /// </summary>
        public bool? IsPreview { get; set; }

        /// <summary>
        /// Gets or sets the list of available upgrade versions.
        /// </summary>
        public IList<PSOrchestratorProfile> Upgrades { get; set; }
    }
}
