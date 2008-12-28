using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace TestClientApp
{
	class Program
	{
		static void Main(string[] args)
		{
			ColorettoServiceClient client = new ColorettoServiceClient("DefaultBinding_IColorettoService_IColorettoService");
			client.ClientCredentials.UserName.UserName = "ischyrus";
			client.ClientCredentials.UserName.Password = "5342";

			client.Open();
			var hi = client.GetMyGames();
			//var id = client.CreateGame();
			//var hi2 = client.GetMyGames();
		}
	}
}
