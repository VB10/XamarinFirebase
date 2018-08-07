using SQLite;

namespace XamarinFirebaseHA.Helper
{
    public interface ISqliteConnection
    {
        SQLiteConnection GetConnection();
    }
}
