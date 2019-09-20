using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;

namespace MyLastHope
{

    [Table("Candy")]
    public class Test
    {
       
       
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }


    }

    

}

