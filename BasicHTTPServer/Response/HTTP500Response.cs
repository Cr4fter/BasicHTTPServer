// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Text;

namespace BasicHTTPServer.Response
{
    /// <summary>
    /// Standard 500 Response send when the server runs into an unhandled exception while processing the request.
    /// </summary>
    public class HTTP500Response : HTTPResponse
    {
        public override byte[] ProcessResponse()
        {
            StringBuilder responseString = new StringBuilder();
            SetStatusCode(500);
            responseString.Append(ProcessHead());
            responseString.Append(ProcessHeader());
            responseString.Append('\n');
            return Encoding.UTF8.GetBytes(responseString.ToString());
        }
    }
}