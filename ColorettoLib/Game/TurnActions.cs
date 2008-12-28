using System;
using System.Collections.Generic;
using System.Text;
using Coloretto.Actions;
using System.Collections.ObjectModel;

namespace Coloretto.Game
{
    /// <summary>
    /// A container for all of the actions performed by a player in their turn
    /// </summary>
    [Serializable]
    public class TurnActions
    {
        private static TurnActions _empty = new TurnActions();
        private ReadOnlyCollection<BaseAction> _actions;
        private int _cycle;
        private ReadOnlyCollection<ActionResult> _results;
        private int _round;
        private int _turn;
        private Player.Profile _player;

        /// <summary>
        /// Get the actions performed in this turn
        /// </summary>
        public ReadOnlyCollection<BaseAction> Actions { get { return _actions; } }

        /// <summary>
        /// Get the cycle number
        /// </summary>
        /// <remarks>Refers to the number of times a user has taken a turn before selecting a piel.</remarks>
        public int Cycle { get { return _cycle; } }

        /// <summary>
        /// Get an empty turn action
        /// </summary>
        public static TurnActions Empty { get { return _empty; } }

        /// <summary>
        /// Get the results of the actions
        /// </summary>
        public ReadOnlyCollection<ActionResult> Results { get { return _results; } }

        /// <summary>
        /// Get which round the turn is in
        /// </summary>
        /// <remarks>Refers to the number of times the deck has been exhausted</remarks>
        public int Round { get { return _round; } }

        /// <summary>
        /// Get the turn number 
        /// </summary>
        /// <remarks>Refers to the number of hands each player has picked.</remarks>
        public int Turn { get { return _turn; } }

        /// <summary>
        /// get the player who performed the turn
        /// </summary>
        public Player.Profile Player { get { return _player; } }

        private TurnActions() { }

        public TurnActions(Player.Profile player, int round, int turn, int cycle, IList<BaseAction> actions)
        {
            _player = player;
            _round = round;
            _turn = turn;
            _cycle = cycle;
            _actions = new ReadOnlyCollection<BaseAction>(actions);
        }

        /// <summary>
        /// Create a turns action by supplying a previous one and appending a new action
        /// </summary>
        /// <param name="original"></param>
        /// <param name="appendAction"></param>
        public TurnActions(TurnActions original, BaseAction appendAction, ActionResult appendResult)
        {
            _player = original.Player;
            _round = original.Round;
            _turn = original.Turn;
            _cycle = original.Cycle;

            _actions = Helpers.CloneAndAppend(original.Actions, appendAction);
            _results = Helpers.CloneAndAppend(original.Results, appendResult);
        }

        public TurnActions(ColorettoGame game)
        {
            _player = game.CurrentPlayer;
            _round = game.Round;
            _turn = game.Turn;
            _cycle = game.Cycle;

            _actions = new ReadOnlyCollection<BaseAction>(new List<BaseAction>(0));
            _results = new ReadOnlyCollection<ActionResult>(new List<ActionResult>(0));
        }

        public TurnActions(ColorettoGame game, BaseAction action, ActionResult result)
            : this(game)
        {
            _actions = (new List<BaseAction> { action }).AsReadOnly();
            _results = (new List<ActionResult> { result }).AsReadOnly();
        }
    }
}
