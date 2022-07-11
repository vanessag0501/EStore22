using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.EStore.Models;
using Newtonsoft.Json;

namespace Library.EStore.Services
{
	public class CartService
	{
		private List<Product> cartList;

		public List<Product> Cart
		{
			get
			{
				return cartList;
			}
		}


		public int NextId
		{
			get
			{
				if (!Cart.Any())
				{
					return 1;
				}

				return Cart.Select(t => t.Id).Max() + 1;
			}
		}
		private static CartService current;

		public static CartService Current
		{
			get
			{
				if (current == null)
				{
					current = new CartService();
				}

				return current;
			}

		}

		private CartService()
		{

			cartList = new List<Product>();

		}

		public void Create(Product product)
		{
			product.Id = NextId;
			Cart.Add(product);

			var productService = ProductService.Current;

			//productService.Delete(id);

		}

		public void Update(Product product)
		{

		}

		public void Delete(double id)
		{
			var cartDelete = cartList.FirstOrDefault(t => t.Id == id);

			if (cartDelete == null)
			{
				return;
			}

			cartList.Remove(cartDelete);
		}

		public void Save(string fileName)
		{
			var productsJson = JsonConvert.SerializeObject(cartList);

			File.WriteAllText(fileName, productsJson);


		}

		public void Load(string fileName)
		{

			var productsJson = File.ReadAllText(fileName);

			cartList = JsonConvert.DeserializeObject<List<Product>>
				(productsJson) ?? new List<Product>();
		}
	}
}




