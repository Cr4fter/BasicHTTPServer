using BasicHTTPServer;
using BasicHTTPServer.Parsers;
using NUnit.Framework;

namespace BasicHTTPServerTests.Parsers.CookieParser
{
    [TestFixture]
    public class CookieParser
    {
        private StandartHTTPCookieParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new StandartHTTPCookieParser();
        }

        [Test]
        public void ParseCookie()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseCookies("Cookie_1=value", ref request);
            Assert.AreEqual(1, request.Cookies.Count);
            Assert.AreEqual("value", request.Cookies["Cookie_1"]);
        }
        [Test]
        public void ParseCookies()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseCookies("Cookie_1=value; foo=bar", ref request);
            Assert.AreEqual(2, request.Cookies.Count);
            Assert.AreEqual("value", request.Cookies["Cookie_1"]);
            Assert.AreEqual("bar", request.Cookies["foo"]);
        }
    }
}