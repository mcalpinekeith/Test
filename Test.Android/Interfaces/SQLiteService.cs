using System;
using SQLite;
using Xamarin.Forms;
using Test.Droid.Interfaces;
using System.IO;
using Test.Interfaces;

[assembly: Dependency(typeof(SQLiteService))]

namespace Test.Droid.Interfaces
{
    public class SQLiteService : ISQLiteService
    {
        public SQLiteAsyncConnection GetConnection(string databaseName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, databaseName);

            return new SQLiteAsyncConnection(path);
        }
    }
}