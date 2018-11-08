using Microsoft.Rest;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CognitivePlayground.Server
{
    class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        string subscriptionKey = "57decc2cbcad499590b355c89db41805"; //Insert your Text Anaytics subscription key

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
