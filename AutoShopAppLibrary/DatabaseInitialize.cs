using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace AutoShopAppLibrary
{
    public class DatabaseInitialize
    {
        private string BaseDBDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string DatabaseName { get; set; }
        private Type[] tables { get; set; } =
           [
            typeof(Data.Car),
               typeof(Data.Customer),
               typeof(Data.Workorder)];

        public DatabaseInitialize(string dbname = "FNGAutoDB")
        {
            DatabaseName = dbname;
        }

        public async Task<SQLiteAsyncConnection> InitializeLocalDatabase()
        {
            var database = GetConnection();

            await MakeDB(database);

            return database;
        }

        public SQLiteAsyncConnection GetConnection()
        {
            if (!Directory.Exists(BaseDBDirectory)) Directory.CreateDirectory(BaseDBDirectory);

            var database = new SQLiteAsyncConnection(Path.Combine(BaseDBDirectory, DatabaseName));

            return database;
        }

        public async Task<SQLiteAsyncConnection> RestartDatabase()
        {
            var database = GetConnection();
            await MakeDB(database);
            foreach (var type in tables)
            {
                await database.DeleteAllAsync(new SQLite.TableMapping(type));
            }
            return database;
        }

        public async Task MakeDB(SQLiteAsyncConnection db)
        {
            await db.CreateTablesAsync(CreateFlags.None, tables);
        }
    }
}