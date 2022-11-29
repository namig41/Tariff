using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using System.Net.Mail;

namespace Tariff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();

            LabelName.Text = RegisterPage.person.name;
            LabelEmail.Text = RegisterPage.person.email;
            LabelPhoneNumber.Text = RegisterPage.person.phoneNumber;
        }

        double hot;
        double cold;
        double gas;
        double electro;

        async private void Button_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(Hot_Water.Text, out hot) && double.TryParse(Cold_Water.Text, out cold) && double.TryParse(Gas_numbers.Text, out gas) && double.TryParse(Electro_numbers.Text, out electro))
            {
                String templateForEmail = $"Показания\nГорячая вода: {Hot_Water.Text}\nХолодная вода: {Cold_Water.Text}\nГаз: {Gas_numbers.Text}\nЭлектричество: {Electro_numbers.Text}";
                await SendEmail(templateForEmail);
                appendNewEvent(Hot_Water.Text, Cold_Water.Text, Gas_numbers.Text, Electro_numbers.Text);
            }

        }

        public async Task SendEmail(String emailBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("guseinovnamig41@gmail.com");
                mail.To.Add(RegisterPage.person.email);
                mail.Subject = "Показания";
                mail.Body = emailBody;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("guseinovnamig41@gmail.com", "carqhtndzfopylto");

                SmtpServer.Send(mail);

                await DisplayAlert("Успешно", "Показания успешно переданы", "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            RegisterPage.person.name = LabelName.Text;
            RegisterPage.person.email = LabelEmail.Text;
            RegisterPage.person.phoneNumber = LabelPhoneNumber.Text;

            DisplayAlert("", "Данные успешно изменены", "OK");
        }

        void appendNewEvent(String numberOfHotWater, String numberOfColdWater, String numberOfGas, String numberOfElectricity)
        {
            Frame frame = new Frame
            {
                BackgroundColor = Color.Black,
                CornerRadius = 20
            };

            StackLayout newEvent = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = $"Горячая вода: {numberOfHotWater}",
                        TextColor = Color.White,
                    },
                     new Label
                    {
                        Text = $"Холодная вода: {numberOfColdWater}",
                        TextColor = Color.White,
                    },
                    new Label
                    {
                        Text = $"Газ: {numberOfGas}",
                        TextColor = Color.White,
                    },
                    new Label
                    {
                        Text = $"Электричество: {numberOfElectricity}",
                        TextColor = Color.White,
                    },
                }
            };

            frame.Content = newEvent;
            StackHistory.Children.Add(frame);
        }
    }
}