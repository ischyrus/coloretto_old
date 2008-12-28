using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CardManagement;
using CardManagement.Coloretto;
using Coloretto.Actions;

namespace Coloretto.Game
{
	/// <summary>
	/// Represents a game of Coloretto.
	/// </summary>
	[Serializable]
	public class ColorettoGame
	{
		/// <summary>
		/// A source of random numbers
		/// </summary>
		private static Random _random = new Random();

		private static ColorettoGame _empty = new ColorettoGame();

		#region Private variables
		/// <summary>
		/// The player's index whos turn it is
		/// </summary>
		int _currentPlayerIndex;

		/// <summary>
		/// Previous actions
		/// </summary>
		TurnActions _currentTurn;

		/// <summary>
		/// The current cycle count.
		/// </summary>
		int _cycle;

		/// <summary>
		/// The deck of cards that can be drawn from
		/// </summary>
		Deck<ColorettoCard> _deck;

		/// <summary>
		/// Contains each players hand at the end of each round.
		/// </summary>
		ReadOnlyCollection<ReadOnlyCollection<CardCollection>> _gameScores;

		/// <summary>
		/// Represents each player's hand. 
		/// </summary>
		CardCollection[] _hands;

		/// <summary>
		/// The unique id assigned to this game
		/// </summary>
		Guid _id;

		/// <summary>
		/// A flag indicating if this is the last cycle for teh round
		/// </summary>
		bool _isLastCycleForRound;

		/// <summary>
		/// The number of rounds to play
		/// </summary>
		int _numberOfRounds;

		/// <summary>
		/// The piles that are on the game board.
		/// </summary>
		CardCollection[] _piles;

		/// <summary>
		/// The players in the game
		/// </summary>
		Player.Profile[] _players;

		/// <summary>
		/// Previous turns performed in this game.
		/// </summary>
		ReadOnlyCollection<TurnActions> _previousTurns;

		/// <summary>
		/// A round is how many times the deck has been exhausted.
		/// </summary>
		int _round;

		/// <summary>
		/// The current state of the game
		/// </summary>
		GameStates _state;

		/// <summary>
		/// How many time each play has played within this round.
		/// </summary>
		int _turn;

		/// <summary>
		/// If the user just drew a card then this holds the card that they drew
		/// </summary>
		ColorettoCard _visibleCard;

		/// <summary>
		/// Flags that map to players to indicate if they are completed for the cycle.
		/// </summary>
		bool[] _doneWithCycle;
		#endregion

		#region Properties
		/// <summary>
		/// Get the actions that are available
		/// </summary>
		public GameActions AvailableActions
		{
			get
			{
				if (State == GameStates.Finished)
				{
					return GameActions.None;
				}
				else if (_visibleCard != null)
				{
					return GameActions.PlaceCard;
				}

				bool atLeastOneHasAtLeastOneCard = false;
				bool atLeastOnePileIsNotFull = false;

				for (int i = 0; i < _piles.Length; i++)
				{
					if (_piles[i] != null)
					{
						if (_piles[i].Count > 0)
						{
							atLeastOneHasAtLeastOneCard = true;
							if (_piles[i].Count < Constants.DefaultMaxPileSize)
							{
								return GameActions.DrawOrPickupPile;
							}
						}
						else
						{
							atLeastOnePileIsNotFull = true;
						}
					}
				}

				GameActions actions = GameActions.None;
				if (atLeastOneHasAtLeastOneCard)
					actions |= GameActions.PickupPile;
				if (atLeastOnePileIsNotFull && _visibleCard == null)
					actions |= GameActions.Draw;

				Debug.Assert(actions != GameActions.None);

				return actions;
			}
		}

		/// <summary>
		/// Get the current player
		/// </summary>
		public Player.Profile CurrentPlayer
		{
			get { return _players[CurrentPlayerIndex]; }
		}

		/// <summary>
		/// Get or set the index of the current player
		/// </summary>
		public int CurrentPlayerIndex
		{
			get { return _currentPlayerIndex; }
			set
			{
				_currentPlayerIndex = value;
			}
		}

		/// <summary>
		/// Get the list of previous actions.
		/// </summary>
		public TurnActions CurrentTurn
		{
			get { return _currentTurn; }
			private set { _currentTurn = value; }
		}

		/// <summary>
		/// Get the cycle of this game. How many times a user has placed a card.
		/// </summary>
		public int Cycle
		{
			get { return _cycle; }
		}

		/// <summary>
		/// Get or set the deck of the game.
		/// </summary>
		protected Deck<ColorettoCard> Deck
		{
			get { return _deck; }
			set { _deck = value; }
		}

		/// <summary>
		/// Get an empty game 
		/// </summary>
		public static ColorettoGame Empty
		{
			get { return _empty; }
		}

		/// <summary>
		/// Get each player's scores for each round
		/// </summary>
		public ReadOnlyCollection<ReadOnlyCollection<CardCollection>> GameScores
		{
			get { return _gameScores; }
		}

		/// <summary>
		/// Get the hands that are in play
		/// </summary>
		public ReadOnlyCollection<CardCollection> Hands
		{
			get
			{
				ReadOnlyCollection<CardCollection> hands = new ReadOnlyCollection<CardCollection>(_hands);
				return hands;
			}
		}

		/// <summary>
		/// Get the unique id for this game
		/// </summary>
		public Guid Id
		{
			get { return _id; }
		}

		/// <summary>
		/// Get if the game is currently in the last cycle for this round
		/// </summary>
		public bool IsLastCycleForRound
		{
			get { return _isLastCycleForRound; }
			private set { _isLastCycleForRound = value; }
		}

		/// <summary>
		/// Get the number of rounds this game will last
		/// </summary>
		public int NumberOfRounds
		{
			get { return _numberOfRounds; }
			private set { _numberOfRounds = value; }
		}

		/// <summary>
		/// Get the current piles
		/// </summary>
		/// <remarks>Current piles are null if they have been picked up. I recommend making this a collection type rather than a list.</remarks>
		public ReadOnlyCollection<CardCollection> Piles
		{
			get
			{
				ReadOnlyCollection<CardCollection> piles = new ReadOnlyCollection<CardCollection>(_piles);
				return piles;
			}
		}

		/// <summary>
		/// Get the list of players in the game
		/// </summary>
		public IEnumerable<Player.Profile> Players
		{
			get { return _players; }
		}

		/// <summary>
		/// Get the list of previous turn actions performed in this game
		/// </summary>
		public ReadOnlyCollection<TurnActions> PreviousActions
		{
			get { return _previousTurns; }
			private set { _previousTurns = value; }
		}

		/// <summary>
		/// Get the round. This is how many times the deck has been exhausted.
		/// </summary>
		public int Round
		{
			get { return _round; }
			private set { _round = value; }
		}

		/// <summary>
		/// Get  the game state
		/// </summary>
		public GameStates State
		{
			get { return _state; }
			private set { _state = value; }
		}

		/// <summary>
		/// Get the turn we are in. A turn represents the number of times piles have been picked.
		/// </summary>
		public int Turn
		{
			get { return _turn; }
			private set { _turn = value; }
		}

		/// <summary>
		/// Get the visible card
		/// </summary>
		public ColorettoCard VisibleCard
		{
			get { return _visibleCard; }
			private set { _visibleCard = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Create an empty useless game
		/// </summary>
		private ColorettoGame()
		{
		}

		/// <summary>
		/// Create a new three player game
		/// </summary>
		/// <param name="player1"></param>
		/// <param name="player2"></param>
		/// <param name="player3"></param>
		public ColorettoGame(Player.Profile player1, Player.Profile player2, Player.Profile player3)
		{
			InitializeGame(player1, player2, player3);
		}

		/// <summary>
		/// Create a new four player game
		/// </summary>
		/// <param name="player1"></param>
		/// <param name="player2"></param>
		/// <param name="player3"></param>
		/// <param name="player4"></param>
		public ColorettoGame(Player.Profile player1, Player.Profile player2, Player.Profile player3, Player.Profile player4)
		{
			InitializeGame(player1, player2, player3, player4);
		}

		/// <summary>
		/// Create a new five player game
		/// </summary>
		/// <param name="player1"></param>
		/// <param name="player2"></param>
		/// <param name="player3"></param>
		/// <param name="player4"></param>
		/// <param name="player5"></param>
		public ColorettoGame(Player.Profile player1, Player.Profile player2, Player.Profile player3, Player.Profile player4, Player.Profile player5)
		{
			InitializeGame(player1, player2, player3, player4, player5);
		}

		public ColorettoGame(params Player.Profile[] players)
		{
			if (players.Length == 0 || players.Length < 3 || players.Length > 5)
				throw new ArgumentException("Invalid number of players");

			InitializeGame(players);
		}
		#endregion

		/// <summary>
		/// Adds an action to the game by performing the action and placing the changed game
		/// inside of ActionResult.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static ActionResult operator +(ColorettoGame game, BaseAction action)
		{
			if (action == null)
			{
				return new ActionResult(null, game, game.CurrentPlayer, null, false);
			}


			ActionResult result = action.PerformAction(game);
			if (result.Success)
			{
				result.Game.CurrentTurn = new TurnActions(result.Game.CurrentTurn, action, result);

				if (action.EndsTurn)
				{
					// Rollover the current turn actions to the previous turn list
					result.Game.PreviousActions = Helpers.CloneAndAppend(result.Game.PreviousActions, result.Game.CurrentTurn);
					result.Game.CurrentTurn = new TurnActions(result.Game);
				}
			}
			return result;
		}

		#region Public methods
		/// <summary>
		/// Clone this game
		/// </summary>
		/// <returns></returns>
		internal ColorettoGame Clone()
		{
			ColorettoGame newGame = new ColorettoGame();
			newGame.CurrentPlayerIndex = CurrentPlayerIndex;
			newGame._currentTurn = _currentTurn;
			newGame._cycle = _cycle;
			newGame._deck = _deck;
			newGame._gameScores = _gameScores;
			newGame._hands = CloneArray<CardCollection>(_hands);
			newGame._id = _id;
			newGame._isLastCycleForRound = _isLastCycleForRound;
			newGame._numberOfRounds = _numberOfRounds;
			newGame._piles = CloneArray<CardCollection>(_piles);
			newGame._players = CloneArray<Player.Profile>(_players);
			newGame._previousTurns = _previousTurns;
			newGame._round = _round;
			newGame._state = _state;
			newGame._turn = _turn;
			newGame._visibleCard = _visibleCard;
			newGame._doneWithCycle = CloneArray<bool>(_doneWithCycle);
			return newGame;
		}

		/// <summary>
		/// Pick up a pile
		/// </summary>
		/// <param name="pile"></param>
		/// <returns></returns>
		internal ColorettoGame PickupPile(int pile)
		{
			if ((AvailableActions & GameActions.PickupPile) != GameActions.PickupPile)
			{
				throw new InvalidOperationException("Picking up a pile is not one of the available actions.");
			}
			else if (pile < 0 || pile >= _piles.Length)
			{
				throw new ArgumentException("Pile " + pile + " does not exist. Unable to pick it up.");
			}
			else if (_piles[pile] == null)
			{
				throw new InvalidOperationException("Pile " + pile + " has already been picked up.");
			}
			else if (_piles[pile].Count == 0)
			{
				throw new InvalidOperationException("A user cannot pick up an empty pile.");
			}
			else
			{
				ColorettoGame newGame = Clone();
				newGame._hands[CurrentPlayerIndex] = _piles[pile] + _hands[CurrentPlayerIndex];
				newGame._piles[pile] = null;
				newGame._doneWithCycle[CurrentPlayerIndex] = true;
				AdvanceTurn(newGame);
				return newGame;
			}
		}

		/// <summary>
		/// Place the card on pile.
		/// Note: The card being placed is the same card returned by ViewDrawCard()
		/// </summary>
		/// <param name="pile"></param>
		/// <returns></returns>
		internal ColorettoGame PlaceCardOnPile(int pile)
		{
			if (AvailableActions != GameActions.PlaceCard)
			{
				throw new InvalidOperationException("A card can only be placed on a pile when the 'Draw' action is available to the user.");
			}
			else if (pile < 0 || pile >= _piles.Length)
			{
				throw new ArgumentException("Invalid pile.");
			}
			else if (_piles[pile] == null)
			{
				throw new InvalidOperationException("Pile " + pile + " has already been picked up.");
			}
			else if (_piles[pile].Count >= 3)
			{
				throw new InvalidOperationException("A pile cannot have more than three cards.");
			}
			else
			{
				ColorettoCard drawnCard = null;

				Deck<ColorettoCard> deckToDrawFrom = Deck;
				ColorettoGame newGame = Clone();

				bool isLastCycleCard = false;
				// Do until we get a non last cycle card
				do
				{
					isLastCycleCard = false;
					deckToDrawFrom = deckToDrawFrom.Draw(out drawnCard);
					newGame._deck = deckToDrawFrom;
					newGame._piles[pile] = _piles[pile].Add(drawnCard);

					Debug.Assert(drawnCard == newGame.VisibleCard);
					newGame.VisibleCard = null;

					// Perform the check to see if the drawn card is the last cycle card ... if so we need update the flag and make the loop repeat
					// TODO: We need to handle this differently.
					isLastCycleCard = drawnCard.CardType == ColorettoCardTypes.LastCycle;
					if (isLastCycleCard)
					{
						newGame.IsLastCycleForRound = true;
						newGame.VisibleCard = newGame._deck.PreviewDraw;
					}
				} while (isLastCycleCard);

				AdvanceTurn(newGame);
				return newGame;
			}
		}

		/// <summary>
		/// View the card that is next on the deck. 
		/// Note: This does not change the state of the game.
		/// </summary>
		/// <returns></returns>
		internal ColorettoGame DrawCard()
		{
			ColorettoGame clone = Clone();
			if ((AvailableActions & GameActions.Draw) == GameActions.Draw)
			{
				clone._visibleCard = Deck.PreviewDraw;
			}
			else
			{
				return null;
			}

			return clone;
		}
		#endregion

		#region Private methods

		/// <summary>
		/// Initizlies a game for the following player profiles
		/// </summary>
		/// <param name="playerProfiles"></param>
		private void InitializeGame(params Player.Profile[] playerProfiles)
		{
			Debug.Assert(playerProfiles.Length > 2 && playerProfiles.Length < 6, "An incorrect number of players have been allowed to join.");

			_numberOfRounds = 1;
			_players = playerProfiles;

			List<ReadOnlyCollection<CardCollection>> gameScores = new List<ReadOnlyCollection<CardCollection>>(_players.Length);
			for (int i = 0; i < _players.Length; i++)
				gameScores.Add((new List<CardCollection>(0)).AsReadOnly());
			_gameScores = gameScores.AsReadOnly();

			_hands = new CardCollection[_players.Length];
			for (int i = 0; i < _hands.Length; i++)
				_hands[i] = CardCollection.Empty;

			SetupGame();
		}

		/// <summary>
		/// Create the deck needed
		/// </summary>
		private void SetupGame()
		{
			int startingPlayer = _random.Next(0, _players.Length);
			CurrentPlayerIndex = startingPlayer;
			Debug.Assert(CurrentPlayer != null);

			SetupNewRound(this);

			_currentTurn = new TurnActions(this);
			_previousTurns = new ReadOnlyCollection<TurnActions>(new List<TurnActions>(0));
		}
		#endregion

		#region Private static methods
		/// <summary>
		/// Advances the game to the next turn
		/// </summary>
		/// <param name="newGame"></param>
		private static void AdvanceTurn(ColorettoGame newGame)
		{
			newGame._turn++;

			int nextPlayer = newGame.CurrentPlayerIndex;
			do
			{
				nextPlayer = (nextPlayer + 1) % newGame._players.Length;
			} while (newGame._doneWithCycle[nextPlayer] && nextPlayer != newGame.CurrentPlayerIndex);

			// If we have looped back to the same player and all the piles have been picked up then everyone has taken their action for this cycle.
			// TODO: This can be cleaned up by making ColorettoGame.Piles into a type that can have some utility methods added
			if (nextPlayer == newGame.CurrentPlayerIndex && Helpers.AllPilesPickedUp(newGame.Piles))
			{
				if (newGame.IsLastCycleForRound)
				{
					if (newGame.Round < newGame.NumberOfRounds)
					{
						SetupNewRound(newGame);
					}
					else
					{
						EndGame(newGame);
					}
				}
				else
				{
					SetupNewCycle(newGame);
				}
			}
			else
			{
				newGame.CurrentPlayerIndex = nextPlayer;
			}
		}

		/// <summary>
		/// Clone an array. This will create a new array however the references to the content is copied, cloning is not performed on each index.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		private static T[] CloneArray<T>(T[] source)
		{
			if (source == null)
				return null;

			T[] newArray = new T[source.Length];
			source.CopyTo(newArray, 0);
			return newArray;
		}

		/// <summary>
		/// End the game
		/// </summary>
		/// <param name="newGame"></param>
		private static void EndGame(ColorettoGame newGame)
		{
			List<ReadOnlyCollection<CardCollection>> newGameScores = new List<ReadOnlyCollection<CardCollection>>(newGame._gameScores.Count);
			for (int i = 0; i < newGame._gameScores.Count; i++)
			{
				newGameScores.Add(Helpers.CloneAndAppend(newGame.GameScores[i], newGame.Hands[i]));
			}
			newGame._gameScores = newGameScores.AsReadOnly();

			newGame._state = GameStates.Finished;
			newGame._hands = new CardCollection[newGame._players.Length];
			newGame._piles = new CardCollection[newGame._players.Length];

			for (int i = 0; i < newGame._players.Length; i++)
			{
				newGame._hands[i] = CardCollection.Empty;
				newGame._piles[i] = CardCollection.Empty;
			}
		}

		/// <summary>
		/// Setup the deck
		/// </summary>
		private static void SetupDeck(ColorettoGame game)
		{
			ColorettoDeckProvider deckProvider = new ColorettoDeckProvider();
			int numberOfColors = game._players.Length == 3 ? 6 : 7;
			game.Deck = deckProvider.CreateDeck(numberOfColors, true);
		}

		/// <summary>
		/// Setup for a new cycle
		/// </summary>
		/// <param name="game"></param>
		private static void SetupNewCycle(ColorettoGame game)
		{
			game._cycle += 1;
			game._doneWithCycle = new bool[game._players.Length];
			for (int i = 0; i < game._players.Length; i++)
				game._piles[i] = CardCollection.Empty;
		}

		/// <summary>
		/// Perfom the actions needed to start a new round
		/// </summary>
		/// <param name="game"></param>
		private static void SetupNewRound(ColorettoGame game)
		{
			List<ReadOnlyCollection<CardCollection>> newGameScores = new List<ReadOnlyCollection<CardCollection>>(game._gameScores.Count);
			for (int i = 0; i < game._gameScores.Count; i++)
			{
				newGameScores.Add(Helpers.CloneAndAppend(game.GameScores[i], game.Hands[i]));
			}
			game._gameScores = newGameScores.AsReadOnly();

			game.Round += 1;
			game.Turn = 1;
			game._isLastCycleForRound = false;
			game._hands = new CardCollection[game._players.Length];
			game._piles = new CardCollection[game._players.Length];

			for (int i = 0; i < game._players.Length; i++)
			{
				game._hands[i] = CardCollection.Empty;
				game._piles[i] = CardCollection.Empty;
			}

			SetupNewCycle(game);
			SetupDeck(game);
		}
		#endregion
	}
}
