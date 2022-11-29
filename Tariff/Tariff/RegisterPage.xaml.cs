﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tariff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public static Person person { get; }

        readonly static String defaultName = "admin";
        readonly static String defaultPassword = "1234";

        readonly static String defaultEmail = "galligalli16@gmail.com";
        readonly static String defaultPhoneNumber = "+7 (985) 444-84-41";


        static RegisterPage()
        {
            person = new Person();

            person.name = defaultName;
            person.password = defaultPassword;
            person.email = defaultEmail;
            person.phoneNumber = defaultPhoneNumber;
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
    }
}