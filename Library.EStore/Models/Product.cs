using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Library.EStore.Interfaces;

namespace Library.EStore.Models
{
    public class Product
    {

        public string? name { get; set; }
        public string? description { get; set; }
        public double price { get; set; }

        public int Id { get; set; }



        public override string ToString()
        {
            return $"{Id} :: {name} :: {description} :: {price} ::";
        }


    }
}

