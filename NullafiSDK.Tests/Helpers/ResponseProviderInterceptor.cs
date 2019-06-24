using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WireMock;
using WireMock.ResponseProviders;

namespace Nullafi.Tests.Helpers
{
    public class ResponseProviderInterceptor : IResponseProvider
    {
        public ResponseProviderInterceptor(Func<RequestMessage, IResponseProvider> injectedProvider)
        {
            InjectedProvider = injectedProvider;
        }

        private Func<RequestMessage, IResponseProvider> InjectedProvider;
        public Task<ResponseMessage> ProvideResponseAsync(RequestMessage requestMessage)
        {
            var provider = InjectedProvider(requestMessage);
            return provider.ProvideResponseAsync(requestMessage);
        }
    }
}
