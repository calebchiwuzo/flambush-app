using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace flambush
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class termsofservice : ContentPage
    {
        public termsofservice()
        {
            InitializeComponent();
        }
        private async void OnBackwardTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
        private async void OnForwardTapped(object sender, EventArgs e)
        {
            if (cbx_Iagree.IsChecked)
            {
                await Navigation.PushModalAsync(new howitworks());
            }
            else
            {
               await DisplayAlert("Warning", "you must agree to our terms of use before proceeding", "OK");
            }
        }

    }
}