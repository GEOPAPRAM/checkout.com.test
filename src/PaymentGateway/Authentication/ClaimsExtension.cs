using System;
using System.Linq;
using System.Security.Claims;

namespace PaymentGateway.Authentication
{
    public static class ClaimsExtension
    {
        public static Guid GetMerchantId(this ClaimsPrincipal principal)
        {
            return new Guid(principal.Claims.First(x => x.Type == CustomClaims.MerchantId).Value);
        }
    }
}