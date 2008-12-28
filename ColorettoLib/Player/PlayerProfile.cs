using System;
using System.Collections.Generic;
using System.Text;

namespace Coloretto.Player
{
    /// <summary>
    /// Represents user's profile
    /// </summary>
    [Serializable]
    public class Profile
    {
        /// <summary>
        /// The category that this profile belongs to
        /// </summary>
        private ProfileCategory _category;

        /// <summary>
        /// The color the player is assigned
        /// </summary>
        private string _color;

        /// <summary>
        /// The unique id assigned to this user's profile
        /// </summary>
        private Guid _uniqueId;

        /// <summary>
        /// The username of teh player
        /// </summary>
        private string _username;

        /// <summary>
        /// Get or set the category of the profile
        /// </summary>
        public ProfileCategory Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// Get or set the color for this profile
        /// </summary>
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// Get or set the unique id for this profile
        /// </summary>
        public Guid UniqueId
        {
            get { return _uniqueId; }
            set { _uniqueId = value; }
        }

        /// <summary>
        /// Get or set the username for the profile
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// Create an empty profile
        /// </summary>
        public Profile()
        {
            UniqueId = Guid.Empty;
        }
    }
}
