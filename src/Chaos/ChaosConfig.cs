using System.Net;

namespace Chaos
{
    public class ChaosConfig
    {
        /// <summary>
        /// Indicates whether or not to enable chaos testing
        /// </summary>
        public static bool EnableChaos {get; set;}

        /// <summary>
        /// Indicates whether or not the test should always fault rather than random errors.
        /// This is useful for testing backpressure in your service.
        /// </summary>
        /// <returns></returns>
        public static bool AlwaysFault {get; set;}

        /// <summary>
        /// If AlwaysFault is true, this provides the HttpStatusCode to fault with.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode FaultHttpStatusCode {get; set;}
    }
}