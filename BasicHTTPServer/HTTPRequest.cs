using System.Collections.Generic;

namespace BasicHTTPServer
{
    public class HTTPRequest
    {
        public bool RequestMalformed => malforms.Count > 0;
        public List<RequestMalform> malforms = new List<RequestMalform>();

        public RequestType ParsedType;
        public string RawParsedType;
        public string RawRequestURL;
        public string RequestURL;
        public string HTTPVersion;

        

        public Dictionary<string, string> GetParameters = new Dictionary<string, string>();

        public Dictionary<string, string> Headers = new Dictionary<string, string>();

        public Dictionary<string, string> Cookies = new Dictionary<string, string>();
    }
}