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

            readHistory();

        }

        public void readHistory()
        {
            double hotWaterLast = Preferences.Get("HW", 0.0);
            double coldWaterLast = Preferences.Get("CW", 0.0);
            double gasLast = Preferences.Get("GAS", 0.0);
            double electricityLast = Preferences.Get("Electro", 0.0);

            appendNewEvent($"Горячая вода: {hotWaterLast} куб. м.");
            appendNewEvent($"Холодная вода: {coldWaterLast} куб. м.");
            appendNewEvent($"Газ: {gasLast} куб. м.");
            appendNewEvent($"Электричество: {electricityLast} Квт");
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
                double history = Preferences.Get("HW", 0.0);
                if (hotWaterDouble > history)
                {
                    double result = (hotWaterDouble - history) * RegisterPage.person.hotWater;
                    hotWater = result.ToString();
                    Preferences.Set("HW", hotWaterDouble);
                    String hotWaterStringTemplate = $"Горячая вода: {hotWater} руб.";
                    // await SendEmail(hotWaterStringTemplate);
                    await SendSms(hotWaterStringTemplate, RegisterPage.person.phoneNumber);
                    //appendNewEvent(hotWaterStringTemplate);
                }
                else
                {
                    await DisplayAlert("", "Текущие показатели не могут быть меньше предыдущих", "OK");
                }
                
            }
            else if (hotWater == null)
            {
                
            }
            else
            {
                await DisplayAlert("", "Показатели должны бить числами", "OK");
            }

        }
        private async void ColdWaterClicked(object sender, EventArgs e)
        {
            String coldWater = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров", "Отправить", "Отмена");
            double coldWaterDouble;
            if (Double.TryParse(coldWater, out coldWaterDouble))
            {
                double history = Preferences.Get("CW", 0.0);
                if (coldWaterDouble > history)
                {
                    double result = (coldWaterDouble - history) * RegisterPage.person.coldWater;
                    coldWater = result.ToString();
                    Preferences.Set("CW", coldWaterDouble);
                    String coldWaterStringTemplate = $"Холодная вода: {coldWater} руб.";
                    // await SendEmail(hotWaterStringTemplate);
                    await SendSms(coldWaterStringTemplate, RegisterPage.person.phoneNumber);
                    //appendNewEvent(coldWaterStringTemplate);
                }
                else
                {
                    await DisplayAlert("", "Текущие показатели не могут быть меньше предыдущих", "OK");
                }
            }
            else if (coldWater == null)
            {

            }
            else
            {
                await DisplayAlert("", "Показатели должны бить числами", "OK");
            }
        }
        private async void GasClicked(object sender, EventArgs e)
        {
            String gas = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров", "Отправить", "Отмена");
            double gasDouble;
            if (Double.TryParse(gas, out gasDouble))
            {
                double history = Preferences.Get("GAS", 0.0);
                if (gasDouble > history)
                {
                    double result = (gasDouble - history) * RegisterPage.person.gas;
                    gas = result.ToString();
                    Preferences.Set("GAS", gasDouble);
                    String GASStringTemplate = $"Газ: {gas} руб.";
                    // await SendEmail(hotWaterStringTemplate);
                    await SendSms(GASStringTemplate, RegisterPage.person.phoneNumber);
                    //appendNewEvent(GASStringTemplate);

                }
                else
                {
                    await DisplayAlert("", "Текущие показатели не могут быть меньше предыдущих", "OK");
                }

            }
            else if (gas == null)
            {

            }
            else
            {
                await DisplayAlert("", "Показатели должны бить числами", "OK");
            }
        }
        private async void ElectroClicked(object sender, EventArgs e)
        {
            String electricity = await DisplayPromptAsync("Введите показания счетчика", "кВт", "Отправить", "Отмена");
            double electricityDouble;

            if (Double.TryParse(electricity, out electricityDouble))
            {
                double history = Preferences.Get("Electro", 0.0);
                if (electricityDouble > history)
                {
                    double result = (electricityDouble - history) * RegisterPage.person.electricity;
                    electricity = result.ToString();
                    Preferences.Set("Electro", electricityDouble);
                    String ElectroStringTemplate = $"Электричество: {electricity} руб.";
                    // await SendEmail(hotWaterStringTemplate);
                    await SendSms(ElectroStringTemplate, RegisterPage.person.phoneNumber);
                    //appendNewEvent(ElectroStringTemplate);
                }
                else
                {
                    await DisplayAlert("", "Текущие показатели не могут быть меньше предыдущих", "OK");
                }
            }
            else if (electricity == null)
            {

            }
            else
            {
                await DisplayAlert("", "Показатели должны бить числами", "OK");
            }
        }
    }
}