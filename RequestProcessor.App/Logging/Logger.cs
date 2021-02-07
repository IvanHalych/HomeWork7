using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

namespace RequestProcessor.App.Logging
{
    class Logger : ILogger
    {
        public void Log(string message)
        {
           Debug.WriteLine(message);
        }

        public void Log(Exception exception, string message)
        {
            Debug.WriteLine(exception.Message);
            Debug.WriteLine(message);
        }
    }
}
