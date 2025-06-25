using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;

namespace LibraryManagement.CustomAttributes
{
    public class SimpleTokenAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public SimpleTokenAuthorizeAttribute()
        {
        }
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;

            if (!headers.Contains("Authorization"))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Missing Authorization header");
                return;
            }

            var token = headers.GetValues("Authorization").FirstOrDefault();

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "LibraryAuthServer",
                ValidateAudience = true,
                ValidAudience = "LibraryAuthClient",
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a-very-long-and-secure-key-1234567890")),
                ValidateIssuerSigningKey = true
            };
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token.Replace("Bearer","").Trim(), validationParams, out SecurityToken validatedToken);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token.Replace("Bearer", "").Trim());

            // "sub" is a common claim holding the username
            var username = jwtToken.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)
                ?.Value;

            if (string.IsNullOrWhiteSpace(username))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token");
                return;
            }

            // Set thread principal (optional)
            var identity = new GenericIdentity(username);
            Thread.CurrentPrincipal = new GenericPrincipal(identity, null);
        }
    }
}