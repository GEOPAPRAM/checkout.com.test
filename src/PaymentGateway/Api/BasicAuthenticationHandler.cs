using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentGateway.DataAccess;

namespace PaymentGateway.Api
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IAccountRepository _accountRepository;

        public BasicAuthenticationHandler(IAccountRepository accountRepository,
                                          IOptionsMonitor<AuthenticationSchemeOptions> options,
                                          ILoggerFactory logger,
                                          UrlEncoder encoder,
                                          ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _accountRepository = accountRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var authorizationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Parameter)).Split(':');
                var username = credentials.FirstOrDefault();
                var password = credentials.LastOrDefault();

                var merchantAccount = await _accountRepository.GetWithCredentials(username, password);

                if (merchantAccount == null)
                    throw new ArgumentException("Invalid username or password");

                var claims = new[] { new Claim(ClaimTypes.Name, username), new Claim("MerchantId", merchantAccount.MerchantId.ToString()) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                //TODO: Logging for successful authentication attempt
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                //TODO: Logging for unsuccessful authentication attempt
                return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
            }
        }
    }
}