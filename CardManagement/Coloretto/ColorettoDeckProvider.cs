/*
 * Created by SharpDevelop.
 * User: sscherm
 * Date: 6/30/2008
 * Time: 2:45 PM
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardManagement.Coloretto
{
    /// <summary>
    /// Description of ColorettoDeckProvider.
    /// </summary>
    [Serializable]
    public class ColorettoDeckProvider : DeckProvider<ColorettoCard>
    {
        /// <summary>
        /// Get the name of this deck provider
        /// </summary>
        public override string Name
        {
            get { return Constants.ColorettoDeckProviderName; }
        }

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ColorettoDeckProvider()
            : base()
        {
            string[] colors = Enum.GetNames(typeof(ColorettoCardColors));
            int numberOfColors = colors.Length - 1;
            int numberOfCards = (numberOfColors * Constants.DefaultNumberOfEachColor) + Constants.DefaultNumberOfPlus2 + Constants.DefaultNumberOfWilds + Constants.DefaultLastCycleCard;

            _cards = new System.Collections.Generic.List<ColorettoCard>(numberOfCards);

            int indexOffset = 1;
            for (int i = 0; i < Constants.DefaultLastCycleCard; i++)
            {
                ColorettoCard newCard = new ColorettoCard(ColorettoCardTypes.LastCycle, numberOfCards - Constants.DefaultNumberOfCardsOnEndBeforeLastCycleCard - indexOffset);
                _cards.Add(newCard);
            }

            for (int i = 0; i < Constants.DefaultNumberOfPlus2; i++)
                _cards.Add(new ColorettoCard(ColorettoCardTypes.Plus2));

            for (int i = 0; i < Constants.DefaultNumberOfWilds; i++)
                _cards.Add(new ColorettoCard(ColorettoCardTypes.Wild));

            for (int i = 0; i < Constants.DefaultNumberOfEachColor; i++)
                for (int j = 1; j < colors.Length; j++)
                    _cards.Add(new ColorettoCard((ColorettoCardColors)Enum.Parse(typeof(ColorettoCardColors), colors[j])));
        } 
        #endregion

        #region Public methods
        /// <summary>
        /// Create a deck with the specified number of colors
        /// </summary>
        /// <param name="numberOfColors"></param>
        /// <param name="doShuffle"></param>
        /// <returns></returns>
        public override Deck<ColorettoCard> CreateDeck(int numberOfColors, bool doShuffle)
        {
            if (_cards == null)
            {
                Debug.Fail("It should not be possible for a DeckProvider to not have some sort of list of cards. At the very least you should consider making it an empty list.");
                return new Deck<ColorettoCard>(null);
            }

            List<ColorettoCard> allCards = new List<ColorettoCard>();
            string[] allColors = Enum.GetNames(typeof(ColorettoCardColors));
            List<string> useableColors = new List<string>();

            for (int i = 1; i <= numberOfColors; i++)
            {
                useableColors.Add(allColors[i]);
            }

            for (int j = 0; j < _cards.Count; j++)
            {
                if (_cards[j].CardType != ColorettoCardTypes.Color || useableColors.Contains(_cards[j].Color.ToString()))
                {
                    allCards.Add(_cards[j].Clone() as ColorettoCard);
                }
            }

            Deck<ColorettoCard> newDeck = null;
            if (doShuffle)
            {
                IEnumerable<ColorettoCard> allCardsShuffled = Shuffle(allCards);
                newDeck = new Deck<ColorettoCard>(allCardsShuffled);
            }
            else
            {
                newDeck = new Deck<ColorettoCard>(allCards);
            }
            return newDeck;
        } 
        #endregion

        #region Protected and private methods
        /// <summary>
        /// Shuffle the deck. Overrides the base implementation to allow for the notion of fixed position cards.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected override System.Collections.Generic.IEnumerable<ColorettoCard> Shuffle(System.Collections.Generic.IList<ColorettoCard> source)
        {
            Queue<ColorettoCard> lastTurnCards = GetFixedPositionCardsOnly(source);
            IList<ColorettoCard> allOtherCards = GetNonFixedPositionCardsOnly(source);

            List<int> unusedIndexies = new List<int>(allOtherCards.Count);
            for (int i = 0; i < allOtherCards.Count; i++)
            {
                unusedIndexies.Add(i);
            }

            int currentIndex = 0;
            while (unusedIndexies.Count > 0)
            {
                if (lastTurnCards.Count > 0 && currentIndex >= lastTurnCards.Peek().FixedDeckPosition.Value)
                {
                    do
                    {
                        currentIndex++;
                        yield return lastTurnCards.Dequeue();
                    } while (lastTurnCards.Count > 0 && currentIndex == lastTurnCards.Peek().FixedDeckPosition.Value);
                }
                else
                {
                    int randomIndex = unusedIndexies[_random.Next(0, unusedIndexies.Count)];
                    unusedIndexies.Remove(randomIndex);
                    currentIndex++;
                    yield return allOtherCards[randomIndex];
                }
            }
        }

        /// <summary>
        /// Get a queue of cards ordered by what position they should be in.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static Queue<ColorettoCard> GetFixedPositionCardsOnly(IList<ColorettoCard> source)
        {
            List<ColorettoCard> cards = new List<ColorettoCard>(FilterCardsBasedOnFixedShufflePosition(source, true));
            cards.Sort((Comparison<ColorettoCard>)delegate(ColorettoCard left, ColorettoCard right) { return left.FixedDeckPosition.Value - right.FixedDeckPosition.Value; });
            Queue<ColorettoCard> cardQueue = new Queue<ColorettoCard>(cards);
            return cardQueue;
        }

        /// <summary>
        /// Get a list of the cards without a fixed position.
        /// When shuffled these cards will be in any random order.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static IList<ColorettoCard> GetNonFixedPositionCardsOnly(IList<ColorettoCard> source)
        {
            List<ColorettoCard> cards = new List<ColorettoCard>(FilterCardsBasedOnFixedShufflePosition(source, false));
            return cards;
        }

        /// <summary>
        /// Enumerate through the source and either pick cards that have a fixed shuffle position or cards without.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="isFixed"></param>
        /// <returns></returns>
        private static IEnumerable<ColorettoCard> FilterCardsBasedOnFixedShufflePosition(IList<ColorettoCard> source, bool isFixed)
        {
            foreach (ColorettoCard card in source)
                if (card.FixedDeckPosition.HasValue == isFixed)
                    yield return card;
        } 
        #endregion
    }
}
