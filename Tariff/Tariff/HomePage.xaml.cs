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
            readHistory();

        }

        public void readHistory()
        {
            int hotWaterLast = Preferences.Get("HWLast", 0);
            int coldWaterLast = Preferences.Get("CWLast", 0);
            int gasLast = Preferences.Get("GasLast", 0);
            int electricityLast = Preferences.Get("ElectricityLast", 0);
            string currentData = "По данным на " + DateTime.Today.ToString().Substring(0, 11);

            LastDateUpdateHistory.Text = currentData;
            LabelHotWaterLast.Text = $"Горячая вода: {hotWaterLast}  куб. м";
            LabelColdWaterLast.Text = $"Холодная вода: {coldWaterLast}  куб. м";
            LabelGasLast.Text = $"Газ: {gasLast} куб. м";
            LabelElectricityLast.Text = $"Электричество: {electricityLast} кВт";
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

        public async Task SendSms(string messageText)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { RegisterPage.person.phoneNumber });
                await Sms.ComposeAsync(message);
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
                DisplayAlert("Ошибка", "Неверный email", "OK");
            } 
            else if (!RegisterPage.PhoneValid(LabelPhoneNumber.Text))
            {
                DisplayAlert("Ошибка", "Неверный номер телефона", "OK");
            } 
            else if (!RegisterPage.NameValid(LabelName.Text))
            {
                DisplayAlert("Ошибка", "Длина логина должна быть больше 5 символов", "OK");
            }
            else
            {
                RegisterPage.saveSettingsInFile();
                DisplayAlert("", "Данные успешно изменены", "OK");
            }
        }

        private async void HotWaterClicked(object sender, EventArgs e)
        {
            String hotWater = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров", "Отправить", "Отмена");
            int hotWaterint;
            
            if (int.TryParse(hotWater, out hotWaterint))
            {
                int history = Preferences.Get("HWLast", 0);
                if (hotWaterint > history)
                {
                    int result = (hotWaterint - history) * RegisterPage.person.hotWater;
                    hotWater = result.ToString();
                    Preferences.Set("HWLast", hotWaterint);
                    String hotWaterStringTemplate = $"Горячая вода: {hotWaterint} куб. м";
                    LabelHotWaterLast.Text = hotWaterStringTemplate;
                    hotWaterStringTemplate += $"\nИтоговая сумма: {result} руб.";
                    await SendEmail(hotWaterStringTemplate);
                    await SendSms(hotWaterStringTemplate);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Текущие показатели меньше предыдущих", "OK");
                }
                
            }
            else if (hotWater == null)
            {
                
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверно введены показатели", "OK");
            }

        }
        private async void ColdWaterClicked(object sender, EventArgs e)
        {
            String coldWater = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров", "Отправить", "Отмена");
            int coldWaterint;
            if (int.TryParse(coldWater, out coldWaterint))
            {
                int history = Preferences.Get("CWLast", 0);
                if (coldWaterint > history)
                {
                    int result = (coldWaterint - history) * RegisterPage.person.coldWater;
                    coldWater = result.ToString();
                    Preferences.Set("CWLast", coldWaterint);
                    String coldWaterStringTemplate = $"Холодная вода: {coldWaterint} куб. м";
                    LabelColdWaterLast.Text = coldWaterStringTemplate;
                    coldWaterStringTemplate += $"\nИтоговая сумма: {result} руб.";
                    await SendEmail(coldWaterStringTemplate);
                    await SendSms(coldWaterStringTemplate);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Текущие показатели меньше предыдущих", "OK");
                }
            }
            else if (coldWater == null)
            {

            }
            else
            {
                await DisplayAlert("Ошибка", "Неверно введены показатели", "OK");
            }
        }
        private async void GasClicked(object sender, EventArgs e)
        {
            String gas = await DisplayPromptAsync("Введите показания счетчика", "Кубических метров", "Отправить", "Отмена");
            int gasint;
            if (int.TryParse(gas, out gasint))
            {
                int history = Preferences.Get("GasLast", 0);
                if (gasint > history)
                {
                    int result = (gasint - history) * RegisterPage.person.gas;
                    gas = result.ToString();
                    Preferences.Set("GasLast", gasint);
                    String GasStringTemplate = $"Газ: {gasint} куб. м";
                    LabelGasLast.Text = GasStringTemplate;
                    GasStringTemplate += $"\nИтоговая сумма: {result} руб.";
                    await SendEmail(GasStringTemplate);
                    await SendSms(GasStringTemplate);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Текущие показатели меньше предыдущих", "OK");
                }

            }
            else if (gas == null)
            {

            }
            else
            {
                await DisplayAlert("Ошибка", "Неверно введены показатели", "OK");
            }
        }
        private async void ElectroClicked(object sender, EventArgs e)
        {
            String electricity = await DisplayPromptAsync("Введите показания счетчика", "кВт", "Отправить", "Отмена");
            int electricityint;

            if (int.TryParse(electricity, out electricityint))
            {
                int history = Preferences.Get("ElectricityLast", 0);
                
                if (electricityint > history)
                {
                    int result = (electricityint - history) * RegisterPage.person.electricity;
                    electricity = result.ToString();
                    Preferences.Set("ElectricityLast", result);
                    String ElectoricityStringTemplate = $"Электричество: {electricityint} кВт";
                    LabelElectricityLast.Text = ElectoricityStringTemplate;
                    ElectoricityStringTemplate += $"\nИтоговая сумма: {result} руб.";
                    await SendEmail(ElectoricityStringTemplate);
                    await SendSms(ElectoricityStringTemplate);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Текущие показатели меньше предыдущих", "OK");
                }
            }
            else if (electricity == null)
            {

            }
            else
            {
                await DisplayAlert("Ошибка", "Неверно введены показатели", "OK");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("HWLast", 0);
            Preferences.Set("CWLast", 0);
            Preferences.Set("GasLast", 0);
            Preferences.Set("ElectricityLast", 0);

            LabelHotWaterLast.Text = $"Горячая вода: 0 куб. м.";
            LabelColdWaterLast.Text = $"Холодная вода: 0 куб. м.";
            LabelGasLast.Text = $"Газ: 0 куб. м";
            LabelElectricityLast.Text = $"Электричество: 0 кВт";

        }
    }
}