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
        public readonly static String defaultEmail = "guseinovnamig41@gmail.com";
        public readonly static String defaultPhoneNumber = "+7 (985) 355-79-73";
        public readonly static double defaultHotWTarif = 223.04;
        public readonly static double defaultColdWTarif = 44.97;
        public readonly static double defaultGasTarif = 6.37;
        public readonly static double defaultElectricityTarif = 5.92;

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
            if (name.Length > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PasswordValid(String password)
        {
            if (password.Length > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool EmailValid(String email)
        {
            if (email.Length > 5)
            {
                email.ToLower();
                int a = email.IndexOf('@');
                int b = email.IndexOf('.');
                string good = "qwertyuiopasdfghjklzxcvbnm1234567890.@_-";
                if (a == -1 ^ b == -1)
                {
                    return false;
                }
                if (b < a)
                {
                    return false;
                }
                foreach (char y in email)
                {
                    if (!good.Contains(y))
                    {
                        return false;
                    }
                }
                return true;

            }
            else
            {
                return false;
            }
        }


        public static bool PhoneValid(String phone)
        {
            string good = "0123456789";
            if (phone.Length == 11)
            {
                foreach (char y in phone)
                {
                    if (!good.Contains(y))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!NameValid(LabelUsername.Text))
            {
                DisplayAlert("", "Длина логина должна быть больше 5 символов", "OK");
            }else if (!PasswordValid(LabelPassword.Text))
            {
                DisplayAlert("", "Длина пароля должна быть больше 5 символов", "OK");
            }
            else if (!EmailValid(LabelEmail.Text))
            {
                DisplayAlert("", "Введите валидный Email", "OK");
            }
            else if (!PhoneValid(LabelPhoneNumber.Text))
            {
                DisplayAlert("", "Введите настоящий номер телефона", "OK");
            }
            else
            {
                //person.name = LabelUsername.Text;
                //person.password = LabelPassword.Text;
                //person.email = LabelEmail.Text;
                //person.phoneNumber = LabelPhoneNumber.Text;
                saveSettingsInFile();
                Navigation.PopAsync();
            }            
        }

        public void saveSettingsInFile()
        {
            Preferences.Set("name", person.name);
            Preferences.Set("password", person.password);
            Preferences.Set("email", person.email);
            Preferences.Set("phoneNumber", person.phoneNumber);
            Preferences.Set("HW_picker", HWTarif_.SelectedIndex);
            Preferences.Set("CW_picker", СWTarif_.SelectedIndex);
            Preferences.Set("Gas_picker", GasTarif_.SelectedIndex);
            Preferences.Set("Electro_picker", ElectroTarif_.SelectedIndex);

        }

        void HWTarif(object sender, EventArgs e)
        {
            string tarifItem = HWTarif_.Items[HWTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 5);
            try
            {
                Preferences.Set("HWTarif", Double.Parse(tarifItem));
            }
            catch 
            {
                tarifItem = tarifItem.Replace('.', ',');
                Preferences.Set("HWTarif", Double.Parse(tarifItem));
            }
           

        }
        void CWTarif(object sender, EventArgs e)
        {
            string tarifItem = СWTarif_.Items[СWTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 5);
            try
            {
                Preferences.Set("CWTarif", Double.Parse(tarifItem));
            }
            catch
            {
                tarifItem = tarifItem.Replace('.', ',');
                Preferences.Set("CWTarif", Double.Parse(tarifItem));
            }
        }
        void GasTarif(object sender, EventArgs e)
        {
            string tarifItem = GasTarif_.Items[GasTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 7);
            try
            {
                Preferences.Set("GasTarif", Double.Parse(tarifItem));
            }
            catch
            {
                tarifItem = tarifItem.Replace('.', ',');
                Preferences.Set("GasTarif", Double.Parse(tarifItem));

            }

        }
        void ElectroTarif(object sender, EventArgs e)
        {
            string tarifItem = ElectroTarif_.Items[ElectroTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 4);
            try
            {
                Preferences.Set("ElectroTarif", Double.Parse(tarifItem));
            }
            catch
            {
                tarifItem = tarifItem.Replace('.', ',');
                Preferences.Set("ElectroTarif", Double.Parse(tarifItem));
            }
        }

    }
}