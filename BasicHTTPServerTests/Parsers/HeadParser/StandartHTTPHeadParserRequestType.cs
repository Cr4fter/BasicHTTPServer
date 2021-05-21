using BasicHTTPServer;
using BasicHTTPServer.Parsers;
using NUnit.Framework;

namespace BasicHTTPServerTests.Parsers.HeadParser
{
    [TestFixture]
    public class StandartHTTPHeadParserRequestType
    {
        private StandartHTTPHeadParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new StandartHTTPHeadParser();
        }

        [Test]
        public void TestGETRequest()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual(RequestType.GET, request.ParsedType);
            Assert.AreEqual("GET", request.RawParsedType);
        }

        [Test]
        public void TestHEADRequest()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("HEAD /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual(RequestType.HEAD, request.ParsedType);
            Assert.AreEqual("HEAD", request.RawParsedType);
        }

        [Test]
        public void TestPOSTRequest()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("PUT /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual(RequestType.PUT, request.ParsedType);
            Assert.AreEqual("PUT", request.RawParsedType);
        }

        [Test]
        public void TestDELETERequest()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("DELETE /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual(RequestType.DELETE, request.ParsedType);
            Assert.AreEqual("DELETE", request.RawParsedType);
        }

        [Test]
        public void TestCONNECTRequest()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("CONNECT /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual(RequestType.CONNECT, request.ParsedType);
            Assert.AreEqual("CONNECT", request.RawParsedType);
        }

        [Test]
        public void TestOPTIONSRequest()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("OPTIONS /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual(RequestType.OPTIONS, request.ParsedType);
            Assert.AreEqual("OPTIONS", request.RawParsedType);
        }

        [Test]
        public void TestTRACERequest()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("NOTEXSISTING /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual(RequestType.UNKNOWN, request.ParsedType);
            Assert.AreEqual("NOTEXSISTING", request.RawParsedType);
        }

        [Test]
        public void TestUNKNOWNRequest()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("TRACE /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual(RequestType.TRACE, request.ParsedType);
            Assert.AreEqual("TRACE", request.RawParsedType);
        }
    }
}