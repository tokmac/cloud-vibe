namespace Cloud_Vibe.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Microsoft.AspNet.Identity.Owin;

    using Cloud_Vibe.Data;
    using Cloud_Vibe.Data.Models;
    using Cloud_Vibe.Models;


    public static class AuthorizationUtility
    {
        public static void CheckIfProfilePictureChanged(ExternalLoginInfo loginInfo, ICloudVibeData data)
        {
            var currentIdentifier = "";
            var currentUserId = "";
            User currentUser = new User();
            dynamic currentAvatar = null;

            switch (loginInfo.Login.LoginProvider)
            {
                case "Facebook":
                    currentIdentifier = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:facebook:id").Value;
                    currentUserId = data.SocialAccountLinks.All().FirstOrDefault(sal => sal.Identifier == currentIdentifier).User.Id;
                    currentUser = data.Users.GetById(currentUserId);
                    currentAvatar = FilesByteUtility.ImageFromUrlToByteArray("http://graph.facebook.com/" + currentIdentifier + "/picture?width=200&height=200");
                    break;
                case "Google":
                    currentIdentifier = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                    currentUserId = data.SocialAccountLinks.All().FirstOrDefault(sal => sal.Identifier == currentIdentifier).User.Id;
                    currentUser = data.Users.GetById(currentUserId);
                    currentAvatar = FilesByteUtility.ImageFromUrlToByteArray(loginInfo.ExternalIdentity.Claims.First(c => c.Type == "picture").Value);
                    break;
                default:
                    break;
            }

            if (!currentUser.Avatar.SequenceEqual((byte[])currentAvatar))
            {
                currentUser.Avatar = currentAvatar;
                data.SaveChanges();
            }
        }

        public static ExternalLoginConfirmationViewModel GetRightExternalLoginConfirmationViewModel(ExternalLoginInfo loginInfo)
        {
            switch (loginInfo.Login.LoginProvider)
            {
                case "Facebook":
                    return new ExternalLoginConfirmationViewModel
                        {
                            UserName = loginInfo.DefaultUserName,
                            FirstName = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:first_name").Value,
                            LastName = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:last_name").Value,
                            Email = loginInfo.Email
                        };
                case "Google":
                    return new ExternalLoginConfirmationViewModel
                        {
                            UserName = loginInfo.DefaultUserName,
                            FirstName = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value,
                            LastName = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value,
                            Email = loginInfo.Email
                        };
                default:
                    return null;
            }
        }

        public static byte[] GetRightProfilePicture(ExternalLoginInfo loginInfo)
        {
            switch (loginInfo.Login.LoginProvider)
            {
                case "Facebook":
                    var currentUserId = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:id").Value;
                    return FilesByteUtility.ImageFromUrlToByteArray("http://graph.facebook.com/" + currentUserId + "/picture?width=200&height=200");
                case "Google":
                    return FilesByteUtility.ImageFromUrlToByteArray(loginInfo.ExternalIdentity.Claims.First(c => c.Type == "picture").Value);
                default:
                    return null;
            }
        }

        public static void AddSocialAccountLinkForUser(ExternalLoginInfo loginInfo, ICloudVibeData data, User user)
        {
            switch (loginInfo.Login.LoginProvider)
            {
                case "Facebook":
                    var currentFacebookLink = new SocialAccountLink
                        {
                            User = data.Users.GetById(user.Id),
                            SocialNetwork = data.SocialNetworks.All().FirstOrDefault(n => n.Name == loginInfo.Login.LoginProvider),

                            Identifier = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:id").Value,
                            AccountLink = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:link").Value
                        };

                        data.SocialAccountLinks.Add(currentFacebookLink);
                        data.SaveChanges();
                    break;
                case "Google":
                    var currentGoogleLink = new SocialAccountLink
                        {
                            User = data.Users.GetById(user.Id),
                            SocialNetwork = data.SocialNetworks.All().FirstOrDefault(n => n.Name == loginInfo.Login.LoginProvider),
                            Identifier = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value,
                            AccountLink = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "urn:google:profile").Value
                        };
                    data.SocialAccountLinks.Add(currentGoogleLink);
                        data.SaveChanges();
                    break;
                default:
                    break;
            }
        }
    }
}