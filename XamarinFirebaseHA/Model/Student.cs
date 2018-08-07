
namespace XamarinFirebaseHA
{
    [SQLite.Table("Student")]

    public class Student
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID
        {
            get;
            set;
        }
        
        public string name
        {
            get;
            set;
        }
        public string age
        {
            get;
            set;
        }
        public string image
        {
            get;
            set;
        }
        public string room
        {
            get;
            set;
        }

        public string key
        {
            get;
            set;
        }


        //public string key
        //{
        //    get;
        //    set;
        //}


    }
}
