// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BasicHTTPServer.Parsers
{
    public interface IHeadParser
    {
        void ParseHead(string head, ref HTTPRequest request);
    }
}