using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Chaos
{
    public class ChaosDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (ChaosConfig.EnableChaos)
            {
                if (ChaosConfig.AlwaysFault)
                    return Task.FromResult(new HttpResponseMessage(ChaosConfig.FaultHttpStatusCode));
        
                // A simple way to randomize the failures using the current millisecond.
                // This only provides a 9:1000 chance of a failure/delay, so it won't cause too much chaos, 
                // just enough to make sure the service can withstand the occasionally hiccup.
                switch (DateTime.UtcNow.Millisecond)
                {
                    case 0:
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable));
                    case 1:
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound));
                    case 2:
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
                    case 3:
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.BadGateway));
                    case 4:
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden));
                    case 5:
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
                    case 6:
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.UnsupportedMediaType));
                    case 7:
                        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout));
                    case 8:
                        await Task.Delay(TimeSpan.FromSeconds(DateTime.UtcNow.Minute));
                        break;
                }
            }
        
            return base.SendAsync(request, cancellationToken);
        }

    }
}
