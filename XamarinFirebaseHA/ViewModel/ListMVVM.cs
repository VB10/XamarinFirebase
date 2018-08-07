using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFirebaseHA.Views;
using static XamarinFirebaseHA.Helper.BaseEnum;

namespace XamarinFirebaseHA.ViewModel
{
    public class ListMVVM : BaseViewModel
    {
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

        public ListMVVM(Page page) : base(page)
        {
            firebaseService = new DbFirebase();
            studentList = new ObservableCollection<Student>();
            sqliteManager.CreateTable<Student>();

            #region SQLite
            var data = sqliteManager.GetAll<Student>();

            //sqliteManager.Delete(data[0]);

            //sqliteManager.GetAll<Student>();

            ////insert after 
            //var updateData = data[0];
            //updateData.name = "XASA";
            //sqliteManager.Update(updateData);

            //sqliteManager.GetAll<Student>();
            #endregion
    

            MessagingCenter.Subscribe<AddMVVM, bool>(this, MessageSend.addMvvmRefresh.ToString(), async (pg, item) =>
             {
                 isVisible = true;
                 await getlist();
                 isVisible = false;

                 MessagingCenter.Unsubscribe<AddMVVM, bool>(this, MessageSend.addMvvmRefresh.ToString());
             });
        }

        public ICommand addCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PushAsync(new AddStudentPage(), true);
                });
            }
        }

        public ICommand filterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var list = await firebaseService.getListQuery("room", "A", listCount.mid);
                    if (list.Count > 0)
                    {
                        studentList = list;
                    }
                });
            }
        }


        public ICommand refreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    isVisible = true;
                    await getlist();
                    isVisible = false;
                });
            }
        }

        public async Task itemAppering(Student item)
        {

            var count = studentList.IndexOf(item);
            if (studentList.Count - 1 == count)
            {
                isInfinity = true;
                var list = (await firebaseService.getListQuery(item.key, listCount.small));
                isInfinity = false;

                if (list.Last() == studentList.Last()) return;
                else
                {
                    list.RemoveAt(0);
                    foreach (var listItem in list)
                    {
                         studentList.Add(listItem);
                    }
                    studentList = studentList;
                }

            }
        }



            public async Task onAppering()
            {
                if (studentList.Count == 0)
                {
                    isVisible = true;
                    await getlist();
                    isVisible = false;
                }

            }
            async Task getlist()
            {

                var all_list = await firebaseService.getList();

                if (all_list.Count > 0)
                {
                studentList = all_list;

                //sqliteTest
                foreach (var item in studentList)
                {
                    
                    sqliteManager.Insert<Student>(item);
                }
                sqliteManager.Count<Student>();
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
        bool _isInfinity;

        public bool isInfinity
        {
            get
            {
                return _isInfinity;
            }

            set
            {
                _isInfinity = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
