using System;
using System.Collections.Generic;
using System.Text;
using Coloretto.Game;
using Coloretto.Player;

namespace Coloretto.Actions
{
	[Serializable]
	public class ActionResult
	{
		private string _action;
		private object _data;
		private ColorettoGame _game;
		private Profile _player;
		private bool _success;

		/// <summary>
		/// Get the name of the action performed
		/// </summary>
		public string Action
		{
			get { return _action; }
			private set { _action = value; }
		}

		/// <summary>
		/// Get the data that is relevant to this action
		/// </summary>
		public object Data
		{
			get { return _data; }
			private set { _data = value; }
		}

		/// <summary>
		/// Get the game that is the result of this action
		/// </summary>
		public ColorettoGame Game
		{
			get { return _game; }
			private set { _game = value; }
		}

		/// <summary>
		/// Get the profile that this action was performed by
		/// </summary>
		public Profile Player
		{
			get { return _player; }
			private set { _player = value; }
		}

		/// <summary>
		/// Get the indication if the action was successful
		/// </summary>
		public bool Success
		{
			get { return _success; }
			private set { _success = value; }
		}

		/// <summary>
		/// Private constructor
		/// </summary>
		private ActionResult() { }

		/// <summary>
		/// Create an action result.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="game"></param>
		/// <param name="data"></param>
		/// <param name="success"></param>
		internal ActionResult(string action, ColorettoGame game, Profile player, object data, bool success)
		{
			this.Action = action;
			this.Game = game;
			this.Data = data;
			this.Player = player;
			this.Success = success;
		}
	}
}
