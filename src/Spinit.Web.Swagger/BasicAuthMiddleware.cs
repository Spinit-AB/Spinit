using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Spinit.Web.Swagger
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _skipLocally = true;

        public BasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            var section = configuration.GetSection("SwaggerAuth");
            _username = section["Username"];
            if (string.IsNullOrWhiteSpace(_username))
            {
                throw new Exception("SwaggerAuth.Username is missing in config");
            }

            _password = section["Password"];
            if (string.IsNullOrWhiteSpace(_password))
            {
                throw new Exception("SwaggerAuth.Password is missing in config");
            }

            if (bool.TryParse(section["SkipAuthLocally"], out var skipLocally))
            {
                _skipLocally = skipLocally;
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_skipLocally && IsLocalRequest(context))
            {
                await _next.Invoke(context);
                return;
            }

            if (CheckBasicAuthorization(context))
            {
                await _next.Invoke(context);
                return;
            }

            // Make browser show login dialog for Basic
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        private bool CheckBasicAuthorization(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                try
                {
                    var (username, password) = ParseCredentials(authHeader);
                    if (IsAuthorized(username, password))
                    {
                        return true;
                    }
                }
                catch (ParsingException)
                {
                    return false;
                }
            }

            return false;
        }

        private (string username, string password) ParseCredentials(string authHeader)
        {
            var authHeaderSplit = authHeader.Split(' ');
            if (authHeaderSplit.Length < 2)
            {
                throw new ParsingException();
            }

            var encodedUsernamePassword = authHeaderSplit[1]?.Trim() ?? string.Empty;
            var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

            var decodedValues = decodedUsernamePassword.Split(':');
            if (decodedValues.Length < 2)
            {
                throw new ParsingException();
            }

            var username = decodedValues[0];
            var password = decodedValues[1];
            return (username, password);
        }

        private bool IsAuthorized(string username, string password)
        {
            return username.Equals(_username, StringComparison.InvariantCultureIgnoreCase) && password.Equals(_password);
        }

        private bool IsLocalRequest(HttpContext context)
        {
            if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null)
            {
                return true;
            }
            if (context.Connection.RemoteIpAddress != null && context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
            {
                return true;
            }
            if (IPAddress.IsLoopback(context.Connection.RemoteIpAddress))
            {
                return true;
            }

            return false;
        }

        private class ParsingException : Exception
        {
        }
    }
}
