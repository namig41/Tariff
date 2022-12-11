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
            RegisterPage.person.hotWater = Preferences.Get("HWTarif", RegisterPage.defaultHotWTarif);
            RegisterPage.person.coldWater = Preferences.Get("CWTarif", RegisterPage.defaultColdWTarif);
            RegisterPage.person.gas = Preferences.Get("GasTarif", RegisterPage.defaultGasTarif);
            RegisterPage.person.electricity = Preferences.Get("ElectricityTarif", RegisterPage.defaultElectricityTarif);
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