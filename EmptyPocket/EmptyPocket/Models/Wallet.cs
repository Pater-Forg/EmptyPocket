using System;
using SQLite;

namespace EmptyPocket.Models
{
    public class Wallet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Sum { get; set; }
    }
}
