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

        private void Button_Clicked(object sender, EventArgs e)
        {
            person.name = LabelUsername.Text;
            person.password = LabelPassword.Text;
            person.email = LabelEmail.Text;
            person.phoneNumber = LabelPhoneNumber.Text;
            saveSettingsInFile();
            Navigation.PopAsync();
        }

        public void saveSettingsInFile()
        {
            Preferences.Set("name", person.name);
            Preferences.Set("password", person.password);
            Preferences.Set("email", person.email);
            Preferences.Set("phoneNumber", person.phoneNumber);
        }

        void HWTarif(object sender, EventArgs e)
        {
            string tarifItem = HWTarif_.Items[HWTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 5);
            person.hotWater = Double.Parse(tarifItem);
            Preferences.Set("HWTarif", tarifItem);

        }
        void CWTarif(object sender, EventArgs e)
        {
            string tarifItem = СWTarif_.Items[СWTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 5);
            person.coldWater = Double.Parse(tarifItem);
            Preferences.Set("CWTarif", tarifItem);
        }
        void GasTarif(object sender, EventArgs e)
        {
            string tarifItem = GasTarif_.Items[GasTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 7);
            person.gas = Double.Parse(tarifItem);
            Preferences.Set("GasTarif", tarifItem);

        }
        void ElectroTarif(object sender, EventArgs e)
        {
            string tarifItem = ElectroTarif_.Items[ElectroTarif_.SelectedIndex];
            tarifItem = tarifItem.Substring(0, 4);
            person.electricity = Double.Parse(tarifItem);
            Preferences.Set("ElectroTarif", tarifItem);
        }

    }
}