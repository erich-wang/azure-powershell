using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ResourceManager.Common;

namespace Microsoft.Azure.Commands.KeyVault
{
    public class KeyClientAdapter
    {
        private KeyClient keyClient;
        private SecretClient secretClient;

        public KeyClientAdapter(Uri uri, IAzureContext azureContext, AzureRMCmdlet cmdlet)
        {
            var clientOptions = new KeyClientOptions();
            clientOptions.Diagnostics.IsTelemetryEnabled = false;
            clientOptions.AddPolicy(new TelemetryHttpPipelinePolicy(cmdlet), global::Azure.Core.HttpPipelinePosition.PerRetry);
            var tokenCredential = AzureSession.Instance.AuthenticationFactory.GetAzureCoreTokenCredential(azureContext, AzureEnvironment.Endpoint.AzureKeyVaultServiceEndpointResourceId);
            keyClient = new KeyClient(uri, tokenCredential, clientOptions);

            var secretOptions = new SecretClientOptions();
            secretOptions.Diagnostics.IsTelemetryEnabled = false;
            secretOptions.AddPolicy(new TelemetryHttpPipelinePolicy(cmdlet), global::Azure.Core.HttpPipelinePosition.PerRetry);
            secretClient = new SecretClient(uri, tokenCredential, secretOptions);
        }

        public KeyVaultSecret GetSecret(string name, string version)
        {
            return secretClient.GetSecret(name, version);
        }

        public KeyVaultSecret SetSecret(string name, SecureString secureString)
        {
            return secretClient.SetSecret(name, secureString.ToString());
        }

        public DeletedSecret StartDeleteSecret(string name)
        {
            return secretClient.StartDeleteSecret(name).WaitForCompletionAsync().GetAwaiter().GetResult();
        }

        public bool DeleteKey(string keyName)
        {
            keyClient.StartDeleteKey(keyName).WaitForCompletionAsync().GetAwaiter().GetResult();
            return true;
        }
    }

    public class AzureCoreAuthenticationHttpPipelinePolicy : HttpPipelinePolicy
    {
        public override void Process(HttpMessage message,ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            throw new NotImplementedException();
        }

        public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {

            await ProcessNextAsync(message, pipeline);
        }
    }

    //TODO: need to split into different policies
    public class TelemetryHttpPipelinePolicy : HttpPipelineSynchronousPolicy
    {
        AzureRMCmdlet cmdlet;
        public TelemetryHttpPipelinePolicy(AzureRMCmdlet cmdlet)
        {
            this.cmdlet = cmdlet;
        }

        public override void OnSendingRequest(HttpMessage message)
        {
            base.OnSendingRequest(message);
            message.Request.Headers.SetValue("CommandName", this.cmdlet.MyInvocation.InvocationName);
            message.Request.Headers.SetValue("User-Agent", "AzurePowerShell/1.0");
            cmdlet.DebugMessages.Enqueue("=========================Reqeust===========================");
            cmdlet.DebugMessages.Enqueue(message.Request.Uri.ToString());
            cmdlet.WriteDebug("");
        }

        public override void OnReceivedResponse(HttpMessage message)
        {
            base.OnReceivedResponse(message);
            cmdlet.DebugMessages.Enqueue("=========================Response===========================");
            cmdlet.DebugMessages.Enqueue(message.Response.ToString());
            cmdlet.WriteDebug("");
        }
    }
}
