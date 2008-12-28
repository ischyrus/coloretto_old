using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ColorettoServerLibrary;

namespace TestServiceApp
{
	class Program
	{
		static void Main(string[] args)
		{
			ColorettoService service = new ColorettoService();
			using (ServiceHost host = new ServiceHost(service, new Uri("net.tcp://schermerhorn.me:9999/services")))
			{
				host.Credentials.ServiceCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.X509FindType.FindByThumbprint, "8d 98 9c 60 cb 34 58 70 40 db dd 9e c9 b0 83 e8 08 59 b5 82");
				host.Open();
				Console.WriteLine("Ready...");
				Console.ReadLine();
			}
		}
	}
}
