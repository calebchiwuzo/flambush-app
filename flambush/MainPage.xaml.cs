using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace flambush
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void letsGetStarted_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new termsofservice());
          //  await Navigation.PushModalAsync(new NavigationPage(new termsofservice()));
        }
    }
}
