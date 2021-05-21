using BasicHTTPServer;
using BasicHTTPServer.Parsers;
using NUnit.Framework;

namespace BasicHTTPServerTests.Parsers.HeadParser
{
    [TestFixture]
    public class StandartHTTPHeadParserTest
    {
        private StandartHTTPHeadParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new StandartHTTPHeadParser();
        }

        [Test]
        public void IncompleteHead()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt", ref request);
            Assert.IsTrue(request.RequestMalformed);
        }

        [Test]
        public void PollutedHead()
        {
            HTTPRequest request = new HTTPRequest();
            parser.ParseHead("GET /url/hallo/welt HTTP/1.1 THISDOESNOTEXIST", ref request);
            Assert.IsTrue(request.RequestMalformed);
        }
    }
}