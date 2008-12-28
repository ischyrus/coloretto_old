using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Coloretto.Player;
using Game = Coloretto.Game;
using Coloretto.Actions;
using Coloretto.Services.Data;

namespace ColorettoServerLibrary
{
	/// <summary>
	/// The different states of the game
	/// </summary>
	[DataContract]
	public enum GameStates : int
	{
		/// <summary>
		/// Unknown state
		/// </summary>
		[EnumMember]
		Unknown = 0,

		[EnumMember]
		Created = 1,

		/// <summary>
		/// Game is in progress.
		/// </summary>
		[EnumMember]
		InProgress = 2,

		/// <summary>
		/// Games is finished
		/// </summary>
		[EnumMember]
		Finished = 3
	}

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class ColorettoService : IColorettoService
	{
		GameManager _manager;

		public string CurrentUser
		{
			get { return System.ServiceModel.OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name; }
		}

		public ColorettoService()
		{
			_manager = new GameManager();
		}

		public Guid CreateGame()
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				try
				{
					Coloretto.Services.Data.Game game = new Coloretto.Services.Data.Game()
							{
								Status = GameStates.Created.ToString(),
								GameId = Guid.NewGuid(),
								owner = CurrentUser,
								Start = DateTime.Now
							};
					game.GamePlayers.Add(new GamePlayer { GameId = game.GameId, Order = 0, Username = CurrentUser });
					context.Games.InsertOnSubmit(game);
					context.SubmitChanges();
					return game.GameId;
				}
				catch (Exception)
				{
					return Guid.Empty;
				}
			}
		}

		public IEnumerable<GameInfo> GetMyGames()
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				var l = context.GamePlayers.Where(player => player.Username == CurrentUser && (player.Game.Status == GameStates.Created.ToString() || player.Game.Status == GameStates.InProgress.ToString()))
										  .Select(p => new GameInfo
										  {
											  Players = p.Game.GamePlayers.Select(gp => gp.Username).ToList(),
											  GameId = p.Game.GameId,
											  State = (GameStates)Enum.Parse(typeof(GameStates), p.Game.Status, true),
											  Creation = p.Game.Start,
											  Owner = p.Game.owner
										  })
										  .ToList();
				return l;
			}
		}

		public string HelloWorld()
		{
			return "Hello World!";
		}

		public byte[] JoinGame(Guid gameId)
		{
			using (ColorettaDataContext context = new ColorettaDataContext())
			{
				GameInfo alreadyInGame = GetMyGames().Where(info => info.GameId == gameId).SingleOrDefault();
				if (alreadyInGame != null)
				{

				}
				else
				{
					_manager.JoinGame(gameId, CurrentUser);
				}
				return null;
			}
		}



		public Coloretto.Actions.ActionResult DrawCard(int pileNumber)
		{
			Profile player1 = new Profile();
			Profile player2 = new Profile();
			Profile player3 = new Profile();
			Profile player4 = new Profile();
			Game.ColorettoGame target = new Game.ColorettoGame(player1, player2, player3, player4);

			ActionResult result = target + DrawCardAction.DefaultAction;
			return result;
		}

		public Coloretto.Actions.ActionResult PlaceCard(int pileNumber)
		{
			return null;
		}
	}

	public class GameManager
	{
		public Dictionary<string, Guid> SessionIdToGameIdMappings { get; set; }
		public Dictionary<Guid, Game.ColorettoGame> Games { get; set; }
		public Dictionary<string, string> UserToSessionid { get; set; }
		public Dictionary<string, Guid> UserToGameId { get; set; }

		public GameManager()
		{
			Games = new Dictionary<Guid, Game.ColorettoGame>();
			SessionIdToGameIdMappings = new Dictionary<string, Guid>();
			UserToSessionid = new Dictionary<string, string>();
			UserToGameId = new Dictionary<string, Guid>();
		}

		public bool RejoinGame(string gameId)
		{
			throw  new NotImplementedException();
		}

		public bool JoinGame(Guid gameId, string CurrentUser)
		{
			if (Games.ContainsKey(gameId) && !UserToSessionid.ContainsKey(CurrentUser))
			{
				Game.ColorettoGame game = Games[gameId];
				if (game == null)
				{
					UserToSessionid.Add(CurrentUser, OperationContext.Current.SessionId);
					SessionIdToGameIdMappings.Add(OperationContext.Current.SessionId, gameId);
					UserToGameId.Add(CurrentUser, gameId);
					return true;
				}
			}
			else
			{
				throw new NotImplementedException();
			}

			return false;
		}
	}
}
