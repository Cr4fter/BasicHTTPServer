using BasicHTTPServer;
using BasicHTTPServer.Parsers;
using NUnit.Framework;

namespace BasicHTTPServerTests.Parsers.HeadParser
{
    [TestFixture]
    public class StandartHTTPHeadParserHTTPVersion
    {

        private StandartHTTPHeadParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new StandartHTTPHeadParser();
        }

        [Test, Order(3)]
        public void TestHTTPVersionV1()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt HTTP/1.1", ref request);

            Assert.AreEqual("HTTP/1.1", request.HTTPVersion);
        }

        [Test, Order(3)]
        public void TestHTTPVersionV2()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt HTTP/2", ref request);

            Assert.AreEqual("HTTP/2", request.HTTPVersion);
            Assert.IsTrue(request.RequestMalformed);
        }

    }
}