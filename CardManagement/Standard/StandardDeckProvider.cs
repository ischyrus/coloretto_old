using System;
using System.Collections.Generic;

namespace CardManagement.Standard
{
    /// <summary>
    /// Provides a deck for standard cards.
    /// </summary>
    [Serializable]
    public class StandardDeckProvider : DeckProvider<StandardCard>
    {
        /// <summary>
        /// Get the name of the standard provider
        /// </summary>
        public override string Name
        {
            get { return Constants.StandardDeckProviderName; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public StandardDeckProvider()
        {
            _cards = new List<StandardCard>(54);
            _cards.Add(new StandardCard(StandardCardTypes.Joker));
            _cards.Add(new StandardCard(StandardCardTypes.Joker));

            string[] typeName = Enum.GetNames(typeof(StandardCardValues));
            for (int i = 1; i < 5; i++)
            {
                StandardCardTypes type = (StandardCardTypes)i;
                for (int j = 1; j < typeName.Length; j++)
                {
                    StandardCard card = new StandardCard(type, (StandardCardValues)Enum.Parse(typeof(StandardCardValues), typeName[j]));
                    _cards.Add(card);
                }
            }
        }
    }
}
