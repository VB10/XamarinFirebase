using System;
using SQLite;
using System.IO;
using XamarinFirebaseHA.Helper;
using System.Diagnostics;
using Xamarin.Forms;
using XamarinFirebaseHA.iOS.Dependency;

[assembly : Dependency(typeof(GetSqliteConnection))]
namespace XamarinFirebaseHA.iOS.Dependency
{
    public class GetSqliteConnection : ISqliteConnection
    {
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
