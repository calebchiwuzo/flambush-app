using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flambush
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class howitworks : ContentPage
    {
        
        Account account;
        [Obsolete]

        AccountStore store;

        [Obsolete]
        public howitworks()
        {
            InitializeComponent();
            store = AccountStore.Create();
            HowItWorks_Tutorial();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await getAPIData();
            });
        }
        public IList<ItemClass> Items { get; private set; }
        public class ItemClass
        {
            public string img_path { get; set; }
            public string text { get; set; }
        }  
        
        public class respAPI
        {
            string data { get; set; }
        }

        void HowItWorks_Tutorial()
        {
            Items = new List<ItemClass>();
            Items.Add(new ItemClass
            {
                img_path = "hand_with_phone",
                text = "swipe"
            });
            Items.Add(new ItemClass
            {
                img_path = "hand_with_phone",
                text = "swipe"
            });
            Items.Add(new ItemClass
            {
                img_path = "hand_with_phone",
                text = "swipe"
            });
            BindingContext = this;
        }

        [Obsolete]
        void LoginWithGoogle_Clicked(object sender, EventArgs args)
        {
            string clientId = null;
            string redirectUri = null;
            //Xamarin.Auth.CustomTabsConfiguration.CustomTabsClosingMessage = null;            

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.GoogleiOSClientId;
                    redirectUri = Constants.GoogleiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.GoogleAndroidClientId;
                    redirectUri = Constants.GoogleAndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.GoogleScope,
                new Uri(Constants.GoogleAuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.GoogleAccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        [Obsolete]
        void LoginWithFacebook_Clicked(object sender, EventArgs args)
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.FacebookiOSClientId;
                    redirectUri = Constants.FacebookiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.FacebookAndroidClientId;
                    redirectUri = Constants.FacebookAndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                Constants.FacebookScope,
                new Uri(Constants.FacebookAuthorizeUrl),
                new Uri(Constants.FacebookAccessTokenUrl),
                null);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
            
        }        

        [Obsolete]
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }


            if (e.IsAuthenticated)
            {
                if (authenticator.AuthorizeUrl.Host == "www.facebook.com")
                {
                    FacebookUser facebookUser = null;

                    var httpClient = new HttpClient();

                    var json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=id,name,first_name,last_name,email,picture.type(large)&access_token=" + e.Account.Properties["access_token"]);

                    facebookUser = JsonConvert.DeserializeObject<FacebookUser>(json);

                    await store.SaveAsync(account = e.Account, Constants.AppName);

                    Application.Current.Properties.Remove("Id");
                    Application.Current.Properties.Remove("FirstName");
                    Application.Current.Properties.Remove("LastName");
                    Application.Current.Properties.Remove("DisplayName");
                    Application.Current.Properties.Remove("EmailAddress");
                    Application.Current.Properties.Remove("ProfilePicture");

                    Application.Current.Properties.Add("Id", facebookUser.Id);
                    Application.Current.Properties.Add("FirstName", facebookUser.First_Name);
                    Application.Current.Properties.Add("LastName", facebookUser.Last_Name);
                    Application.Current.Properties.Add("DisplayName", facebookUser.Name);
                    Application.Current.Properties.Add("EmailAddress", facebookUser.Email);
                    Application.Current.Properties.Add("ProfilePicture", facebookUser.Picture.Data.Url);

                    //await Navigation.PushAsync(new dashboard());
                    await Navigation.PushModalAsync(new dashboard());
                }
                else
                {
                    GoogleUser googleUser = null;

                    // If the user is authenticated, request their basic user data from Google
                    // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                    var request = new OAuth2Request("GET", new Uri(Constants.GoogleUserInfoUrl), null, e.Account);
                    var response = await request.GetResponseAsync();
                    if (response != null)
                    {
                        // Deserialize the data and store it in the account store
                        // The users email address will be used to identify data in SimpleDB
                        string userJson = await response.GetResponseTextAsync();
                        googleUser = JsonConvert.DeserializeObject<GoogleUser>(userJson);
                    }

                    if (account != null)
                    {
                        store.Delete(account, Constants.AppName);
                    }

                    await store.SaveAsync(account = e.Account, Constants.AppName);

                    Application.Current.Properties.Remove("Id");
                    Application.Current.Properties.Remove("FirstName");
                    Application.Current.Properties.Remove("LastName");
                    Application.Current.Properties.Remove("DisplayName");
                    Application.Current.Properties.Remove("EmailAddress");
                    Application.Current.Properties.Remove("ProfilePicture");

                    Application.Current.Properties.Add("Id", googleUser.Id);
                    Application.Current.Properties.Add("FirstName", googleUser.GivenName);
                    Application.Current.Properties.Add("LastName", googleUser.FamilyName);
                    Application.Current.Properties.Add("DisplayName", googleUser.Name);
                    Application.Current.Properties.Add("EmailAddress", googleUser.Email);
                    Application.Current.Properties.Add("ProfilePicture", googleUser.Picture);

                    await Navigation.PushAsync(new dashboard());
                }
            }
        }

        [Obsolete]
        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }

        private async void CreateNewAccountTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new register());
        }
        private async void LoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new login());
        }
        private async void OnBackwardTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new termsofservice());
        }

        public async Task getAPIData()
        {
            try
            {
                
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(30);
                var response = client.GetStringAsync("https://192.168.43.200/flambush/membership/test").Result;
                var Data = JsonConvert.DeserializeObject<respAPI>(response);

                testme.Text = Data.ToString();
               
            }
            catch (Exception exception)
            {
                /* var properties = new Dictionary<string, string> {
                      { "newfirstteam", "RefreshPage" }
                  };
                 Crashes.TrackError(exception, properties);
                   Device.BeginInvokeOnMainThread(async () =>
                   {
                       await DisplayAlert("Error", exception.Message, "OK");
                   });*/
            }
        }
    }
}