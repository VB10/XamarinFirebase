using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFirebaseHA.Helper;
using XamarinFirebaseHA.Views;
using static XamarinFirebaseHA.Helper.BaseEnum;

namespace XamarinFirebaseHA.ViewModel
{
    public class ListMVVM : INotifyPropertyChanged
    {
        Page page;
        DbFirebase firebaseService;
        ObservableCollection<Student> _studentList;


        public ObservableCollection<Student> studentList
        {
            get
            {
                return _studentList;
            }

            set
            {
                _studentList = value;
                OnPropertyChanged();
            }
        }

        public ListMVVM(Page page)
        {
            this.page = page;
            firebaseService = new DbFirebase();
            studentList = new ObservableCollection<Student>();

            MessagingCenter.Subscribe<AddMVVM, bool>(this, MessageSend.addMvvmRefresh.ToString(),async (pg, item) =>
            {
                isVisible = true;
                await getlist();
                isVisible = false;

                MessagingCenter.Unsubscribe<AddMVVM, bool>(this, MessageSend.addMvvmRefresh.ToString());
            });
        }

        public ICommand addCommand{
            get {
                return new Command(async () =>
                {
                    await page.Navigation.PushAsync(new AddStudentPage(), true);
                });
            }
        }


        internal async Task onAppering()
        {
            if (studentList.Count == 0)
            {
                isVisible = true;
                await getlist();
                isVisible = false;
            }

        }
        async Task getlist(){
            
            var all_list = await firebaseService.getList();
            if (all_list.Count > 0)
            {
                foreach (var item in all_list)
                {
                    studentList.Add(item);
                }
            }
            //onRefresh = false; 
        }


        #region boolen değişikler
        bool _isVisible;

        public bool isVisible
        {
            get
            {
                return _isVisible;
            }

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
