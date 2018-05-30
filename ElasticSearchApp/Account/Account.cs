using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchApp.Accounts
{
	public enum Gender
	{
		M,
		F
	}

	public class Account
	{
		[Text(Name="account_number")]
		public string AccountNumber { get; set; }
		public decimal Balance { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public byte Age { get; set; }
		public Gender Gender { get; set; }
		public string Address { get; set; }
		public string Employer { get; set; }
		public string Email { get; set; }
		public string City{ get; set; }
		public string State	{ get; set; }
	}
}
