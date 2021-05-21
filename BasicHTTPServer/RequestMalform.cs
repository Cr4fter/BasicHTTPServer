using System.Threading;

namespace BasicHTTPServer
{
    public enum Criticality
    {
        NonCritical,
        Critical
    }

    public class RequestMalform
    {
        public RequestMalform(Criticality criticality, string message)
        {
            Criticality = criticality;
            Message = message;
        }

        public Criticality Criticality;
        public string Message;
    }
}