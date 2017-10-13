using System;
using System.Collections.Generic;
using Plugin.Media;
using Xamarin.Forms;

namespace XamarinFirebaseHA.Views
{
    public partial class XamarinFirebaseStroage : ContentPage
    {
        DbFirebase dbfire = new DbFirebase();
        public XamarinFirebaseStroage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {

            await CrossMedia.Current.Initialize();

            var imgData = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions());

            await dbfire.saveImage(imgData.GetStream());
            _img.Source = ImageSource.FromStream(imgData.GetStream);

        }
    }
}
