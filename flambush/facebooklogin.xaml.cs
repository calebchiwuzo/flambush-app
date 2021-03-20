using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flambush
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class facebooklogin : ContentPage
    {
        public class FacebookProfile
        {
            public string Name { get; set; }
            public Picture Picture { get; set; }
            public string Id { get; set; }
        }
        public class Picture
        {
            public Data Data { get; set; }
        }

        public class Data
        {
            public bool IsSilhouette { get; set; }
            public string Url { get; set; }
        }
        public facebooklogin()
        {
            InitializeComponent();
            this.ToolbarItems.Add(new ToolbarItem
            {
                IconImageSource = "icon_back",
                Command = new Command(async () => {
                    await this.Navigation.PopModalAsync();
                })
            });
            facebook_WebView.Source = "https://www.facebook.com/v7.0/dialog/oauth?client_id=473111487402617&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";
            facebook_WebView.Navigated += facebook_Webview_Navigated;
        }
        async void facebook_Webview_Navigated(object sender, WebNavigatedEventArgs e)
        {
            var AccessURL = e.Url;
            if (AccessURL.Contains("access_token"))
            {
                AccessURL = AccessURL.Replace("https://web.facebook.com/connect/login_success.html?_rdc=1&_rdr#access_token=", string.Empty);
                var accesstoken = AccessURL.Split('&')[0];
                HttpClient client = new HttpClient();
                var response = client.GetStringAsync("https://graph.facebook.com/me?fields=email,name,picture&access_token=" + accesstoken).Result;
                var Data = JsonConvert.DeserializeObject<FacebookProfile>(response);

                var name = Data.Name;
                var userID = Data.Id;
                var pixpath = Data.Picture.Data.Url;

                if (!string.IsNullOrEmpty(userID))
                {
                    await Navigation.PushModalAsync(new dashboard());
                }


            }
        }
    }
}