using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Aks.Models
{
    public class PSContainerServiceNetworkProfile
    {
        /// <summary>
        /// Gets or sets network plugin used for building Kubernetes network.
        /// Possible values include: 'azure', 'kubenet'
        /// </summary>
        public string NetworkPlugin { get; set; }

        /// <summary>
        /// Gets or sets network policy used for building Kubernetes network.
        /// Possible values include: 'calico', 'azure'
        /// </summary>
        public string NetworkPolicy { get; set; }

        /// <summary>
        /// Gets or sets a CIDR notation IP range from which to assign pod IPs
        /// when kubenet is used.
        /// </summary>
        public string PodCidr { get; set; }

        /// <summary>
        /// Gets or sets a CIDR notation IP range from which to assign service
        /// cluster IPs. It must not overlap with any Subnet IP ranges.
        /// </summary>
        public string ServiceCidr { get; set; }

        /// <summary>
        /// Gets or sets an IP address assigned to the Kubernetes DNS service.
        /// It must be within the Kubernetes service address range specified in
        /// serviceCidr.
        /// </summary>
        public string DnsServiceIP { get; set; }

        /// <summary>
        /// Gets or sets a CIDR notation IP range assigned to the Docker bridge
        /// network. It must not overlap with any Subnet IP ranges or the
        /// Kubernetes service address range.
        /// </summary>
        public string DockerBridgeCidr { get; set; }

        /// <summary>
        /// Gets or sets the load balancer sku for the managed cluster.
        /// Possible values include: 'standard', 'basic'
        /// </summary>
        public string LoadBalancerSku { get; set; }

        /// <summary>
        /// Gets or sets profile of the cluster load balancer.
        /// </summary>
        public PSManagedClusterLoadBalancerProfile LoadBalancerProfile { get; set; }

    }
}
