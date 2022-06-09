using System;
using SQLite;

namespace EmptyPocket.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public float Sum { get; set; }
        public int Number { get; set; }
    }
}
