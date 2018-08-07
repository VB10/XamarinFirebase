using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using XamarinFirebaseHA.Helper;


namespace XamarinFirebaseHA.Database
{
    public class SqliteManager : ISqliteManager
    {
        SQLiteConnection connection;
        public SqliteManager()
        {
            connection = DependencyService.Get<ISqliteConnection>().GetConnection();
        }

        public int Count<T>() where T : new()
        {
            return connection.Table<T>().Count();
        }

        public void CreateTable<T>()
        {
            connection.CreateTable<T>();
        }

        public void Delete<T>(T data)
        {
            connection.Delete(data);
        }

        public List<T> GetAll<T>() where T : new()
        {

            var allData = connection.Table<T>().ToList();

            return allData;


        }

        public void Insert<T>(T data)
        {
            connection.Insert(data);
        }

        public void Update<T>(T data)
        {
            connection.Update(data);
        }
    }
}
