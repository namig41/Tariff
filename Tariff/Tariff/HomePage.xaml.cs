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
using static System.Net.Mime.MediaTypeNames;

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
            HWTarif_.SelectedIndex = Preferences.Get("HW_picker", 0);
            СWTarif_.SelectedIndex = Preferences.Get("CW_picker", 0);
            GasTarif_.SelectedIndex = Preferences.Get("Gas_picker", 0);
            ElectroTarif_.SelectedIndex = Preferences.Get("Electro_picker", 0);
            readHistory();

        }

        public void readHistory()
        {
            double hotWaterLast = Preferences.Get("HW", 0.0);
            double coldWaterLast = Preferences.Get("CW", 0.0);
            double gasLast = Preferences.Get("GAS", 0.0);
            double electricityLast = Preferences.Get("Electro", 0.0);
            string Text = "По данным на " + DateTime.Today.ToString().Substring(0, 11) + " последние показания:";
            appendDate(Text);
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
                mail.To.Add(Preferences.Get("email", ""));
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
            if (!RegisterPage.EmailValid(LabelEmail.Text))
            {
                DisplayAlert("", "Введите валидный Email", "OK");
            } else if (!RegisterPage.PhoneValid(LabelPhoneNumber.Text))
            {
                DisplayAlert("", "Введите настоящий номер телефона", "OK");
            } else if (!RegisterPage.NameValid(LabelName.Text))
            {
                DisplayAlert("", "Длина логина должна быть больше 5 символов", "OK");
            }
            else
            {
                Preferences.Set("name", LabelName.Text);
                Preferences.Set("email", LabelEmail.Text);
                Preferences.Set("phoneNumber", LabelPhoneNumber.Text);

                string hwZamena = HWTarif_.Items[HWTarif_.SelectedIndex].Substring(0, 5);
                string cwZamena = СWTarif_.Items[СWTarif_.SelectedIndex].Substring(0, 5);
                string gasZamena = GasTarif_.Items[GasTarif_.SelectedIndex].Substring(0, 7);
                string electroZamena = ElectroTarif_.Items[ElectroTarif_.SelectedIndex].Substring(0, 4);


                try
                {
                    Preferences.Set("HWTarif", double.Parse(hwZamena));
                    Preferences.Set("CWTarif", double.Parse(cwZamena));
                    Preferences.Set("GasTarif", double.Parse(gasZamena));
                    Preferences.Set("ElectroTarif", double.Parse(electroZamena));
                }
                catch
                {
                    hwZamena = hwZamena.Replace('.', ',');
                    cwZamena = cwZamena.Replace('.', ',');
                    gasZamena = gasZamena.Replace('.', ',');
                    electroZamena = electroZamena.Replace('.', ',');
                    Preferences.Set("HWTarif", double.Parse(hwZamena));
                    Preferences.Set("CWTarif", double.Parse(cwZamena));
                    Preferences.Set("GasTarif", double.Parse(gasZamena));
                    Preferences.Set("ElectroTarif", double.Parse(electroZamena));

                }
                Preferences.Set("HW_picker", HWTarif_.SelectedIndex);
                Preferences.Set("CW_picker", СWTarif_.SelectedIndex);
                Preferences.Set("Gas_picker", GasTarif_.SelectedIndex);
                Preferences.Set("Electro_picker", ElectroTarif_.SelectedIndex);
                DisplayAlert("", "Данные успешно изменены", "OK");
            }


            
        }


        public void appendDate(String textBody)
        {
            Frame frame = new Frame
            {
                BackgroundColor = Color.Transparent,
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
                        FontSize = 25,
                    }
                }
            };

            frame.Content = newEvent;
            StackHistory.Children.Add(frame);
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
                    double result = (hotWaterDouble - history) * Preferences.Get("HWTarif", 0.0);
                    hotWater = result.ToString();
                    Preferences.Set("HW", hotWaterDouble);
                    String hotWaterStringTemplate = $"Горячая вода: {hotWater} руб.";
                    await SendEmail(hotWaterStringTemplate);
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
                    double result = (coldWaterDouble - history) * Preferences.Get("CWTarif", 0.0);
                    coldWater = result.ToString();
                    Preferences.Set("CW", coldWaterDouble);
                    String coldWaterStringTemplate = $"Холодная вода: {coldWater} руб.";
                    await SendEmail(coldWaterStringTemplate);
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
                    double result = (gasDouble - history) * Preferences.Get("GasTarif", 0.0);
                    gas = result.ToString();
                    Preferences.Set("GAS", gasDouble);
                    String GASStringTemplate = $"Газ: {gas} руб.";
                    await SendEmail(GASStringTemplate);
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
                    double result = (electricityDouble - history) * Preferences.Get("ElectroTarif", 0.0);
                    electricity = result.ToString();
                    Preferences.Set("Electro", electricityDouble);
                    String ElectroStringTemplate = $"Электричество: {electricity} руб.";
                    await SendEmail(ElectroStringTemplate);
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