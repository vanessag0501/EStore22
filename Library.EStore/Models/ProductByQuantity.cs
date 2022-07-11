using System;
namespace Library.EStore.Models
{
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


