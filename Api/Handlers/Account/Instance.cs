﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Handlers.Account
{
	public class OtherEmail
	{
		public string Email = "";
		public bool Verified = false;
	}

	public class AccountData
	{
		public ObjectId _id = new ObjectId();
		public string UserName = "";
		public string Password = "";
		public string DisplayName = "";
		public string Email = "";
		public OtherEmail[]? OtherEmails = null;
		public string Token = "";
		public string Identifier = "";
		public Boolean EmailVerified = false;
	}
	 
	public class Instance
	{
		public AccountData? Data = null;

		public Instance(string? token = null, string? identifier = null)
		{
			var data = GlobalStorage.DataBaseConnection?.GetDatabase(GlobalStorage.Name).GetCollection<AccountData>("OnlineInstances").Find(Builders<AccountData>.Filter.Where((x) => true)).Limit(1).ToList();

			if (data?.Count == 1)
			{
				Data = data[0];
				return;
			}

			throw new Exception("The account does not exist");
		}
	}
}
