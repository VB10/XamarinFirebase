using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinFirebaseHA
{
    public class Student : INotifyPropertyChanged
	{
        string _name;

        public string name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        int _age;

        public int age
        {
            get
            {
                return _age;
            }

            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string image
        {
            get;
            set;
        } = "https://picsum.photos/200/300";
        public string key
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}
