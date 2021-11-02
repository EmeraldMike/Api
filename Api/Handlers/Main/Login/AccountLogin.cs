﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Handlers.Account;
using Exolix.ApiHost;
using Exolix.Json;
using MongoDB.Bson;

namespace Api.Handlers.Main.Login
{
	public class LoginMessage
	{
		public string Token = "";
	}

	public class LoginFailedMessage
	{

	}

	public class LoginSuccessMessage
	{

	}

	public class AccountLogin
	{
		private ApiConnection Connection;

		public AccountLogin(ApiConnection connection)
		{
			Connection = connection;

			connection.OnMessage("login", (raw) =>
			{
				LoginMessage message = JsonHandler.Parse<LoginMessage>(raw);
				Instance accountInstance = new Instance(message.Token);
				AccountData? accountData = accountInstance.Data;

				if (accountData != null)
				{
					GlobalStorage.DataBase?.InsertRecord(GlobalStorage.Name, "OnlineInstances", new BsonDocument
					{
						{ "UserIdentifier", accountData.Identifier },
						{ "Node", GlobalStorage.Api?.ListeningAddress },
						{ "ConnectionIdentifier", connection.Identifier }
					});

					connection.Send<LoginSuccessMessage>("login _reply:success", new LoginSuccessMessage { });
					return;
				}

				connection.Send<LoginFailedMessage>("login _reply:failed", new LoginFailedMessage { });
			});
		}
	}
}
