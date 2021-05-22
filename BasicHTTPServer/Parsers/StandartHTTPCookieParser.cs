// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BasicHTTPServer.Helpers;

namespace BasicHTTPServer.Parsers
{
    public class StandartHTTPCookieParser : ICookieParser
    {
        public void ParseCookies(string cookies, ref HTTPRequest request)
        {
            var cookieList = cookies.Split(';');
            foreach (var rawCookie in cookieList)
            {
                var cookieParts = rawCookie.Split('=');
                if (cookieParts.Length != 2)
                {
                    request.MarkAsMalformed(
                        $"Cookie does not consist of exactly one key and one Value part {rawCookie}");
                    continue;
                }

                request.Cookies.Add(cookieParts[0].Trim(), cookieParts[1]);
            }
        }
    }
}