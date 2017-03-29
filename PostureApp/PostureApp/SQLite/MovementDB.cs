using System.Collections.Generic;
using System.Threading.Tasks;
using PostureApp.Model;
using SQLite;

namespace PostureApp.SQLite
{
    public class MovementDB
    {
        readonly SQLiteAsyncConnection database;

        public MovementDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<MovementListTbl>().Wait();
        }

        public Task<List<MovementListTbl>> GetItemsAsync()
        {
            return database.Table<MovementListTbl>().ToListAsync();
        }

        public Task<List<MovementListTbl>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<MovementListTbl>("SELECT * FROM [MovementList] WHERE [Done] = 0");
        }

        public Task<MovementListTbl> GetItemAsync(int id)
        {
            return database.Table<MovementListTbl>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateIcon(MovementListTbl item)
        {
            return database.UpdateAsync(item);
        }

        public Task<int> SaveItemAsync1(MovementListTbl item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
            
        }

        public Task<int> DeleteItemAsync(MovementListTbl item)
        {
            return database.DeleteAsync(item);
        }
    }
}