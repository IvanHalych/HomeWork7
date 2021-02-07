using System;
using System.Collections.Generic;
using System.Text;

namespace RequestProcessor.App.Models
{
    class Response:IResponse
    {
        public Response(int code, string content)
        {
            Handled = true;
            Code = code;
            Content = content;
        }

        public bool Handled { get; set; }
        public int Code { get; }
        public string Content { get; }
    }
}
