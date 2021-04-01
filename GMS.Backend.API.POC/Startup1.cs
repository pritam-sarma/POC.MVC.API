using GMS.Backend.API.POC.GMS.API.DAL;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(GMS.Backend.API.POC.Startup1))]

namespace GMS.Backend.API.POC
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {

            var httpConfiguration = CreateHttpConfiguration();

            app.UseWebApi(httpConfiguration);
            // configure Web API to use only bearer token authentication.
            // config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // enable OAuth auth server
            bool allowInsecureHttp = app.Properties.ContainsKey("host.AppMode") && string.Equals("development", app.Properties["host.AppMode"].ToString(), StringComparison.Ordinal);
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = allowInsecureHttp,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = GetTokenExpireTime(),
                Provider = new GMSSecurityAuthorizationServerProvider()
            });

            // enable OAuth bearer auth
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                Realm = "GMS"
            });
        }

        public static HttpConfiguration CreateHttpConfiguration()
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            return httpConfiguration;
        }
        private TimeSpan GetTokenExpireTime()
        {
            return TimeSpan.FromHours(24);
        }

    }
}
