using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using System.Net.Mail;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;

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
        string hotWater;
        string coldWater;
        string Gas;
        string electricity;


        public static async Task SendEmail(String emailBody)
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

                // await DisplayAlert("Успешно", "Показания успешно переданы", "OK");

            }
            catch (Exception ex)
            {
                // await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            RegisterPage.person.name = LabelName.Text;
            RegisterPage.person.email = LabelEmail.Text;
            RegisterPage.person.phoneNumber = LabelPhoneNumber.Text;

            DisplayAlert("", "Данные успешно изменены", "OK");
        }

        public void appendNewEvent(String textBody)
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
                        Text = textBody,
                        TextColor = Color.White,
                        FontSize = 20,
                    },
                }
            };

            frame.Content = newEvent;
            StackHistory.Children.Add(frame);
        }

        private async void HotWaterClicked(object sender, EventArgs e)
        {
            hotWater = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров");
            
        }
        private async void ColdWaterClicked(object sender, EventArgs e)
        {
            coldWater = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров");
        }
        private async void GasClicked(object sender, EventArgs e)
        {
            Gas = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров");
        }
        private async void ElectroClicked(object sender, EventArgs e)
        {
            electricity = await DisplayPromptAsync("Введите показания счетчика", "кВт");
        }
        async private void Button_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(hotWater, out hot) && double.TryParse(coldWater, out cold) && double.TryParse(Gas, out gas) && double.TryParse(electricity, out electro))
            {
                String templateForEmail = $"Показания\nГорячая вода: {hotWater}\nХолодная вода: {coldWater}\nГаз: {Gas}\nЭлектричество: {electricity}";
                await SendEmail(templateForEmail);
                //appendNewEvent(hotWater, coldWater, Gas, electricity);
            }

        }

    }
}