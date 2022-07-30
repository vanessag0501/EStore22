using System;
using Library.EStore.Models;
using EStoreApplication.API.Database;
namespace EStoreApplication.API.Database.EC
{
	public class ProductEC
	{
		public List<ProductByQuantity> Get()
        {
			return eStoreDatabase.productByQuantities;
        }

		public List<ProductByWeight> Get(int id)
		{
			return eStoreDatabase.productByWeights;
		}

		public Product Remove(int id)
        {
			var prodToDelete = eStoreDatabase.products.FirstOrDefault(i => i.Id == id);

			if(prodToDelete != null)
            {
				eStoreDatabase.products.Remove(prodToDelete);
			}

			return prodToDelete ?? new Product();
        }

		public Product Create(Product p)
        {
			//if(p.Id <=0)
   //         {
			//	p.Id = eStoreDatabase.NextId;
			//	eStoreDatabase.products.Add(p);

   //         }

			//else
   //         {
			//	var ItemToReplace = eStoreDatabase.products.FirstOrDefault(i => i.Id == p.Id);

			//	if(ItemToReplace != null)
   //             {
			//		eStoreDatabase.products.Remove(ItemToReplace);
   //             }

			//	eStoreDatabase.products.Add(p);
   //         }

			var returnValue = Filebase.Current.Create(p) as Product;

			if(returnValue == null)
            {
				return new Product();

            }

			return returnValue;
			
        }

        internal static bool Any()
        {
            throw new NotImplementedException();
        }

        internal class Create : ProductByQuantity
        {
            private ProductByQuantity product;

            public Create(ProductByQuantity product)
            {
                this.product = product;
            }
        }
    }
}

