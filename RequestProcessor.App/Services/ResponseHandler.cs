using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    class ResponseHandler:IResponseHandler
    {
        public Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            return File.WriteAllTextAsync(responseOptions.Path, $"{response.Code} {response.Handled}\n+{response.Content}");
        }
    }
}
