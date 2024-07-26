using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MyDbWebApi
{
    public class CtmAuthorizationProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
             
            context.Validated();
            //return base.ValidateClientAuthentication(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //var user = await userManager.FindAsync(context.UserName, context.Password);
            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}
            

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (true)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Test User"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "provided username and password is incorrect.");
                return;
            }
            //return base.GrantResourceOwnerCredentials(context);
        }
    }
}