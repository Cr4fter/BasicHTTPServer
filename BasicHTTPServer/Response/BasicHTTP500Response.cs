namespace BasicHTTPServer.Response
{
    public class BasicHTTP500Response : BasicHTTPResponse
    {
        public override byte[] ProcessResponse()
        {
            return new byte[1];
        }
    }
}