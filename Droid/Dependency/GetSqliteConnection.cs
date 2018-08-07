using System;
using System.Diagnostics;
using System.IO;
using SQLite;
using Xamarin.Forms;
using XamarinFirebaseHA.Droid.Dependency;
using XamarinFirebaseHA.Helper;

[assembly: Dependency(typeof(GetSqliteConnection))]
namespace XamarinFirebaseHA.Droid.Dependency
{
    public class GetSqliteConnection : ISqliteConnection
    {
        public GetSqliteConnection()
        {
        }

        public SQLiteConnection GetConnection()
        {
            try
            {
                var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(docPath, App.DB_NAME);
                var connection = new SQLiteConnection(path);

                return connection;

            }
            catch (Exception ex)
            {
                Debug.Write(ex + "Error sqlite ");
                return null;
            }
        }
    }
}
