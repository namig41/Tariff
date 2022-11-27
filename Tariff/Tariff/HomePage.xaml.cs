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
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        double hot;
        double cold;
        double gas;
        double electro;

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Hot_Water.Text, out hot) && double.TryParse(Cold_Water.Text, out cold) && double.TryParse(Gas_numbers.Text, out gas) && double.TryParse(Electro_numbers.Text, out electro))
            {
                DisplayAlert("Успешно", "Показания переданы", "Ok");
                Navigation.PushModalAsync(new HomePage());
            }
            else
            {
                DisplayAlert("Oops...", "Показания должны быть числами", "OK");
            }

        }



    }
}