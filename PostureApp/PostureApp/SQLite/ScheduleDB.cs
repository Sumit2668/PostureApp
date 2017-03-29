using System.Collections.Generic;
using System.Threading.Tasks;
using PostureApp.Model;
using SQLite;

namespace PostureApp.SQLite
{
    public class ScheduleDB
    {
        readonly SQLiteAsyncConnection database;

        public ScheduleDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ScheduleTbl>().Wait();
        }

        public Task<ScheduleTbl> GetItemsAsync()
        {
            return database.Table<ScheduleTbl>().FirstOrDefaultAsync();
        }

        //public Task<List<ScheduleTbl>> GetItemsNotDoneAsync()
        //{
        //    return database.QueryAsync<ScheduleTbl>("SELECT * FROM [ScheduleTbl] WHERE [Done] = 0");
        //}

        public Task<ScheduleTbl> GetItemAsync(int id)
        {
            return database.Table<ScheduleTbl>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync1(ScheduleTbl item)
        {
            if (item.Id > 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
            
        }

        public Task<int> DeleteItemAsync(ScheduleTbl item)
        {
            return database.DeleteAsync(item);
        }
    }
}