using System.Security.Claims;
using System.Text.Encodings.Web;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Scobius.Services;

namespace Scobius.Handlers
{
    public class FirebaseAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        FirebaseAuth firebaseAuth,
        UserService userService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        private readonly FirebaseAuth _firebaseAuth = firebaseAuth;

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.NoResult();
            }

            string bearerToken = Context.Request.Headers.Authorization.ToString();
            Console.WriteLine(bearerToken);
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                return AuthenticateResult.Fail("Invalid token header");
            }

            string token = bearerToken["Bearer ".Length..];

            try
            {
                FirebaseToken firebaseToken = await _firebaseAuth.VerifyIdTokenAsync(token);

                bool exists = await userService.Exists(firebaseToken.Uid);
                UserRecord user = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(firebaseToken.Claims["email"]?.ToString());
                bool emailVerified = user.EmailVerified;

                if (!exists || !emailVerified) return AuthenticateResult.Fail("User doesn't exist");

                foreach (var key in firebaseToken.Claims.Keys)
                    Console.WriteLine(key);
                List<Claim> claims =
                [
                    new(ClaimTypes.NameIdentifier, firebaseToken.Uid),
                    new(ClaimTypes.Email, firebaseToken.Claims["email"]?.ToString() ?? ""),
                ];

                ClaimsIdentity identity = new(claims, Scheme.Name);
                ClaimsPrincipal principal = new(identity);
                AuthenticationTicket ticket = new(principal, Scheme.Name);


                return AuthenticateResult.Success(ticket);
            }
            catch (FirebaseAuthException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }
    }
}
