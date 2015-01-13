using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Cloud_Vibe.Models;
using Cloud_Vibe.Data.Models;
using Cloud_Vibe.Data;
using Microsoft.Owin.Security.Facebook;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cloud_Vibe
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(CloudVibeDbContex.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");


            //Facebook Login Options
            var facebookOptions = new FacebookAuthenticationOptions();

            facebookOptions.Scope.Add("email");
            facebookOptions.Scope.Add("public_profile");

            facebookOptions.AppId = "831232856922288";
            facebookOptions.AppSecret = "5609a5569bee51919cd6b9f6f550e4a4";
            facebookOptions.Provider = new FacebookAuthenticationProvider()
            {
                OnAuthenticated = async  context =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                    foreach (var claim in context.User)
                    {
                        var claimType = string.Format("urn:facebook:{0}", claim.Key);
                        string claimValue = claim.Value.ToString();
                        if (!context.Identity.HasClaim(claimType, claimValue))
                            context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));

                    }

                }
            };

            facebookOptions.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseFacebookAuthentication(facebookOptions);
            //app.UseFacebookAuthentication(
            //   appId: "831232856922288",
            //   appSecret: "5609a5569bee51919cd6b9f6f550e4a4");


            var googleOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "246716165019-ll9umh733aljba2hemsdjna7ft884sbu.apps.googleusercontent.com",
                ClientSecret = "OhgpGlMDNwV1OjQDAtMln7LM",
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = context =>
                    {
                        var userDetail = context.User;
                        //context.Identity.AddClaim(new Claim(ClaimTypes.Name, context.Identity.FindFirstValue(ClaimTypes.Name)));
                        //context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Identity.FindFirstValue(ClaimTypes.Email)));

                        //var gender = userDetail.Value<string>("gender");
                        //context.Identity.AddClaim(new Claim(ClaimTypes.Gender, gender));

                        var picture = userDetail.Value<string>("picture");
                        context.Identity.AddClaim(new Claim("picture", picture));

                        return Task.FromResult(0);
                    },
                },
            };
            googleOptions.Scope.Add("https://www.googleapis.com/auth/plus.login");
            googleOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.email");

            app.UseGoogleAuthentication(googleOptions);
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "246716165019-ll9umh733aljba2hemsdjna7ft884sbu.apps.googleusercontent.com",
            //    ClientSecret = "OhgpGlMDNwV1OjQDAtMln7LM"
            //});
        }
    }
}