// ICard.cs created with MonoDevelop
// User: steven at 10:04 PM 6/28/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace CardManagement
{
    /// <summary>
    /// Used to generically define what a card must implement
    /// </summary>
    [Serializable]
    public abstract class Card
    {
        private Guid _id;
        private string _name;

        /// <value>
        /// Get the id for the card
        /// </value>
        public Guid Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        /// <value>
        /// Get the name for the card 
        /// </value>
        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }
        
        protected Card()
        {
        	Id = Guid.NewGuid();
        }

        /// <summary>
        /// Tells the derived type to clone
        /// </summary>
        /// <returns></returns>
        public abstract Card Clone();

        /// <summary>
        /// Called by derived types to get parent classes to clone.
        /// </summary>
        /// <param name="cloned"></param>
        /// <returns></returns>
        protected virtual void InternalClone(Card cloned)
        {
            cloned.Id = Id;
            cloned.Name = Name;
        }
    }
}
