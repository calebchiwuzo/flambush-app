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
    public partial class dashboard : ContentPage
    {
        public IList<ItemClass> Items { get; private set; }

        public class ItemClass
        {
            public string id { get; set; }
            public string trans_date { get; set; }
            public string trans { get; set; }
            public string trans_rate { get; set; }
            public string trans_status { get; set; }
            public bool IsIndicatorVisible { get; set; }
        }
        public dashboard()
        {
            InitializeComponent();

            Items = new List<ItemClass>();
            Items.Add(new ItemClass
            {
                trans_date = "23rd Feb, 2021",
                trans = "0.0002 BTC",
                trans_rate = "N1/0.00000002BTC",
                trans_status = "Completed"
            });

            Items.Add(new ItemClass
            {
                trans_date = "23rd Feb, 2021",
                trans = "0.0002 BTC",
                trans_rate = "N1/0.00000002BTC",
                trans_status = "Completed"
            });
            Items.Add(new ItemClass
            {
                trans_date = "23rd Feb, 2021",
                trans = "0.0002 BTC",
                trans_rate = "N1/0.00000002BTC",
                trans_status = "Completed"
            });
            Items.Add(new ItemClass
            {
                trans_date = "23rd Feb, 2021",
                trans = "0.0002 BTC",
                trans_rate = "N1/0.00000002BTC",
                trans_status = "Completed"
            });
            Items.Add(new ItemClass
            {
                trans_date = "23rd Feb, 2021",
                trans = "0.0002 BTC",
                trans_rate = "N1/0.00000002BTC",
                trans_status = "Completed"
            });
            BindingContext = this;
        }
    }
}