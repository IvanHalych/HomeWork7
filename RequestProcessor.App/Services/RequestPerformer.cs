using System;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Menu;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        private IRequestHandler requestHandler;
        private IResponseHandler responseHandler;
        private ILogger logger;
        public RequestPerformer(
            IRequestHandler requestHandler,
            IResponseHandler responseHandler, 
            ILogger logger)
        {
            this.requestHandler = requestHandler;
            this.responseHandler = responseHandler;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(
            IRequestOptions requestOptions, 
            IResponseOptions responseOptions)
        {
            Response response = null;
            try
            {
                logger.Log($"Send: {requestOptions.Name}");
                MainMenu.ConsoleWrite($"Send: {requestOptions.Name}");

                response = (Response)await requestHandler.HandleRequestAsync(requestOptions);

                logger.Log($"Response: {requestOptions.Name} Code:{response.Code}");
                MainMenu.ConsoleWrite($"Response: {requestOptions.Name} Code:{response.Code}");

                await responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
                return true;
            }
            catch (Exception exception)
            {
                if (response != null)
                {
                    response.Handled = false;
                    await responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
                }

                throw new PerformException("",exception);
            }
        }
    }
}
