using CardManagement.Standard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CardManagement;
using System.Collections.Generic;
using System;

namespace CardManagementVSTS
{
    [TestClass()]
    public class StandardDeckProviderTest
    {
        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return _testContextInstance; }
            set { _testContextInstance = value; }
        }

        /// <summary>
        ///A test for StandardDeckProvider Constructor.
        ///Verifies that all the correct cards were played.
        ///</summary>
        [TestMethod()]
        public void StandardDeckProviderConstructorTest()
        {
            StandardDeckProvider target = new StandardDeckProvider();

            int numberOfCards = 54;
            Assert.AreEqual<int>(numberOfCards, target.OrderedSetOfCards.Count());
            Assert.AreEqual<int>(0, target.OrderedSetOfCards.Where(Card => Card.Type == StandardCardTypes.Unknown).Count());
            Assert.AreEqual<int>(2, target.OrderedSetOfCards.Where(card => card.Type == StandardCardTypes.Joker).Count());
            Assert.AreEqual<int>(13, target.OrderedSetOfCards.Where(card => card.Type == StandardCardTypes.Clover).Select(card => card.Value.ToString()).Distinct().Count());
            Assert.AreEqual<int>(13, target.OrderedSetOfCards.Where(card => card.Type == StandardCardTypes.Diamond).Select(card => card.Value.ToString()).Distinct().Count());
            Assert.AreEqual<int>(13, target.OrderedSetOfCards.Where(card => card.Type == StandardCardTypes.Heart).Select(card => card.Value.ToString()).Distinct().Count());
            Assert.AreEqual<int>(13, target.OrderedSetOfCards.Where(card => card.Type == StandardCardTypes.Spade).Select(card => card.Value.ToString()).Distinct().Count());
            Assert.AreEqual<int>(5, target.OrderedSetOfCards.Select(card => card.Type).Distinct().Count());
        }

        /// <summary>
        /// Creates a deck with a single set of cards.
        /// </summary>
        [TestMethod()]
        public void CreateDeckTest()
        {
            StandardDeckProvider provider = new StandardDeckProvider();
            List<StandardCard> cards = provider.OrderedSetOfCards.ToList();

            Deck<StandardCard> deck = provider.CreateDeck(1, false);
            bool isValid = IsValid_CollatedDuplication(cards, deck, 1);
            Assert.IsTrue(isValid, "The creation a deck by the StandardDeckProvier did not return a non-shuffled deck in a collated manner as expected.");

            Deck<StandardCard> shuffledDeck = provider.CreateDeck(1, true);
            bool isInvalid = !IsValid_CollatedDuplication(cards, shuffledDeck, 1);
            Assert.IsTrue(isInvalid, "The creation of a shuffled deck by the StandardDeckProvider returned a deck that was fully collated and unshuffled. This maybe possible, but winning the lottery is more likely.");
        }

        /// <summary>
        /// Creates a deck with a mulitple set of cards
        /// </summary>
        [TestMethod]
        public void CreateDeckTest_MultiDeck()
        {
            StandardDeckProvider provider = new StandardDeckProvider();
            List<StandardCard> cards = provider.OrderedSetOfCards.ToList();

            Deck<StandardCard> deck = provider.CreateDeck(3, false);
            bool isValid = IsValid_CollatedDuplication(cards, deck, 3);
            Assert.IsTrue(isValid, "The creation a deck by the StandardDeckProvier did not return a non-shuffled deck in a collated manner as expected.");

            Deck<StandardCard> shuffledDeck = provider.CreateDeck(3, true);
            bool isInvalid = !IsValid_CollatedDuplication(cards, shuffledDeck, 3);
            Assert.IsTrue(isInvalid, "The creation of a shuffled deck by the StandardDeckProvider returned a deck that was fully collated and unshuffled. This maybe possible, but winning the lottery is more likely.");
        }

        /// <summary>
        /// Creates a shuffled deck with multiple sets and verifies the content
        /// </summary>
        [TestMethod]
        public void CreateDeckTest_WithShuffle()
        {
            StandardDeckProvider provider = new StandardDeckProvider();
            List<StandardCard> cards = provider.OrderedSetOfCards.ToList();

            Deck<StandardCard> deck = provider.CreateDeck(3, true);
            bool isValid = IsValid_ShuffledDuplication(cards, deck, 3);
            Assert.IsTrue(isValid, "The creation a deck by the StandardDeckProvier did not return a shuffled deck.");
        }

        /// <summary>
        /// Verifies that deck consists of sets of singleDeck in accordance with numberOfDups
        /// </summary>
        /// <param name="singleDeck"></param>
        /// <param name="deck"></param>
        /// <param name="numberOfDups"></param>
        /// <returns></returns>
        private static bool IsValid_ShuffledDuplication(List<StandardCard> singleDeck, Deck<StandardCard> deck, int numberOfDups)
        {
            bool isCollated = IsValid_CollatedDuplication(singleDeck, deck, numberOfDups);
            if (isCollated)
            {
                return false;
            }

            List<StandardCard> deckCards = ConvertDeckToList(deck);

            Dictionary<Guid, int> expectedCounts = CountCardGuids(singleDeck);
            Dictionary<Guid, int> counts = CountCardGuids(deckCards);

            if (expectedCounts.Count != counts.Count)
            {
                return false;
            }
            
            foreach (KeyValuePair<Guid,int> expectedCount in expectedCounts)
            {
                if (counts.ContainsKey(expectedCount.Key) == false)
                    return false;
                else if (counts[expectedCount.Key] != expectedCount.Value * numberOfDups)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Counts the number of cards that have a Guid
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        private static Dictionary<Guid, int> CountCardGuids(List<StandardCard> cards)
        {
            Dictionary<Guid, int> counts = new Dictionary<Guid, int>(cards.Count);
            foreach (StandardCard card in cards)
            {
                if (counts.ContainsKey(card.Id) == false)
                {
                    counts.Add(card.Id, 1);
                }
                else
                {
                    counts[card.Id] += 1;
                }
            }
            return counts;
        }

        /// <summary>
        /// Takes a deck and converts it into a list by drawing cards.
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        private static List<StandardCard> ConvertDeckToList(Deck<StandardCard> deck)
        {
            List<StandardCard> list = new List<StandardCard>(deck.OriginalSize);
            for (int i = 0; i < deck.OriginalSize; i++)
            {
                StandardCard card = null;
                deck = deck.Draw(out card);
                list.Add(card);
            }
            return list;
        }

        /// <summary>
        /// Verifies that expectedContent is a subset of deck and each card in expectedContent exists sequentually numberOfDups times.
        /// </summary>
        /// <param name="expectedContent"></param>
        /// <param name="deck"></param>
        /// <param name="numberOfDups"></param>
        /// <returns></returns>
        private static bool IsValid_CollatedDuplication(List<StandardCard> expectedContent, Deck<StandardCard> deck, int numberOfDups)
        {
            for (int i = 0; i < expectedContent.Count; i++)
            {
                for (int j = 0; j < numberOfDups; j++)
                {
                    StandardCard card;
                    deck = deck.Draw(out card);
                    if (expectedContent[i].Equals(card) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
