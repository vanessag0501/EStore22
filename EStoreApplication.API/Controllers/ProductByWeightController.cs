using System;
using EStoreApplication.API.Database;
using EStoreApplication.API.Database.EC;
using Library.EStore.Models;
using Microsoft.AspNetCore.Mvc;


namespace EStoreApplication.API.Controllers
{
	[ApiController]
	[Route("[controller]")]

	public class ProductByWeightController: ControllerBase
	{
		private readonly ILogger<ProductByWeightController> _logger;

		public ProductByWeightController(ILogger<WeatherForecastController> logger)
		{
			_logger = (ILogger<ProductByWeightController>?)logger;
		}


		[HttpGet]
		public List<ProductByWeight> Get()
		{
			return eStoreDatabase.productByWeights;

		}

		[HttpPost("Create")]
		public ProductByQuantity Create(ProductByWeight product)
		{
			if (product.Id <= 0)
			{
				product.Id = eStoreDatabase.NextId();


				eStoreDatabase.productByWeights.Add(product);

			}

			var productToUpdate = eStoreDatabase.productByWeights.FirstOrDefault(t => t.Id == product.Id);

			if (productToUpdate != null)
			{
				eStoreDatabase.productByWeights.Remove(productToUpdate);

				eStoreDatabase.productByWeights.Add(product);
			}

			return product;

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

