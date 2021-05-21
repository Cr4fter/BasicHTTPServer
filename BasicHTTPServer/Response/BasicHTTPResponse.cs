using System.Collections.Generic;
using System.Text;

namespace BasicHTTPServer.Response
{
    public abstract class BasicHTTPResponse
    {
        public int statusCode;
        public string statusCodeName;

        protected Dictionary<string, string> Headers = new Dictionary<string, string>();

        /// <summary>
        /// Helper function to Set the <see cref="statusCode"/> code and auto fill the <see cref="statusCodeName"/>
        /// </summary>
        /// <param name="statusCode"></param>
        public void SetStatusCode(int statusCode)
        {
            this.statusCode = statusCode;
            switch (statusCode)
            {
                case 200:
                    statusCodeName = "OK";
                    break;
                case 404:
                    statusCodeName = "NOT FOUND";
                    break;
                case 500:
                    statusCodeName = "INTERNAL SERVER ERROR";
                    break;
            }
        }

        public string ProcessHead()
        {
            return $"HTTP/1.1 {statusCode} {statusCodeName}\n";
        }

        public string ProcessHeader()
        {
            StringBuilder headerBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> header in Headers)
            {
                headerBuilder.Append(header.Key);
                headerBuilder.Append(": ");
                headerBuilder.Append(header.Value);
                headerBuilder.Append("\n");
            }

            return headerBuilder.ToString();
        }

        public abstract byte[] ProcessResponse();

    }
}