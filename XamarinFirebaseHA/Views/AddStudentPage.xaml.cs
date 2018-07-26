using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFirebaseHA.ViewModel;

namespace XamarinFirebaseHA.Views
{
    public partial class AddStudentPage : ContentPage
    {
        AddMVVM addMVVM;
        public AddStudentPage()
        {
            InitializeComponent();
            BindingContext = addMVVM = new AddMVVM(this,_image);
        }
        async protected override void OnAppearing()
        {
            base.OnAppearing();
            await addMVVM.onAppering();
        }
    }
}
