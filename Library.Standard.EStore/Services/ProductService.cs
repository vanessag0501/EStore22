using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.EStore.Models;
using Newtonsoft.Json;

namespace Library.EStore.Services
{
	public class ProductService
	{

		private string persistPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\SaveData.json";


		private List<Product> productsList;

		public List<Product> Products
		{
			get
			{
				return productsList;
			}
		}


		public int NextId
		{
			get
			{
				if (!Products.Any())
				{
					return 1;
				}

				return Products.Select(t => t.Id).Max() + 1;
			}
		}
		private static ProductService current;

		public static ProductService Current
		{
			get
			{
				if (current == null)
				{
					current = new ProductService();
				}

				return current;
			}

		}

		private ProductService()
		{

			productsList = new List<Product>();


		}

		public void Create(Product product)
		{
			if(product.Id <= 0)
            {
				product.Id = NextId;
				Products.Add(product);

            }
			//product.Id = NextId;
			//Products.Add(product);
		}

		public void Update(Product product)
		{

		}

		public void Delete(double id)
		{
			var productDelete = productsList.FirstOrDefault(t => t.Id == id);

			if (productDelete == null)
			{
				return;
			}

			productsList.Remove(productDelete);
		}

		public void Save(string fileName = null)
		{

			if(string.IsNullOrEmpty(fileName))
            {
				fileName = $"(persistPath)\\SaveData.json";
            }
			else
            {
				fileName = $"(persistPath)\\{fileName}.json";
			} 
			var productsJson = JsonConvert.SerializeObject(productsList,
				new JsonSerializerSettings
				{ TypeNameHandling = TypeNameHandling.All });

			File.WriteAllText(fileName, productsJson);


		}

		public void Load(string fileName = null)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				fileName = $"(persistPath)\\SaveData.json";
			}
			else
			{
				fileName = $"(persistPath)\\{fileName}.json";
			}

			var productsJson = File.ReadAllText(fileName);

			productsList = JsonConvert.DeserializeObject<List<Product>>
				(productsJson, new JsonSerializerSettings
				{ TypeNameHandling = TypeNameHandling.All }) ?? new List<Product>();
		}


	}
}


