using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using T7_P2_1.Repositories;

namespace T7_P2_1.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private Func<IAuthRepository> authRepoFactory;

        public SimpleAuthorizationServerProvider(Func<IAuthRepository> authRepoFactory)
        {
            this.authRepoFactory = authRepoFactory;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            IdentityUser user = null;
            IEnumerable<string> roles = null;
            IAuthRepository _repo = authRepoFactory();
            
            user = await _repo.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            roles = await _repo.FindRoles(user.Id);
            
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, string.Join(",", roles)));

            context.Validated(identity);
            _repo.Dispose();
        }
    }

}