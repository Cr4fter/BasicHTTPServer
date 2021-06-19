![Base Build](https://github.com/Cr4fter/BasicHTTPServer/actions/workflows/BaseAction.yml/badge.svg)

# BasicHTTPServer
A basic easy to use and extensible HTTP Server written purely in C#

## Example Use
Example on how to Setup a Server Listening on port 8080 responding to /helloworld Requests by a plain Text response.
```csharp
static void Main(string[] args)
{
    HTTPServer server = new HTTPServer(8080);
    server.AddHandler("/hellowrold", helloworldRequest);
    server.StartListening();
    Console.ReadKey();
}

private static HTTPResponse helloworldRequest(HTTPRequest request)
{
    return new HTTPTextResponse("Hello World");
}
```
