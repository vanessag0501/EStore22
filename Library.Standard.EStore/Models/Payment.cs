using System;
using Library.Standard.EStore.Utility;
using Newtonsoft.Json;
namespace Library.EStore.Models
{
	[JsonConverter(typeof(ProductJsonConverter))]
	public class Payment : Product
	{
		public string pName { get; set; }
		public string Email { get; set; }
		public int CardNum { get; set; }
		public string Expire { get; set; }
		public int CVC { get; set; }
		public string Address { get; set; }

		public Payment()
		{
		}


		public override string ToString()
		{
			return $"{pName} :: {Email} :: {CardNum} :: {Expire} :: {CVC} :: {Address}";
		}
	}
}

