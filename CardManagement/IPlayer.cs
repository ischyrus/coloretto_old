using System;
using System.Collections.Generic;
using System.Text;

namespace CardManagement
{
    /// <summary>
    /// The contract that every player implements
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The username for this player
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Get a unique identifier for this player
        /// </summary>
        Guid UniqueId { get; }
    }
}
