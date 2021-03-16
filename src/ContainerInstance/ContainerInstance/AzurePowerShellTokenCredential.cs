using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Azure.Core;

namespace Microsoft.Azure.Commands.ContainerInstance
{
    public class AzurePowerShellTokenCredential : TokenCredential
    {
        private TokenCredential TokenCredential { get; set; }

        public AzurePowerShellTokenCredential(TokenCredential tokenCredential)
        {
            TokenCredential = tokenCredential;
        }

        public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return TokenCredential.GetToken(requestContext, cancellationToken);
        }

        public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return await TokenCredential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(false);
        }
    }
}
