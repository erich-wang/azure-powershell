using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;

namespace Microsoft.Azure.Commands.Common.Authentication.Authentication
{
    public class AzureCoreTokenCredential : TokenCredential
    {
        private IAzureContext context = null;
        private string endpoint = null;

        public AzureCoreTokenCredential(IAzureContext context, string endpoint)
        {
            this.context = context;
            this.endpoint = endpoint;
        }

        public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            var token = await Task.Run(() => GetToken(requestContext, cancellationToken));
            return token;
        }

        public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            return AzureSession.Instance.AuthenticationFactory.GetAzureCoreAccessToken(context, requestContext, endpoint);
        }
    }
}
