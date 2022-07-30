using Library.EStore.Models;

namespace EStoreApplication.API.Database
{
	public static class eStoreDatabase
	{

		public static List<Product> products {
            get
            {
				var returnList = new List<Product>();
				productByQuantities.ForEach(returnList.Add);
				productByWeights.ForEach(returnList.Add);

				return returnList;
			}

		}


		public static List<ProductByQuantity> productByQuantities = new List<ProductByQuantity>
		{
			new ProductByQuantity{name = "Test Product1", description = "Description 1",Id=1},
			new ProductByQuantity{name = "Test Product1", description = "Description 2",Id=2}

		};

		public static List<ProductByWeight> productByWeights = new List<ProductByWeight>
		{
			new ProductByWeight{name ="test product weight", description = "description weight", Id =3}
		};

		public static int NextId
		{
            get{
		
				return products.Select(t => t.Id).Max() + 1;

			}


		}
		private static Dictionary<string, List<Product>> _carts;

		public static Dictionary<string, List<Product>> Carts {
            get
            {
				if(_carts == null)
                {
					_carts = new Dictionary<string, List<Product>>();
                }
				return _carts;
            }

            set
            {
				_carts = value;
            }

		}



	}
}

