using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.EStore.Models;
using Newtonsoft.Json;
using System.IO;
using Library.Standard.EStore.Utility;

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
                var productsJson = new WebRequestHandler().Get("http://23.25.176.177/Products");
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

			var productsJson = new WebRequestHandler().Get("http://23.25.176.177/Products").Result;
			productsList = JsonConvert.DeserializeObject<List<Product>>(productsJson);


			//productsList = new List<Product>();


		}

		public void Create(Product product)
		{
			if(product is Product)
            {
				var response = new WebRequestHandler().Post("http://23.25.176.177/Create", product as Product).Result;
				var newProduct = JsonConvert.DeserializeObject<Product>(response);

				var oldVersion = productsList.FirstOrDefault(i => i.Id == newProduct.Id);

				if(oldVersion != null)
                {
					var index = productsList.IndexOf(oldVersion);
					productsList.RemoveAt(index);

					productsList.Insert(index, newProduct);
                }
				productsList.Add(newProduct);

			}
			else if (product is ProductByQuantity)
            {
				var response = new WebRequestHandler().Post("http://23.25.176.177/ProductsByQuantity", product as Product).Result;
				var newProduct = JsonConvert.DeserializeObject<ProductByQuantity>(response);
				productsList.Add(newProduct);

			}
			else if(product is ProductByWeight)
            {
				var response = new WebRequestHandler().Post("http://23.25.176.177/ProductsByWeight", product as Product).Result;
				var newProduct = JsonConvert.DeserializeObject<ProductByWeight>(response);
				productsList.Add(newProduct);

			}


		}

		public void Update(Product product)
		{

		}

		public void Delete(int id)
		{

			//if (productDelete == null)
			//{
			//	return;
			//}

			//productsList.Remove(productDelete);

			var response = new WebRequestHandler().Get("http://23.25.176.177/Products/Delete/{id}");
			var productDelete = productsList.FirstOrDefault(t => t.Id == id);

			if(productDelete == null)
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


