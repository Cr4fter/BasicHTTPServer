using System.Text;

namespace BasicHTTPServer.Response
{
    public class BasicHTTPTextResponse : BasicHTTPResponse
    {
        public string ResponseBody;

        public BasicHTTPTextResponse(string body)
        {
            ResponseBody = body;
        }

        public override byte[] ProcessResponse()
        {
            StringBuilder responseHttp = new StringBuilder();
            SetStatusCode(200);
            responseHttp.Append(ProcessHead());
            Headers.Add("Content-Type", "text/plain");
            responseHttp.Append(ProcessHeader());
            return Encoding.UTF8.GetBytes(responseHttp.ToString());
        }
    }
}