using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tariff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotWater : ContentPage
    {
        public HotWater()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            String templateForEmail = $"Показания\nГорячая вода: {Hot_Water.Text}";
            await HomePage.SendEmail(templateForEmail);
        }
    }
}