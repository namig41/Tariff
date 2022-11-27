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

        async private void Button_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Hot_Water.Text, out hot) && double.TryParse(Cold_Water.Text, out cold) && double.TryParse(Gas_numbers.Text, out gas) && double.TryParse(Electro_numbers.Text, out electro))
            {
                await DisplayAlert("Успешно", "Показания успешно переданы", "OK");
                // await Email.ComposeAsync("guseinovnamig41@gmail.com", "", "Привет");
            }

        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
                await DisplayAlert("Ошибка", "Данные не переданы", "OK");
            }
            catch (Exception ex)
            {
                // Some other exception occurred

                await DisplayAlert("Ошибка", "Данные не переданы", "OK");
            }
        }

    }
}