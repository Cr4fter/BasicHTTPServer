// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Text;

namespace BasicHTTPServer.Response
{
    /// <summary>
    /// Basic Response Returning JSON to the Client.
    /// </summary>
    public class HTTPHtmlResponse : HTTPResponse
    {
        private string _responseBody;

        public HTTPHtmlResponse(string body)
        {
            _responseBody = body;
        }

        /// <summary>
        /// This function assembles Head, Headers and Text Body.
        /// </summary>
        /// <returns>Bytes Ready to be send over the network.</returns>
        public override byte[] ProcessResponse()
        {
            var responseHttp = new StringBuilder();
            SetStatusCode(200);
            responseHttp.Append(ProcessHead());
            Headers.Add("Content-Type", "text/html");
            responseHttp.Append(ProcessHeader());
            responseHttp.Append('\n');
            responseHttp.Append(_responseBody);
            return Encoding.UTF8.GetBytes(responseHttp.ToString());
        }
    }
}