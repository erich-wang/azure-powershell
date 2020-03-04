// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Commands.Aks.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Profile of the managed cluster load balancer
    /// </summary>
    public partial class PSManagedClusterLoadBalancerProfile
    {
        /// <summary>
        /// Gets or sets desired managed outbound IPs for the cluster load
        /// balancer.
        /// </summary>
        public PSManagedClusterLoadBalancerProfileManagedOutboundIPs ManagedOutboundIPs { get; set; }

        /// <summary>
        /// Gets or sets desired outbound IP Prefix resources for the cluster
        /// load balancer.
        /// </summary>
        public PSManagedClusterLoadBalancerProfileOutboundIPPrefixes OutboundIPPrefixes { get; set; }

        /// <summary>
        /// Gets or sets desired outbound IP resources for the cluster load
        /// balancer.
        /// </summary>
        public PSManagedClusterLoadBalancerProfileOutboundIPs OutboundIPs { get; set; }

        /// <summary>
        /// Gets or sets the effective outbound IP resources of the cluster
        /// load balancer.
        /// </summary>
        public IList<PSResourceReference> EffectiveOutboundIPs { get; set; }
    }
}
