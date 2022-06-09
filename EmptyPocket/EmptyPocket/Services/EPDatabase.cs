using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using EmptyPocket.Models;

namespace EmptyPocket.Services
{
    public class EPDatabase
    {
        public SQLiteAsyncConnection database;

        public EPDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            database.DropTableAsync<Transaction>().Wait();
            database.DropTableAsync<Category>().Wait();
            database.DropTableAsync<Wallet>().Wait();

            database.CreateTableAsync<Transaction>().Wait();
            database.CreateTableAsync<Category>().Wait();
            database.CreateTableAsync<Wallet>().Wait();
        }
    }
}
