// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Text;

namespace BasicHTTPServer.Response
{
    /// <summary>
    /// Standard 404 Response send to the client when no Callback for the requested Address found.
    /// </summary>
    public class HTTP404Response : HTTPResponse
    {
        public override byte[] ProcessResponse()
        {
            StringBuilder responseString = new StringBuilder();
            SetStatusCode(404);
            responseString.Append(ProcessHead());
            responseString.Append(ProcessHeader());
            responseString.Append('\n');
            return Encoding.UTF8.GetBytes(responseString.ToString());
        }
    }
}