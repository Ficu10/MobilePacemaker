using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MobilePacemaker;

namespace MobilePacemaker
{
    public class SQLiteHelper
    {
        private readonly SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<History>();
        }

        public Task<int> CreateHistory (History history)
        {
            return db.InsertAsync(history);
        }
        public Task<List<History>>ReadHistory()
        {
            return db.Table<History>().ToListAsync();
        }

        public Task<int> DeleteHistory(History history)
        {
            return db.DeleteAsync(history);
        }
       
    }
}
