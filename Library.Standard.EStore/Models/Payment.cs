using System;
namespace Library.EStore.Models
{
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

