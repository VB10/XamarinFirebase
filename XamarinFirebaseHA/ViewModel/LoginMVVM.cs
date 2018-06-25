using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFirebaseHA.Helper;
using XamarinFirebaseHA.Views;

namespace XamarinFirebaseHA.ViewModel
{
    public class LoginMVVM : INotifyPropertyChanged
    {
        Page page;
        DbFirebase service;
        public LoginMVVM(Page page)
        {
            this.page = page;
            service = new DbFirebase();
        }

        //TODO user sign in 
        public ICommand checkUserClick
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var user = await service.firebaseAuthProvier.SignInWithEmailAndPasswordAsync(userName.Trim() + "@hwa.com", password.Trim());
                        UserLocalData.userToken = user.FirebaseToken;
                        await page.Navigation.PushModalAsync(new NavigationPage(new StudentListPage()), true);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("err {0}", ex.Message);
                    }
                });
            }
        }


        string _userName;

        public string userName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        string _password;

        public string password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
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

