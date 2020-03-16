namespace Microsoft.Azure.Commands.Aks.Models
{
    public class PSOrchestratorProfile
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
        /// Gets or sets whether Kubernetes version is currently in preview.
        /// </summary>
        public bool? IsPreview { get; set; }
    }
}