using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RequestProcessor.App.Models
{
    internal class RequestOptions : IResponseOptions,IRequestOptions
    {
        public RequestOptions()
        {
        }

        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("method")]
        public RequestMethod Method
        {
            get;
            set;
        }

        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }

        public bool IsValid
        {
            get
            {
                if ((Address == null) || (Path == null)||(Name ==null))
                    return false;
                if (((Method == RequestMethod.Post) || (Method == RequestMethod.Put))&&
                     (Body != null))
                    return true;
                return Method == RequestMethod.Get || Method == RequestMethod.Patch;
            }
        }
    }
}
