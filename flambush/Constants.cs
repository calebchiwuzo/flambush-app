using System;
using System.Collections.Generic;
using System.Text;

namespace flambush
{
    public static class Constants
    {
        public static string AppName = "flambush";

        // Google OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string GoogleiOSClientId = "541656516701-292q9034bgm3258599d1bdgiodm077q8.apps.googleusercontent.com";
        public static string GoogleAndroidClientId = "541656516701-qhf4073jeqa0voi4jsbdd4onp9daouf0.apps.googleusercontent.com";

        // These values do not need changing 
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string GoogleiOSRedirectUrl = "com.googleusercontent.apps.541656516701-292q9034bgm3258599d1bdgiodm077q8:/oauth2redirect";
        public static string GoogleAndroidRedirectUrl = "com.googleusercontent.apps.541656516701-qhf4073jeqa0voi4jsbdd4onp9daouf0:/oauth2redirect";

        //-------------------------------------------------------------------------------------------------------

        // Facebook OAuth
        // For Facebook login, configure at https://developers.facebook.com
        public static string FacebookiOSClientId = "473111487402617";
        public static string FacebookAndroidClientId = "473111487402617";

        // These values do not need changing
        public static string FacebookScope = "email";
        public static string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
        public static string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email&access_token={accessToken}";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string FacebookiOSRedirectUrl = "https://www.facebook.com/connect/login_success.html:/oauth2redirect";
        public static string FacebookAndroidRedirectUrl = "https://www.facebook.com/connect/login_success.html";
    }
}
