namespace BasicHTTPServer.Helpers
{
    /// <summary>
    /// <see cref="HTTPRequest" /> only contains data which should not be manipulated by the user, this class contains helper functions that should only be used by the server when parsing requests.
    /// </summary>
    internal static class BasicHTTPRequestHelper
    {
        /// <summary>
        /// Adds a statement that a Request is NonCritical malformed.
        /// </summary>
        /// <param name="request">The request which should be marked as malformed.</param>
        /// <param name="message">The Message why the Request is malformed.</param>
        internal static void MarkAsMalformed(this HTTPRequest request, string message)
        {
            request.malforms.Add(new RequestMalform(Criticality.NonCritical, message));
        }
    }
}