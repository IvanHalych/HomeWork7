using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    class RequestHandler:IRequestHandler
    {
        private HttpClient httpClient;

        public RequestHandler(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            using var message = new HttpRequestMessage(
                MapMetod(requestOptions.Method),
                new Uri(requestOptions.Address));
            if (requestOptions.Body != null)
            {
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(requestOptions.ContentType));
                message.Content = new StringContent(requestOptions.Body);
            }
                using var response = await httpClient.SendAsync(message);
            response.Content.Headers.ContentType.CharSet = "UTF-8";
            return new Response((int)response.StatusCode, await response.Content.ReadAsStringAsync());
           }

        public HttpMethod MapMetod(RequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Patch:
                    return HttpMethod.Patch;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestMethod), requestMethod, null);
            }
        }
    }
}
