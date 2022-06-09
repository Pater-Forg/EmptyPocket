using System;
using SQLite;

namespace EmptyPocket.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Category { get; set; }
        public string Account { get; set; }
        public string Place { get; set; }
        public string Comment { get; set; }
        public string Currency { get; set; }
        public float Sum { get; set; }
        public DateTime Date { get; set; }

    }
}