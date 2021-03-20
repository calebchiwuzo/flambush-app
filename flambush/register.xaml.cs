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
    public partial class register : ContentPage
    {
        public register()
        {
            InitializeComponent();
        }
        [Obsolete]
        private async void OnBackwardTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new howitworks());
        }
        private async void LoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new login());
        }
    }
}