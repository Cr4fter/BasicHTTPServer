// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using BasicHTTPServer.Response;

namespace BasicHTTPServer
{
    public class HTTPServer
    {
        public delegate HTTPResponse RequestHandlerCallback(HTTPRequest request);

        private readonly int _port;

        internal HTTPServerSettings _settings;

        private TcpListener serverSocket;

        public HTTPServer(int port, HTTPServerSettings settings = null)
        {
            _port = port;
            _settings = settings ?? new HTTPServerSettings();
        }

        public Dictionary<string, RequestHandlerCallback> RequestHandlers { get; } =
            new Dictionary<string, RequestHandlerCallback>();

        public void StartListening()
        {
            serverSocket = new TcpListener(IPAddress.Any, _port);
            serverSocket.Start();
            serverSocket.BeginAcceptSocket(AcceptAsync, serverSocket);
        }

        public void AddHandler(string URL, RequestHandlerCallback requestHandlerCallbackHandler)
        {
            RequestHandlers.Add(URL, requestHandlerCallbackHandler);
        }

        private void AcceptAsync(IAsyncResult ar)
        {
            var clientSocket = serverSocket.EndAcceptSocket(ar);
            var Request = new HTTPRequestHandler(clientSocket, this);
            serverSocket.BeginAcceptSocket(AcceptAsync, serverSocket);
        }
    }
}