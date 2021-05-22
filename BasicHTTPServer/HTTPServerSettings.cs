// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BasicHTTPServer.Parsers;

namespace BasicHTTPServer
{
    public class HTTPServerSettings
    {
        public ICookieParser CookieParser = new StandartHTTPCookieParser();
        public IHeadParser HeadParser = new StandartHTTPHeadParser();
    }
}