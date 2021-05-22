// // This is an open source non-commercial project. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BasicHTTPServer.Helpers
{
    /// <summary>
    ///     <see cref="HTTPRequest" /> only contains data which should not be manipulated by the user, this class contains
    ///     helper functions that should only be used by the server when parsing requests.
    /// </summary>
    internal static class HTTPRequestHelper
    {
        /// <summary>
        ///     Adds a statement that a Request is NonCritically malformed.
        /// </summary>
        /// <param name="request">The request which should be marked as malformed.</param>
        /// <param name="message">The Message why the Request is malformed.</param>
        internal static void MarkAsMalformed(this HTTPRequest request, string message)
        {
            request.malforms.Add(new RequestMalform(Criticality.NonCritical, message));
        }

        /// <summary>
        ///     Adds a statement that a Request is Critically malformed.
        /// </summary>
        /// <param name="request">The request which should be marked as malformed.</param>
        /// <param name="message">The Message why the Request is malformed.</param>
        internal static void MarkAsCriticallyMalformed(this HTTPRequest request, string message)
        {
            request.malforms.Add(new RequestMalform(Criticality.Critical, message));
        }
    }
}