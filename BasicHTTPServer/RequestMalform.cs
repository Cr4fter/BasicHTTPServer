// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BasicHTTPServer
{
    public enum Criticality
    {
        NonCritical,
        Critical
    }

    public class RequestMalform
    {
        public Criticality Criticality;
        public string Message;

        public RequestMalform(Criticality criticality, string message)
        {
            Criticality = criticality;
            Message = message;
        }
    }
}