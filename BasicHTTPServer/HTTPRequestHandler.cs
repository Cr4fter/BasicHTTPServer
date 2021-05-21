using System;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using BasicHTTPServer.Response;

namespace BasicHTTPServer
{
    public enum RequestType
    {
        GET,
        HEAD,
        POST,
        PUT,
        DELETE,
        CONNECT,
        OPTIONS,
        TRACE,
        PATH,
        UNKNOWN
    }

    public class HTTPRequestHandler
    {
        private BasicHTTPServer.HTTPServer _server;
        private Socket _connection;
        private byte[] buffer = new byte[2048];

        public HTTPRequestHandler(Socket connection, BasicHTTPServer.HTTPServer server)
        {
            _server = server;
            _connection = connection;
            _connection.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, BeginReceive, _connection);
        }

        private void BeginReceive(IAsyncResult ar)
        {
            int result = _connection.EndReceive(ar);
            if (IsConnected(_connection) == false)
            {
                Console.WriteLine("Connection Lost!");
                return;
            }
            handleRequest(result);
        }

        private void handleRequest(int length)
        {
            string messageContent = Encoding.UTF8.GetString(buffer, 0, length);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(messageContent);
            Console.WriteLine();
            Console.ResetColor();
            messageContent = messageContent.Replace("\r\n", "\n");
            string[] lines = messageContent.Split('\n');

            int parsingState = 0; //Start with Headers

            HTTPRequest request = new HTTPRequest();

            for (int i = 0; i < lines.Length; i++)
            {
                //GET /hallowelt HTTP/1.1
                if (parsingState == 0)
                {
                    _server._settings.HeadParser.ParseHead(lines[i], ref request);
                    parsingState++;
                } 
                else if (parsingState == 1)
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                    {
                        parsingState++;
                        continue;
                    }
                    int split = lines[i].IndexOf(':');

                    var name = lines[i].Substring(0, split);
                    var value = lines[i].Substring(split + 2, lines[i].Length - split - 2);
                    
                    request.Headers.Add(name, value);

                    if (name == "Cookie")
                    {
                        _server._settings.CookieParser.ParseCookies(value, ref request);
                    }
                }
                else if (parsingState == 2)
                {
                    //TODO Parse Body
                }
            }

            BasicHTTPResponse HTTPresponse;

            try
            {
                if (_server.RequestHandlers.TryGetValue(request.RequestURL, out BasicHTTPServer.HTTPServer.RequestHandlerCallback calback))
                {
                    HTTPresponse = calback(request);
                }
                else
                {
                    HTTPresponse = new BasicHTTP404Response();
                }
            }
            catch (Exception exception)
            {
                HTTPresponse = new BasicHTTP500Response();
            }

            //string responseStr =
            //    "HTTP/1.1 200 OK\n" +
            //    "Access-Control-Allow-Origin: *\n" +
            //    "Connection: Keep-Alive\n" +
            //    "Content-Encoding: gzip\n" +
            //    "Content-Type: text/html; charset = utf-8\n" +
            //    "Date: Mon, 18 Jul 2016 16:06:00 GMT\n" +
            //    "Etag: \"c561c68d0ba92bbeb8b0f612a9199f722e3a621a\"\n" +
            //    "Keep-Alive: timeout = 5, max = 997\n" +
            //    "Last-Modified: Mon, 18 Jul 2016 02:36:04 GMT\n" +
            //    "Server: BasicHTTPServer\n" +
            //    "Set-Cookie: mykey = myvalue; expires = Mon, 17-Jul-2017 16:06:00 GMT; Max-Age = 31449600; Path =/; secure\n" +
            //    "Transfer-Encoding: chunk\n" +
            //    "Vary: Cookie, Accept-Encoding\n" +
            //    "\n\n";
            //byte[] response = Encoding.UTF8.GetBytes(responseStr);
            _connection.Send(HTTPresponse.ProcessResponse());
            _connection.Close();
            _connection.Dispose();
        }

        public bool IsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }
    }
}