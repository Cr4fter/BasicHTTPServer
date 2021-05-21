using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using BasicHTTPServer.Response;

namespace BasicHTTPServer
{
    public class HTTPServer
    {
        public delegate BasicHTTPResponse RequestHandlerCallback(HTTPRequest request);

        private int _port;

        private TcpListener serverSocket;

        public Dictionary<string, RequestHandlerCallback> RequestHandlers { private set; get; } = new Dictionary<string, RequestHandlerCallback>();

        internal HTTPServerSettings _settings;

        public HTTPServer(int port, HTTPServerSettings settings = null)
        {
            _port = port;
            _settings = settings ?? new HTTPServerSettings();
        }

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
            Socket clientSocket = serverSocket.EndAcceptSocket(ar);
            HTTPRequestHandler Request = new HTTPRequestHandler(clientSocket, this);
            serverSocket.BeginAcceptSocket(AcceptAsync, serverSocket);
        }
    }
}
