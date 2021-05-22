// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace BasicHTTPServer
{
    /// <summary>
    /// This class contains all parsed informations of a web request.
    /// </summary>
    public class HTTPRequest
    {
        /// <summary>
        /// All cookies in KeyValue Form.
        /// </summary>
        public Dictionary<string, string> Cookies = new Dictionary<string, string>();

        /// <summary>
        /// KeyValue Store containing all Valid Get Parameters of the request.
        /// </summary>
        public Dictionary<string, string> GetParameters = new Dictionary<string, string>();

        /// <summary>
        /// KeyValue Store of all Headers Send in the Request.
        /// This List Contains all Headers even the ones that were parsed into other values in this class.
        /// </summary>
        public Dictionary<string, string> Headers = new Dictionary<string, string>();

        /// <summary>
        /// The HTTP Version send in this Request.
        /// The Server will handle all requests as HTTP/1.1 Requests regardles of the value of this field.
        /// </summary>
        public string HTTPVersion;

        /// <summary>
        /// List of all Malformation Complaints about this request.
        /// </summary>
        public List<RequestMalform> malforms = new List<RequestMalform>();

        /// <summary>
        /// A Bool Representation representing wether there was a Malformed field in the request.
        /// </summary>
        public bool RequestMalformed => malforms.Count > 0;

        /// <summary>
        /// The Parsed type of the Request <see cref="RequestType"/>.
        /// </summary>
        public RequestType ParsedType;

        /// <summary>
        /// The Raw string containing the Request code that was parsed.
        /// This can be used to implement non Standard Request types.
        /// </summary>
        public string RawParsedType;
        
        /// <summary>
        /// The Raw Request URL containing the Requested address with all the Get Parameters. (e.g. /test?foo=bar)
        /// </summary>
        public string RawRequestURL;

        /// <summary>
        /// The requested url which is used to find registered callbacks. (e.g. /test)
        /// </summary>
        public string RequestURL;
    }
}