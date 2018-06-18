using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFirebaseHA.Helper;
using static XamarinFirebaseHA.Helper.BaseEnum;

namespace XamarinFirebaseHA.ViewModel
{
    public class AddMVVM : INotifyPropertyChanged
    {
        Student _student;
        Page page;
        DbFirebase firebaseService;

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
        public ICommand saveCommand{
            get{
                return new Command(async () =>
                {
                    if (await firebaseService.saveUser(student))
                    {
                        await page.Navigation.PopAsync();
                        MessagingCenter.Send<AddMVVM,bool>(this, MessageSend.addMvvmRefresh.ToString(),true);
                    }
                    else await page.DisplayAlert("hata", "bir hata ile karşılaşıldı", "okey");
                });
            }
        }

        public AddMVVM(Page page)
        {
            firebaseService = new DbFirebase();
            student = new Student();
            this.page = page;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
