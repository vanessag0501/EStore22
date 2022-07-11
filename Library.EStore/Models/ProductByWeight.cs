using System;
namespace Library.EStore.Models
{
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
