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

        public async Task SendSms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
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
                    new Label
                    {
                        Text = DateTime.Now.ToString(),
                        TextColor = Color.White,
                        FontSize = 20
                    }
                }
            };

            frame.Content = newEvent;
            StackHistory.Children.Add(frame);
        }

        private async void HotWaterClicked(object sender, EventArgs e)
        {
            String hotWater = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров", "Отправить", "Отмена");
            double hotWaterDouble;
            if (Double.TryParse(hotWater, out hotWaterDouble))
            {
                double result = hotWaterDouble * RegisterPage.person.hotWater;
                hotWater = result.ToString();
            }

            String hotWaterStringTemplate = $"Горячая вода: {hotWater} руб. за куб. м.";
            await SendEmail(hotWaterStringTemplate);
            await SendSms(hotWaterStringTemplate, RegisterPage.person.phoneNumber);
            appendNewEvent(hotWaterStringTemplate);

        }
        private async void ColdWaterClicked(object sender, EventArgs e)
        {
            String coldWater = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров", "Отправить", "Отмена");
            double coldWaterDouble;
            if (Double.TryParse(coldWater, out coldWaterDouble))
            {
                double result = coldWaterDouble * RegisterPage.person.coldWater;
                coldWater = result.ToString();
                String coldWaterStringTemplate = $"Холодная вода: {coldWater} руб. за куб. м.";
                await SendEmail(coldWaterStringTemplate);
                await SendSms(coldWaterStringTemplate, RegisterPage.person.phoneNumber);
                appendNewEvent(coldWaterStringTemplate);
            }
        }
        private async void GasClicked(object sender, EventArgs e)
        {
            String gas = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров", "Отправить", "Отмена");
            double gasDouble;
            if (Double.TryParse(gas, out gasDouble))
            {
                double result = gasDouble * RegisterPage.person.gas;
                gas = result.ToString();
                String gasStringTemplate = $"Газ: {gas} руб. за 1000 куб. м.";
                await SendEmail(gasStringTemplate);
                await SendSms(gasStringTemplate, RegisterPage.person.phoneNumber);
                appendNewEvent(gasStringTemplate);
            }
        }
        private async void ElectroClicked(object sender, EventArgs e)
        {
            String electricity = await DisplayPromptAsync("Введите показания счетчика", "кВт", "Отправить", "Отмена");
            double electricityDouble;

            if (Double.TryParse(electricity, out electricityDouble))
            {
                double result = electricityDouble * RegisterPage.person.gas;
                electricity = result.ToString();
                String electricityStringTemplate = $"Электричество: {electricity} руб. за кВт";
                await SendEmail(electricityStringTemplate);
                await SendSms(electricityStringTemplate, RegisterPage.person.phoneNumber);
                appendNewEvent(electricityStringTemplate);
            }   
        }
    }
}