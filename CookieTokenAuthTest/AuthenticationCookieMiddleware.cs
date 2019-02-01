using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieTokenAuthTest
{
    /// <summary>
    /// Middleware that copies the bearer access token from cookie to Authorization header
    /// </summary>
    public class AuthenticationCookieMiddleware
    {
        public const string AuthCookieName = "yunona_access_token";

        private readonly RequestDelegate _next;

        public AuthenticationCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext http)
        {
            var request = http.Request;

            if (request.Cookies.TryGetValue(AuthCookieName, out var authToken))
            {
                request.Headers.TryAdd("Authorization", "Bearer " + authToken);
            }

            await _next(http);
        }
    }
}
