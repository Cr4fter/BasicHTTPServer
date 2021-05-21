namespace BasicHTTPServer.Parsers
{
    public interface IHeadParser
    {
        void ParseHead(string head, ref HTTPRequest request);
    }
}
