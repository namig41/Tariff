using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Tariff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public static Person person { get; }

        readonly static String defaultName = "admin";
        readonly static String defaultPassword = "1234";
        readonly static String defaultEmail = "guseinovnamig41@gmail.com";
        readonly static String defaultPhoneNumber = "+7 (985) 355-79-73";
        readonly static double defaultHotWTarif = 223.04;
        readonly static double defaultColdWTarif = 44.97;
        readonly static double defaultGasTarif = 6.37;
        readonly static double defaultElectricityTarif = 5.92;


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
            Navigation.PopAsync();
        }

        void HWTarif(object sender, EventArgs e)
        {
            string tarifItem = HWTarif_.Items[HWTarif_.SelectedIndex];
            person.hotWater = Double.Parse(tarifItem);
        }
        void CWTarif(object sender, EventArgs e)
        {
            string tarifItem = СWTarif_.Items[СWTarif_.SelectedIndex];
            person.coldWater = Double.Parse(tarifItem);
        }
        void GasTarif(object sender, EventArgs e)
        {
            string tarifItem = GasTarif_.Items[GasTarif_.SelectedIndex];
            person.gas = Double.Parse(tarifItem);
        }
        void ElectroTarif(object sender, EventArgs e)
        {
            string tarifItem = ElectroTarif_.Items[ElectroTarif_.SelectedIndex];
            person.electricity = Double.Parse(tarifItem);
        }

    }
}