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
    public partial class LoginPage : ContentPage
    {
        readonly String defaultString = "admin";
        readonly String defaultPassword = "1234";
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (LabelUsername.Text == defaultString && LabelPassword.Text == defaultPassword)
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