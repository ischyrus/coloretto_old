using System;
using System.Collections.Generic;
using System.Text;

namespace Coloretto.Player
{
    [Serializable]
    public class ProfileCategory
    {
        private Guid _id;
        private string _name;

        /// <summary>
        /// Get or set the category id
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Get or set the category's name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
