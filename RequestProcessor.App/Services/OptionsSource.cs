using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    class OptionsSource:IOptionsSource
    {
        private string pathOption;
        public OptionsSource(string path)
        {
            pathOption = path;
        }

        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            var json = await File.ReadAllTextAsync(pathOption);
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            var list = JsonSerializer.Deserialize<List<RequestOptions>>(json,options);
            var list1 = list.Select(l => ((IRequestOptions)l, (IResponseOptions)l)).ToList();
            return list1.AsEnumerable();
        }

    }
}
