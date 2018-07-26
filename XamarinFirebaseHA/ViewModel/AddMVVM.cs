using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using XamarinFirebaseHA.Helper;
using static XamarinFirebaseHA.Helper.BaseEnum;

namespace XamarinFirebaseHA.ViewModel
{
    public class AddMVVM : INotifyPropertyChanged
    {
        Page page;
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
                        await page.Navigation.PopAsync();
                        MessagingCenter.Send<AddMVVM, bool>(this, MessageSend.addMvvmRefresh.ToString(), true);
                    }
                    else await page.DisplayAlert("hata", "bir hata ile karşılaşıldı", "okey");
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

                    var chosee = await page.DisplayActionSheet("Seçim yapınız", "Kapat", "Fotoğraf çek", "Galeriden seç");
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
                            await page.DisplayAlert("Hata", "Cihazınızla ilgili bir sorunla karşılaşıldı.Lütfen hatayı bize bildirin", "Tamam");
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
                            await page.DisplayAlert("Hatırlatma", "Herhangi bir fotoğraf seçmediniz.", "Tamam");
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

        public AddMVVM(Page page, Image image)
        {
            firebaseService = new DbFirebase();
            student = new Student();
            this.image = image;
            image.Source = "addImage";
            this.page = page;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
