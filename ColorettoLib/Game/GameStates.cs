using System;
using System.Collections.Generic;
using System.Text;

namespace Coloretto.Game
{
    /// <summary>
    /// The different states of the game
    /// </summary>
    public enum GameStates : int
    {
        /// <summary>
        /// Unknown state
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Game is in progress.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Games is finished
        /// </summary>
        Finished = 2
    }
}
