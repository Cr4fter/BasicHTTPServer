namespace BasicHTTPServer.Response
{
    public class BasicHTTP404Response : BasicHTTPResponse
    {
        public override byte[] ProcessResponse()
        {
            return new byte[1];
        }
    }
}