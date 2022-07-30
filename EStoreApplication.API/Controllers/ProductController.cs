using System;
using EStoreApplication.API.Database;
using EStoreApplication.API.Database.EC;
using Library.EStore.Models;
using Microsoft.AspNetCore.Mvc;
namespace EStoreApplication.API.Controllers
{
	[ApiController]
	[Route("[controller]")]

	public class ProductController:ControllerBase
	{
		private readonly ILogger<ProductController> _logger;

		public ProductController(ILogger<ProductController> logger)
		{
			_logger = (ILogger<ProductController>?)logger;
		}


		[HttpGet]
		public List<Product> Get()
		{
			return Database.eStoreDatabase.products;
		}

		[HttpPost("Create")]
		public ProductByQuantity Create(Product product)
		{
			if (product.Id <= 0)
			{
				product.Id = eStoreDatabase.NextId();


				eStoreDatabase.products.Add(product);

			}

			var productToUpdate = eStoreDatabase.products.FirstOrDefault(t => t.Id == product.Id);

			if (productToUpdate != null)
			{
				eStoreDatabase.products.Remove(productToUpdate);

				eStoreDatabase.products.Add(product);
			}
			
			return (ProductByQuantity)product;

		}

		[HttpGet("Delete/{id}")]
		public int Delete(int id)
		{
			var productToDelete = eStoreDatabase.products.FirstOrDefault(i => i.Id == id);

			if (productToDelete != null)
			{
				var product = productToDelete as Product;

				if (product != null)
				{
					eStoreDatabase.products.Remove(productToDelete as Product);

				}

			}

			return id;
		}




	}
}


