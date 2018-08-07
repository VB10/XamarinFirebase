using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFirebaseHA.Helper;
using XamarinFirebaseHA.Views;

namespace XamarinFirebaseHA.ViewModel
{
    public class LoginMVVM : BaseViewModel
    {
        DbFirebase service;
        public LoginMVVM(Page page):base(page)
        {
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
                        var user = await App.firebaseAuthProvier.SignInWithEmailAndPasswordAsync(userName.ToLower().Trim() + "@hwa.com", password.Trim());
                        UserLocalData.userToken = user.FirebaseToken;
                        //TODO UserLocalData.userName = username.trim().base64
                        Navigation.PushModalAsync(new NavigationPage(new StudentListPage()), true);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("err {0}", ex.Message);
                    }
                });
            }
        }

        public async Task onAppering(){
           await service.authUser();
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


    }
}

