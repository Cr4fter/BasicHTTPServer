using System;
using BasicHTTPServer.Helpers;

namespace BasicHTTPServer.Parsers
{
    public class StandartHTTPCookieParser : ICookieParser
    {
        public void ParseCookies(string cookies, ref HTTPRequest request)
        {
            string[] cookieList = cookies.Split(';');
            foreach (string rawCookie in cookieList)
            {
                string[] cookieParts = rawCookie.Split('=');
                if (cookieParts.Length != 2)
                {
                    request.MarkAsMalformed($"Cookie does not consist of exactly one key and one Value part {rawCookie}");
                    continue;
                }
                request.Cookies.Add(cookieParts[0].Trim(), cookieParts[1]);
            }
        }
    }
}