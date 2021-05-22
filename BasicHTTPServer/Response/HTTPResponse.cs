// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Text;

namespace BasicHTTPServer.Response
{
    /// <summary>
    /// Base class for all HTTP Responses.
    /// </summary>
    public abstract class HTTPResponse
    {
        /// <summary>
        /// KeyValue Store containing all Headers for the response.
        /// </summary>
        protected Dictionary<string, string> Headers = new Dictionary<string, string>();

        /// <summary>
        /// The Response Status Code as an int Representation.
        /// </summary>
        public int StatusCode;
        /// <summary>
        /// The Response Status Code as string int Representation.
        /// </summary>
        public string StatusCodeName;

        /// <summary>
        /// Helper function to Set the <see cref="StatusCode" /> code and auto fill the <see cref="StatusCodeName" />
        /// </summary>
        /// <param name="statusCode"></param>
        public void SetStatusCode(int statusCode)
        {
            this.StatusCode = statusCode;
            switch (statusCode)
            {
                case 100:
                    StatusCodeName = "Continue";
                    break;
                case 101:
                    StatusCodeName = "Switching Protocols";
                    break;
                case 200:
                    StatusCodeName = "OK";
                    break;
                case 201:
                    StatusCodeName = "Created";
                    break;
                case 202:
                    StatusCodeName = "Accepted";
                    break;
                case 203:
                    StatusCodeName = "Non - Authoritative Information";
                    break;
                case 204:
                    StatusCodeName = "No Content";
                    break;
                case 205:
                    StatusCodeName = "Reset Content";
                    break;
                case 206:
                    StatusCodeName = "Partial Content";
                    break;
                case 300:
                    StatusCodeName = "Multiple Choices";
                    break;
                case 301:
                    StatusCodeName = "Moved Permanently";
                    break;
                case 302:
                    StatusCodeName = "Found";
                    break;
                case 303:
                    StatusCodeName = "See Other";
                    break;
                case 304:
                    StatusCodeName = "Not Modified";
                    break;
                case 305:
                    StatusCodeName = "Use Proxy";
                    break;
                case 307:
                    StatusCodeName = "Temporary Redirect";
                    break;
                case 400:
                    StatusCodeName = "Bad Request";
                    break;
                case 401:
                    StatusCodeName = "Unauthorized";
                    break;
                case 402:
                    StatusCodeName = "Payment Required";
                    break;
                case 403:
                    StatusCodeName = "Forbidden";
                    break;
                case 404:
                    StatusCodeName = "Not Found";
                    break;
                case 405:
                    StatusCodeName = "Method Not Allowed";
                    break;
                case 406:
                    StatusCodeName = "Not Acceptable";
                    break;
                case 407:
                    StatusCodeName = "Proxy Authentication Required";
                    break;
                case 408:
                    StatusCodeName = "Request Time-out";
                    break;
                case 409:
                    StatusCodeName = "Conflict";
                    break;
                case 410:
                    StatusCodeName = "Gone";
                    break;
                case 411:
                    StatusCodeName = "Length Required";
                    break;
                case 412:
                    StatusCodeName = "Precondition Failed";
                    break;
                case 413:
                    StatusCodeName = "Request Entity Too Large";
                    break;
                case 414:
                    StatusCodeName = "Request - URI Too Large";
                    break;
                case 415:
                    StatusCodeName = "Unsupported Media Type";
                    break;
                case 416:
                    StatusCodeName = "Requested range not satisfiable";
                    break;
                case 417:
                    StatusCodeName = "Expectation Failed";
                    break;
                case 500:
                    StatusCodeName = "Internal Server Error";
                    break;
                case 501:
                    StatusCodeName = "Not Implemented";
                    break;
                case 502:
                    StatusCodeName = "Bad Gateway";
                    break;
                case 503:
                    StatusCodeName = "Service Unavailable";
                    break;
                case 504:
                    StatusCodeName = "Gateway Time-out";
                    break;
                case 505:
                    StatusCodeName = "HTTP Version not supported";
                    break;
            }
        }

        /// <summary>
        /// This Helper function Returns the HTTP Response Head as string.
        /// </summary>
        /// <returns></returns>
        public string ProcessHead()
        {
            return $"HTTP/1.1 {StatusCode} {StatusCodeName}\n";
        }

        /// <summary>
        /// This Helper function Processes all registered Headers.
        /// </summary>
        /// <returns>All Response Headers in a Single String</returns>
        public string ProcessHeader()
        {
            var headerBuilder = new StringBuilder();
            foreach (var header in Headers)
            {
                headerBuilder.Append(header.Key);
                headerBuilder.Append(": ");
                headerBuilder.Append(header.Value);
                headerBuilder.Append("\n");
            }

            return headerBuilder.ToString();
        }

        /// <summary>
        /// This function should combine Head, Headers and Optional Body for the response.
        /// </summary>
        /// <returns>A Byte Array Ready to be send back to the Requesting Client.</returns>
        public abstract byte[] ProcessResponse();
    }
}