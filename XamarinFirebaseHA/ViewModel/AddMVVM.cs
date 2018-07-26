using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using static XamarinFirebaseHA.Helper.BaseEnum;

namespace XamarinFirebaseHA.ViewModel
{
    public class AddMVVM : BaseViewModel
    {
        DbFirebase firebaseService;
        Image image;
        Stream imgSource;

        Student _student;

        public Student student
        {
            get
            {
                return _student;
            }

            set
            {
                _student = value;
                OnPropertyChanged();

            }
        }
        public async Task onAppering()
        {
            await firebaseService.authUser();
        }
        public ICommand saveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    isLoading = true;
                    if(imgSource != null){
                        string url = await firebaseService.saveImage(imgSource);
                        if (!string.IsNullOrEmpty(url)) student.image = url;

                    }

                    if (await firebaseService.saveUser(student))
                    {
                        await Navigation.PopAsync(true);
                        MessagingCenter.Send<AddMVVM, bool>(this, MessageSend.addMvvmRefresh.ToString(), true);
                    }
                    else errorAlert("bir hata ile karşılaşıldı"); 
                    isLoading = false;

                });
            }
        }

        public ICommand selectedImageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CrossMedia.Current.Initialize();

                    var chosee = await actionSheet("Fotoğraf çek", "Galeriden seç");
                    if (chosee == "Fotoğraf çek")
                    {
                        try
                        {
                            var img = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                            {
                                AllowCropping = true,
                                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                                CompressionQuality = 50
                            });
                            imgSource = img.GetStream();
                            image.Source = ImageSource.FromStream(() =>
                            {
                                return img.GetStream();
                            });

                        }
                        catch (Exception)
                        {
                            errorAlert("Cihazınızla ilgili bir sorunla karşılaşıldı.Lütfen hatayı bize bildirin");
                            return;
                        }

                    }
                    else if (chosee == "Galeriden seç")
                    {
                        var img = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                        {
                            CompressionQuality = 50
                        });
                        if (img == null)
                        {
                            errorAlert("Herhangi bir fotoğraf seçmediniz.");
                            return;
                        }
                        imgSource = img.GetStream();

                        image.Source = ImageSource.FromStream(() =>
                        {
                            return img.GetStream();
                        });
                    }
                });
            }
        }

        public AddMVVM(Page page, Image image) : base(page)
        {
            firebaseService = new DbFirebase();
            student = new Student();
            this.image = image;
            image.Source = "addImage";
        }
        bool _isLoading;

        public bool isLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

     
    }
}
