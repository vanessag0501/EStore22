using System;
using Library.Standard.EStore.Utility;
using Newtonsoft.Json;
namespace Library.EStore.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public partial class ProductByQuantity : Product
    {

        public double Quantity { get; set; }

        public double totalPrice
        {
            get
            {
                return price * Quantity;
            }
        }


        public ProductByQuantity()
        {
        }

        public override string ToString()
        {
            return $"{Id} :: {name} :: {description} :: ${price} ::" +
                $" {Quantity} :: ${totalPrice} ";
        }
    }
}


