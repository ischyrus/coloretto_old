using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Coloretto.Actions;
using Coloretto.Services.Data;

namespace ColorettoServerLibrary
{
	[ServiceContract]
	public interface IColorettoService
	{
		[OperationContract]
		string HelloWorld();

		[OperationContract]
		Guid CreateGame();

		[OperationContract]
		ActionResult DrawCard(int pileNumber);

		[OperationContract]
		IEnumerable<GameInfo> GetMyGames();

		[OperationContract]
		byte[] JoinGame(Guid gameId);

		//[OperationContract]
		//bool StartGame();
	}
}
