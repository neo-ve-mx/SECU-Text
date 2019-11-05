using SECU_Text.Droid.Services;
using SECU_Text.Services;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidSQLitePlatform))]

namespace SECU_Text.Droid.Services
{
    public class AndroidSQLitePlatform : ISQLitePlatform
    {
        private string GetPatch()
        {
            var dbName = "SECUTextDB.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return path;
        }
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return new SQLiteAsyncConnection(GetPatch());
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPatch());
        }
    }
}