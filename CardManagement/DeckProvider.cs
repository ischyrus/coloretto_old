using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardManagement
{
    /// <summary>
    /// The base functionality required for a provider of a deck.
    /// Derived types will have the ability to generate decks, both from a hard coded standard configuration
    /// unique to each provider and from a DeckConfigurationProvider.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class DeckProvider<T> where T : Card
    {
        #region Static properties and variables
        private static Dictionary<string, object> _providers;

        static DeckProvider()
        {
            _providers = new Dictionary<string, object>();

            CardManagement.Standard.StandardDeckProvider standard = new CardManagement.Standard.StandardDeckProvider();
            CardManagement.Coloretto.ColorettoDeckProvider coloretto = new CardManagement.Coloretto.ColorettoDeckProvider();

            _providers.Add(standard.Name, standard);
            _providers.Add(coloretto.Name, coloretto);
        }

        /// <summary>
        /// Get a deck provider by name and handles cards of type U.
        /// </summary>
        /// <typeparam name="U">The type of card that the deck holds</typeparam>
        /// <param name="name">The name of the deck provider</param>
        /// <returns>The deckprovider.</returns>
        public static DeckProvider<U> GetDeckProvider<U>(string name) where U : Card
        {
            if (_providers.ContainsKey(name))
                return _providers[name] as DeckProvider<U>;
            return null;
        }
        #endregion

        protected List<T> _cards;

        /// <summary>
        /// Get the name of the provider
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Get a list of the cards ordered in some sensable manner.
        /// </summary>
        public virtual IEnumerable<T> OrderedSetOfCards
        {
            get { return _cards; }
        }

        /// <summary>
        /// Get a list of the Cards provided by this deck, shuffled
        /// </summary>
        public virtual IEnumerable<T> ShuffledCards
        {
            get
            {
                IEnumerable<T> shuffledCards = Shuffle(_cards);
                return shuffledCards;
            }
        }

        /// <summary>
        /// Create a new deck where 
        /// </summary>
        /// <param name="numberOfSets">The number of sets that should be in the deck.</param>
        /// <param name="doShuffle">Indicate if the deck should be shuffled. NOTE: unshuffled results will be collated/sorted and not repeated.</param>
        /// <returns>A new deck that contains the same cards that can be found in the OrderedsetOfCards property.</returns>
        public virtual Deck<T> CreateDeck(int numberOfSets, bool doShuffle)
        {
            if (_cards == null)
            {
                Debug.Fail("It should not be possible for a DeckProvider to not have some sort of list of cards. At the very least you should consider making it an empty list.");
                return new Deck<T>(null);
            }

            List<T> allCards = new List<T>(numberOfSets * _cards.Count);

            for (int j = 0; j < _cards.Count; j++)
            {
                for (int i = 0; i < numberOfSets; i++)
                {
                    allCards.Add(_cards[j].Clone() as T);
                }
            }

            Deck<T> newDeck = null;
            if (doShuffle)
            {
                IEnumerable<T> allCardsShuffled = Shuffle(allCards);
                newDeck = new Deck<T>(allCardsShuffled);
            }
            else
            {
                newDeck = new Deck<T>(allCards);
            }
            return newDeck;
        }

        /// <summary>
        /// A random number generator. 
        /// </summary>
        protected static Random _random = new Random();

        /// <summary>
        /// Shuffle the cards located in source.
        /// </summary>
        /// <param name="source">A list of cards.</param>
        /// <returns>An enumeration with the cards from the source in a random sequence.</returns>
        protected virtual IEnumerable<T> Shuffle(IList<T> source)
        {
            List<int> unusedIndexies = new List<int>(source.Count);
            for (int i = 0; i < source.Count; i++)
            {
                unusedIndexies.Add(i);
            }

            while (unusedIndexies.Count > 0)
            {
                int randomIndex = unusedIndexies[_random.Next(0, unusedIndexies.Count)];
                unusedIndexies.Remove(randomIndex);
                yield return source[randomIndex];
            }
        }
    }
}
