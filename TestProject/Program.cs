using System;
using BasicHTTPServer;
using BasicHTTPServer.Response;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            HTTPServer server = new HTTPServer(8080);
            server.AddHandler("/hallowelt", halloweltRequest);
            server.StartListening();
            Console.ReadKey();
        }

        private static HTTPResponse halloweltRequest(HTTPRequest request)
        {
            Console.WriteLine("Hallo Welt Request Received!");
            return new HTTPTextResponse("Hallo Welt");
        }
    }
}
