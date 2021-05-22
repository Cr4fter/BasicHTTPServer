// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Sockets;
using System.Text;
using BasicHTTPServer.Response;

namespace BasicHTTPServer
{
    /// <summary>
    /// The standard HTTP Request Types and <see cref="UNKNOWN"/> to signal a non standard Request type.
    /// </summary>
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

    /// <summary>
    /// This class is used to handle incoming HTTP Requests.
    /// </summary>
    public class HTTPRequestHandler
    {
        private readonly Socket _connection;
        private readonly HTTPServer _server;
        private readonly byte[] buffer = new byte[2048];

        public HTTPRequestHandler(Socket connection, HTTPServer server)
        {
            _server = server;
            _connection = connection;
            _connection.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, BeginReceive, _connection);
        }

        private void BeginReceive(IAsyncResult ar)
        {
            var result = _connection.EndReceive(ar);
            if (IsConnected() == false)
            {
                Console.WriteLine("Connection Lost!");
                return;
            }

            HandleRequest(result);
        }

        /// <summary>
        /// This function is responsible to parse the incoming HTTP Requests, call the responsible callback and send back the answer.
        /// </summary>
        /// <param name="length"></param>
        private void HandleRequest(int length)
        {
            var messageContent = Encoding.UTF8.GetString(buffer, 0, length);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(messageContent);
            Console.WriteLine();
            Console.ResetColor();
            messageContent = messageContent.Replace("\r\n", "\n");
            var lines = messageContent.Split('\n');

            var parsingState = 0; //Start with Headers

            var request = new HTTPRequest();

            for (var i = 0; i < lines.Length; i++)
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

                    var split = lines[i].IndexOf(':');

                    var name = lines[i].Substring(0, split);
                    var value = lines[i].Substring(split + 2, lines[i].Length - split - 2);

                    request.Headers.Add(name, value);

                    if (name == "Cookie") _server._settings.CookieParser.ParseCookies(value, ref request);
                }
                else if (parsingState == 2)
                {
                    //TODO Parse Body
                }

            HTTPResponse HTTPresponse;

            try
            {
                if (_server.RequestHandlers.TryGetValue(request.RequestURL, out var calback))
                    HTTPresponse = calback(request);
                else
                    HTTPresponse = new HTTP404Response();
            }
            catch (Exception)
            {
                HTTPresponse = new HTTP500Response();
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

        /// <summary>
        /// Polls the socket to detect a closed connection.
        /// </summary>
        /// <returns>True if the connection is still open.</returns>
        public bool IsConnected()
        {
            try
            {
                return !(_connection.Poll(1, SelectMode.SelectRead) && _connection.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}