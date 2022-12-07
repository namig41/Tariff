using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Tariff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();

            tryReadSettings();
        }

        public void tryReadSettings()
        {
            RegisterPage.person.name = Preferences.Get("name", RegisterPage.defaultName);
            RegisterPage.person.password = Preferences.Get("password", RegisterPage.defaultPassword);
            RegisterPage.person.email = Preferences.Get("email", RegisterPage.defaultEmail);
            RegisterPage.person.phoneNumber = Preferences.Get("phoneNumber", RegisterPage.defaultPhoneNumber);
            string HW = Preferences.Get("HWTarif", "223.04");
            //RegisterPage.person.hotWater = double.Parse(HW);
            string CW = Preferences.Get("CWTarif", "44.97");
            //RegisterPage.person.coldWater = double.Parse(CW);
            string GAS = Preferences.Get("GasTarif", "6.37");
            //RegisterPage.person.gas = double.Parse(GAS);
            string ElectroTarif = Preferences.Get("ElectroTarif", "5.92");
            //RegisterPage.person.electricity = double.Parse(ElectroTarif);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (LabelUsername.Text == RegisterPage.person.name && LabelPassword.Text == RegisterPage.person.password)
            {
                Navigation.PushModalAsync(new HomePage());
            } 
            else
            {
                DisplayAlert("Oops...", "Неправильный логин или пароль", "OK");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}