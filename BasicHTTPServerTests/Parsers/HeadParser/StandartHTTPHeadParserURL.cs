using BasicHTTPServer;
using BasicHTTPServer.Parsers;
using NUnit.Framework;

namespace BasicHTTPServerTests.Parsers.HeadParser
{
    [TestFixture]
    public class StandartHTTPHeadParserURL
    {
        private StandartHTTPHeadParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new StandartHTTPHeadParser();
        }

        [Test]
        public void TestNoGetParameters()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt HTTP/1.1", ref request);
            Assert.AreEqual("/url/hallo/welt", request.RequestURL);
            Assert.AreEqual(0, request.GetParameters.Count);
        }

        [Test]
        public void TestOneGetParameters()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt?foo=bar HTTP/1.1", ref request);
            Assert.AreEqual("/url/hallo/welt", request.RequestURL);
            Assert.AreEqual(1, request.GetParameters.Count);
            Assert.AreEqual("bar", request.GetParameters["foo"]);
        }

        [Test]
        public void TestTwoGetParameters()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt?foo=bar&ping=pong HTTP/1.1", ref request);
            Assert.AreEqual("/url/hallo/welt", request.RequestURL);
            Assert.AreEqual(2, request.GetParameters.Count);
            Assert.AreEqual("bar", request.GetParameters["foo"]);
            Assert.AreEqual("pong", request.GetParameters["ping"]);
        }

        [Test]
        public void TestGetParameterPollution()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt?foo=bar&foo=ping HTTP/1.1", ref request);
            Assert.AreEqual("/url/hallo/welt", request.RequestURL);
            Assert.AreEqual(1, request.GetParameters.Count);
            Assert.AreEqual("ping", request.GetParameters["foo"]);
            Assert.IsTrue(request.RequestMalformed);
        }

        [Test]
        public void GetParameterEmptyValue()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt?foo=&ping=pong HTTP/1.1", ref request);
            Assert.AreEqual("/url/hallo/welt", request.RequestURL);
            Assert.AreEqual(2, request.GetParameters.Count);
            Assert.AreEqual("pong", request.GetParameters["ping"]);
            Assert.AreEqual(string.Empty, request.GetParameters["foo"]);
            Assert.IsFalse(request.RequestMalformed);
        }

        [Test]
        public void TestMalformedGetParameter()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt?foo&ping=pong HTTP/1.1", ref request);
            Assert.AreEqual("/url/hallo/welt", request.RequestURL);
            Assert.AreEqual(1, request.GetParameters.Count);
            Assert.AreEqual("pong", request.GetParameters["ping"]);
            Assert.IsTrue(request.RequestMalformed);
        }

    }
}