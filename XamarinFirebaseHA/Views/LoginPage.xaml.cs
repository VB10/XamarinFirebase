using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFirebaseHA.ViewModel;

namespace XamarinFirebaseHA.Views
{
    public partial class LoginPage : ContentPage
    {
        LoginMVVM loginMVVM;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = loginMVVM = new LoginMVVM(this);
        }
    }
}
