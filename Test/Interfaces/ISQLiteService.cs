using SQLite;

namespace Test.Interfaces
{
    public interface ISQLiteService
    {
        SQLiteAsyncConnection GetConnection(string databaseName);
    }
}
