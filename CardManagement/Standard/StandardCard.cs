using System;

namespace CardManagement.Standard
{
	/// <summary>
	/// Represents a standard card that is played
	/// </summary>
    [Serializable]
    public class StandardCard : Card
	{
        #region Private variables
        private StandardCardTypes _type;
        private StandardCardValues _value; 
        #endregion

        #region Properties
        /// <value>
        /// Get the type of the card
        /// </value>
        public StandardCardTypes Type
        {
            get { return _type; }
            private set { _type = value; }
        }

        /// <value>
        /// Get the value of the card
        /// </value>
        public StandardCardValues Value
        {
            get { return _value; }
            private set { _value = value; }
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a standard card without a value. For now, this can only be used to create a joker.
        /// </summary>
        /// <param name="type"></param>
        public StandardCard(StandardCardTypes type) : base()
        {
            Type = type;
            ValidateStandardCardArguments();
        }

        /// <summary>
        /// Create a standard card of type
        /// </summary>
        /// <param name="type">
        /// A <see cref="StandardCardTypes"/>
        /// </param>
        public StandardCard(StandardCardTypes type, StandardCardValues value) : base()
        {
            if (type == StandardCardTypes.Joker && value != StandardCardValues.None)
            {
                string message = string.Format("Unable to create Standard Card of type {0} with a value assigned. (Expected {1} for value.)", type, StandardCardValues.None);
                throw new ArgumentException(message);
            }

            Id = Guid.NewGuid();
            Name = Type.ToString();
            Type = type;
            Value = value;
            ValidateStandardCardArguments();
        }

        /// <summary>
        /// Create a card with the specified name
        /// </summary>
        /// <param name="name">
        /// A <see cref="System.String"/>
        /// </param>
        public StandardCard(string name, StandardCardTypes type, StandardCardValues value)
            : this(type, value)
        {
            Name = name;
        }

        /// <summary>
        /// Create a card that has the specified id and name
        /// </summary>
        /// <param name="id">
        /// The id of the card
        /// </param>
        /// <param name="name">
        /// The name of the card
        /// </param>
        public StandardCard(Guid id, string name, StandardCardTypes type, StandardCardValues value)
            : this(type, value)
        {
            Id = id;
        }

        /// <summary>
        /// Override the output of the string to be the output of the card name
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        public override string ToString()
        {
            switch (this.Type)
            {
                case StandardCardTypes.Unknown:
                    return string.Empty;
                case StandardCardTypes.Joker:
                    return Type.ToString();
                default:
                    return string.Format("{0} of {1}s", Value, Type);
            }
        } 
        #endregion

        #region Private Methods
        /// <summary>
        /// Used to validate the arguments passed in during class construction
        /// </summary>
        private void ValidateStandardCardArguments()
        {
            if (Type == StandardCardTypes.Joker)
            {
                if (Value != StandardCardValues.None)
                {
                    string message = string.Format("Unable to create Standard Card of type {0} with a value assigned. (Expected {1} for value.)", Type, StandardCardValues.None);
                    throw new ArgumentException(message);
                }
            }
            else if (Type != StandardCardTypes.Unknown)
            {
                if (Value == StandardCardValues.None)
                {
                    string message = string.Format("Unable to create Standard Card of type {0} without a value assigned.", Type);
                    throw new ArgumentException(message);
                }
            }
            else
            {
                string message = string.Format("A StndardCard was created with a type of {0}. This probably should not be done.", Type);
                System.Diagnostics.Debug.Fail(message);
            }
        } 
        #endregion

        public override Card Clone()
        {
            StandardCard clone = new StandardCard(Type, Value);
            InternalClone(clone);
            return clone;
        }

        public override bool Equals(object obj)
        {
            StandardCard card = obj as StandardCard;
            if (card == null)
                return false;
            else if (card.Id != Id || card.Name != Name || card.Type != Type || card.Value != Value)
                return false;
            else
                return true;
        }
        
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
