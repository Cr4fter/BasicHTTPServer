namespace BasicHTTPServer.Parsers
{
    public interface ICookieParser
    {
        void ParseCookies(string cookies, ref HTTPRequest request);
    }
}