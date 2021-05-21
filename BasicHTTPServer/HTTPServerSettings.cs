using BasicHTTPServer.Parsers;

namespace BasicHTTPServer
{
    public class HTTPServerSettings
    {
        public IHeadParser HeadParser = new StandartHTTPHeadParser();
        public ICookieParser CookieParser = new StandartHTTPCookieParser();
    }
}