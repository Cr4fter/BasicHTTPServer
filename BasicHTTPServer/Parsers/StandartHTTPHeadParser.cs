// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BasicHTTPServer.Helpers;

namespace BasicHTTPServer.Parsers
{
    public class StandartHTTPHeadParser : IHeadParser
    {
        public void ParseHead(string head, ref HTTPRequest request)
        {
            var parts = head.Split(' ');

            if (parts.Length != 3) request.MarkAsMalformed($"Expected 3 Part Header but found {parts.Length}");

            //Request Type
            switch (parts[0])
            {
                case "GET":
                    request.ParsedType = RequestType.GET;
                    break;
                case "HEAD":
                    request.ParsedType = RequestType.HEAD;
                    break;
                case "POST":
                    request.ParsedType = RequestType.POST;
                    break;
                case "PUT":
                    request.ParsedType = RequestType.PUT;
                    break;
                case "DELETE":
                    request.ParsedType = RequestType.DELETE;
                    break;
                case "CONNECT":
                    request.ParsedType = RequestType.CONNECT;
                    break;
                case "OPTIONS":
                    request.ParsedType = RequestType.OPTIONS;
                    break;
                case "TRACE":
                    request.ParsedType = RequestType.TRACE;
                    break;
                case "PATH":
                    request.ParsedType = RequestType.PATH;
                    break;
                default:
                    request.ParsedType = RequestType.UNKNOWN;
                    break;
            }

            request.RawParsedType = parts[0];

            //Request URL
            request.RawRequestURL = parts[1];
            var urlParts = request.RawRequestURL.Split('?');
            request.RequestURL = urlParts[0];
            if (urlParts.Length > 2)
            {
                request.MarkAsMalformed(
                    $"Request URI has multiple Parameter Splitter. Max Allowed 1 found {urlParts.Length}. Full URI {request.RequestURL}");
            }
            else if (urlParts.Length == 2)
            {
                var getParameters = urlParts[1].Split('&');
                foreach (var parameter in getParameters)
                {
                    var paramParts = parameter.Split('=');
                    if (paramParts.Length != 2)
                    {
                        request.MarkAsMalformed($"Failed to Parse GetParameter. full Parameter: {parameter}");
                        continue;
                    }

                    if (request.GetParameters.ContainsKey(paramParts[0])) //HTTP Pollution Check
                    {
                        request.GetParameters[paramParts[0]] = paramParts[1];
                        request.MarkAsMalformed(
                            $"HTTP Pollution Detected! Multiple Get Parameters with the Name \"{paramParts[0]}\"");
                    }
                    else
                    {
                        request.GetParameters.Add(paramParts[0], paramParts[1]);
                    }
                }
            }

            //HTTP Version
            if (parts.Length >= 3)
            {
                request.HTTPVersion = parts[2];
                if (request.HTTPVersion != "HTTP/1.1")
                    request.MarkAsMalformed("The Server Currently only Supports HTTP v1 Requests.");
            }
        }
    }
}