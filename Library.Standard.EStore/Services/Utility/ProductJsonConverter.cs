using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.EStore.Models;
using Newtonsoft.Json.Linq;

namespace Library.Standard.EStore.Utility
{
    public class ProductJsonConverter : JsonCreationConverter<Product>
    {
        protected override Product Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["Product By Quantity"] != null || jObject["product by quantity"] != null)
            {
                return new ProductByQuantity();
            }
            else if (jObject["ProductByWeight"] != null || jObject["product by weight"] != null)
            {
                return new ProductByWeight();
            }
            else
            {
                return new Product();
            }
        }
    }
}
