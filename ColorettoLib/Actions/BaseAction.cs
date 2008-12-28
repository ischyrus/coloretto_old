using Coloretto.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coloretto.Actions
{
    /// <summary>
    /// Represents an action performed in the game.
    /// </summary>
    [Serializable]
    public abstract class BaseAction
    {
        private Guid _uniqueId;
        private string _name;

        #region Properties
        /// <summary>
        /// Get if this action ends a player's turn
        /// </summary>
        public abstract bool EndsTurn { get; }

        /// <summary>
        /// Get the name of the action
        /// </summary>
        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        /// <summary>
        /// Get the unique id of the action
        /// </summary>
        public Guid UniqueId
        {
            get { return _uniqueId; }
            protected set { _uniqueId = value; }
        } 
        #endregion

        #region Constructors
        /// <summary>
        /// The private constructor
        /// </summary>
        private BaseAction() { }

        /// <summary>
        /// Create the base action of name with a random unique id.
        /// </summary>
        /// <param name="name"></param>
        public BaseAction(string name)
        {
            this.Name = name;
            this.UniqueId = Guid.NewGuid();
        } 

        /// <summary>
        /// Create a base action of name with uniqueId.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uniqueId"></param>
        public BaseAction(string name, Guid uniqueId)
            : this(name)
        {
            this.UniqueId = UniqueId;
        }
        #endregion

        /// <summary>
        /// Perform the action on game.
        /// </summary>
        /// <param name="game">The game to perform the action on</param>
        /// <returns>The result of the action.</returns>
        internal ActionResult PerformAction(ColorettoGame game)
        {
            ActionResult result = Execute(game);
            return result;
        }

        /// <summary>
        /// Execute the action
        /// </summary>
        /// <remarks>
        /// I have this marked as protected to force the use of PerformAction(..). 
        /// This will force all actions to go through the base and easier to see when they happen.
        /// </remarks>
        /// <param name="game"></param>
        /// <returns></returns>
        protected abstract ActionResult Execute(ColorettoGame game);

        #region ToString
        /// <summary>
        /// Get a friendly display of this action
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Get a customized string of this action.
        /// {0} will be replaced with the friendly name
        /// {1} will be replaced with the unique id
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public virtual string ToString(string format)
        {
            return string.Format(format, this.Name, this.UniqueId);
        } 
        #endregion
    }
}
