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

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (RegisterPage.person == null)
            {
                DisplayAlert("Oops...", "Создайте пользователя", "OK");
                return;
            }

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