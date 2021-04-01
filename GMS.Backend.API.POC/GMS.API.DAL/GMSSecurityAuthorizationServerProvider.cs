using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;

namespace GMS.Backend.API.POC.GMS.API.DAL
{
    public class GMSSecurityAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

     
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //var response = GMS.Security.Auth.SignIn(context.UserName, context.Password, addToLoginHistory: true, applicationType: GMS.Security.ApplicationType.ProActTransportWeb);
            var result = new User().Authenticate(context.UserName, context.Password);

            if (result)
            {
                //var gmsPrincipal = GMS.Security.GmsPrincipalRepository.GetPrincipal(context.UserName, true);

                //var gmsIdentity = gmsPrincipal.GmsIdentity;

                //var userGroupId = gmsPrincipal.UserGroupId;

                var identity = new ClaimsIdentity(new[]
                        {
                            new Claim("UserName", context.UserName),
                            //new Claim(GmsClaimTypes.Username, gmsIdentity.Name),
                            //new Claim(GmsClaimTypes.UserGroupId, userGroupId.ToString()),
                            //new Claim(GmsClaimTypes.ParentUserGroupId, gmsPrincipal.ParentUserGroupId.ToString()),
                            //new Claim(GmsClaimTypes.GroupCompanyIdents,string.Join(",",gmsPrincipal.GroupCompanyIdents)),
                            //new Claim("PasswordExpires", FormatPasswordExpiresDate(response.PasswordExpires)),
                            //new Claim("SignInResult", response.Result.ToString())
                        }, context.Options.AuthenticationType);

                //if (response.Result == GMS.Security.SignInResult.Success)
                //{
                //    foreach (var permission in gmsPrincipal.Permissions)
                //    {
                //        identity.AddClaim(new Claim(ClaimTypes.Role, permission.ToString()));
                //    }
                //}

                context.Validated(identity);
            }
            else
            {
                context.SetError("Invalid user name or password");
            }

            return Task.FromResult<object>(null);
        }

        private static string FormatPasswordExpiresDate(DateTime passwordExpires)
        {
            if (passwordExpires.Year < 1602)
            {
                return DateTime.UtcNow.AddYears(-1).ToFileTimeUtc().ToString();
            }
            return passwordExpires.ToFileTimeUtc().ToString();
        }

        //public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
        //    context.AdditionalResponseParameters.Add("claim_UserId", context.Identity.FindFirst(GmsClaimTypes.UserId).Value);
        //    context.AdditionalResponseParameters.Add("claim_Username", context.Identity.FindFirst(GmsClaimTypes.Username).Value);
        //    context.AdditionalResponseParameters.Add("claim_UserGroupId", context.Identity.FindFirst(GmsClaimTypes.UserGroupId).Value);
        //    context.AdditionalResponseParameters.Add("claim_ParentUserGroupId", context.Identity.FindFirst(GmsClaimTypes.ParentUserGroupId).Value);
        //    context.AdditionalResponseParameters.Add("claim_GroupCompanyIdents", context.Identity.FindFirst(GmsClaimTypes.GroupCompanyIdents).Value);
        //    context.AdditionalResponseParameters.Add("claim_Roles", string.Join(",", context.Identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)));

        //    context.AdditionalResponseParameters.Add("PasswordExpires", context.Identity.FindFirst("PasswordExpires").Value);
        //    context.AdditionalResponseParameters.Add("SignInResult", context.Identity.FindFirst("SignInResult").Value);

        //    return Task.FromResult<object>(null);
        //}
    }
}
