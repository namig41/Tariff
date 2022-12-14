using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Tariff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public static Person person { get; }

        public readonly static String defaultName = "admin";
        public readonly static String defaultPassword = "1234";
        public readonly static String defaultEmail = "*********";
        public readonly static String defaultPhoneNumber = "*******";
        public readonly static int defaultHotWTarif = 223;
        public readonly static int defaultColdWTarif = 44;
        public readonly static int defaultGasTarif = 6;
        public readonly static int defaultElectricityTarif = 4;

        static RegisterPage()
        {
            person = new Person();

            person.name = defaultName;
            person.password = defaultPassword;
            person.email = defaultEmail;
            person.phoneNumber = defaultPhoneNumber;
            person.hotWater = defaultHotWTarif;
            person.coldWater = defaultColdWTarif;
            person.gas = defaultGasTarif;
            person.electricity = defaultElectricityTarif;
           
        }

        public RegisterPage()
        {
            InitializeComponent();
        }
        
        public static bool NameValid(String name)
        {
            return name == null ? false : name.Length > 5;
        }

        private bool PasswordValid(String password)
        {
            return password == null ? false : password.Length > 5;
        }

        public static bool EmailValid(String email)
        {
            if (email == null || email.Length <= 5)
            {
                return false;
            }
            email.ToLower();
            string good = "qwertyuiopasdfghjklzxcvbnm1234567890.@_-";
            foreach (char y in email)
            {
                if (!good.Contains(y))
                {
                    return false;
                }
            }
            return true;
        }


        public static bool PhoneValid(String phone)
        {
            if (phone == null || phone.Length != 11)
            {
                return false;
            }

            foreach (char y in phone)
            {
                if (!Char.IsDigit(y))
                {
                    return false;
                }
            }
            return true;
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!NameValid(LabelUsername.Text))
            {
                DisplayAlert("Ошибка", "Длина логина должна быть больше 5 символов", "OK");
            }
            else if (!PhoneValid(LabelPhoneNumber.Text))
            {
                DisplayAlert("Ошибка", "Неверный номер телефона", "OK");
            }
            else if (!EmailValid(LabelEmail.Text))
            {
                DisplayAlert("Ошибка", "Неверный email", "OK");
            }
            else if (!PasswordValid(LabelPassword.Text))
            {
                DisplayAlert("Ошибка", "Длина пароля должна быть больше 5 символов", "OK");
            }
            else
            {
                person.name = LabelUsername.Text;
                person.password = LabelPassword.Text;
                person.email = LabelEmail.Text;
                person.phoneNumber = LabelPhoneNumber.Text;
                saveSettingsInFile();
                Navigation.PopAsync();
            }            
        }

        static public void saveSettingsInFile()
        {
            Preferences.Set("name", person.name);
            Preferences.Set("password", person.password);
            Preferences.Set("email", person.email);
            Preferences.Set("phoneNumber", person.phoneNumber);
            Preferences.Set("HWTarif", person.hotWater);
            Preferences.Set("CWTarif", person.coldWater);
            Preferences.Set("GasTarif", person.gas);
            Preferences.Set("ElectricictyTarif", person.electricity);

        }

        void HWTarif(object sender, EventArgs e)
        {
            string tarifItem = HWTarif_.Items[HWTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 3);
            person.coldWater = int.Parse(tarifItem);
        }
        void CWTarif(object sender, EventArgs e)
        {
            string tarifItem = СWTarif_.Items[СWTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 2);
            person.coldWater = int.Parse(tarifItem);
        }

        void GasTarif(object sender, EventArgs e)
        {
            string tarifItem = GasTarif_.Items[GasTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 4);
            person.gas = int.Parse(tarifItem);
        }
        void ElectroTarif(object sender, EventArgs e)
        {
            string tarifItem = ElectroTarif_.Items[ElectroTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 1);
            person.electricity = int.Parse(tarifItem);
        }

    }
}