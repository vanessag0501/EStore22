using System;
using EStoreApplication.API.Database;
using EStoreApplication.API.Database.EC;
using Library.EStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EStoreApplication.API.Controllers
{
	[ApiController]
	[Route("[controller]")]



    public class ProductByQuantityController : ControllerBase
    {
		private readonly ILogger<ProductByQuantityController> _logger;

		public ProductByQuantityController(ILogger<WeatherForecastController> logger)
		{
			_logger = (ILogger<ProductByQuantityController>?)logger;
		}


		[HttpGet]
		public List<ProductByQuantity> Get()
		{
			//return new ProductEC();
			return eStoreDatabase.productByQuantities;
		}

		[HttpPost("Create")]
		public ProductByQuantity Create(ProductByQuantity product)
        {

			return new ProductEC.Create(product);
			//new ProductEC().Create();
			//if (product.Id <= 0)
			//{
			//	product.Id = eStoreDatabase.NextId();


			//	eStoreDatabase.productByQuantities.Add(product);

			//}

			//var productToUpdate = eStoreDatabase.productByQuantities.FirstOrDefault(t => t.Id == product.Id);

			//if(productToUpdate!=null)
   //         {
			//	eStoreDatabase.productByQuantities.Remove(productToUpdate);

			//	eStoreDatabase.productByQuantities.Add(product);
   //         }

			//return product;

		}

		[HttpGet("Delete/{id}")]
		public int Delete(int id)
		{
			var productToDelete = eStoreDatabase.products.FirstOrDefault(i => i.Id == id);

			if(productToDelete != null)
            {
				var product = productToDelete as Product;

				if(product != null)
                {
					eStoreDatabase.products.Remove(productToDelete as Product);

				}
				
			}
			
			return id;
		}

		


	}


}

