using System;
using Library.Standard.EStore.Utility;
using Newtonsoft.Json;
namespace Library.EStore.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public partial class ProductByWeight : Product
    {

        public double Weight
        {
            get;
            set;

        }

        public double totalPrice
        {
            get
            {
                return price * Weight;
            }
        }


        public ProductByWeight()
        {
        }

        public override string ToString()
        {
            return $"{Id} :: {name} :: {description} :: ${price} :: {Weight} LB :: ${totalPrice} ";
        }
    }
}
