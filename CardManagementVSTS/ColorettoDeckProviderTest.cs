using CardManagement.Coloretto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CardManagement;
using System.Collections.Generic;
using System.Text;
namespace CardManagementVSTS
{
    /// <summary>
    ///This is a test class for ColorettoDeckProviderTest and is intended
    ///to contain all ColorettoDeckProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ColorettoDeckProviderTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        /// Verify that coloretto cards are created as the default deck
        ///</summary>
        [TestMethod()]
        public void ColorettoDeckProvider_ConstructorTest()
        {
            ColorettoDeckProvider target = new ColorettoDeckProvider();
            int numberOfCards = target.OrderedSetOfCards.Count();

            int expectedNumberOfColoredCards = 63;
            int expectedNumberOfPlus2 = 10;
            int expectedNumberOfWilds = 3;
            int expectedNumberOfEndTurnCards = 1;

            Assert.AreEqual<int>(expectedNumberOfColoredCards + expectedNumberOfPlus2 + expectedNumberOfWilds + expectedNumberOfEndTurnCards, numberOfCards);
            Assert.AreEqual<int>(expectedNumberOfColoredCards, target.OrderedSetOfCards.Where(cc => cc.CardType == ColorettoCardTypes.Color).Count());
            Assert.AreEqual<int>(expectedNumberOfPlus2, target.OrderedSetOfCards.Where(cc => cc.CardType == ColorettoCardTypes.Plus2).Count());
            Assert.AreEqual<int>(expectedNumberOfWilds, target.OrderedSetOfCards.Where(cc => cc.CardType == ColorettoCardTypes.Wild).Count());
            Assert.AreEqual<int>(expectedNumberOfEndTurnCards, target.OrderedSetOfCards.Where(cc => cc.CardType == ColorettoCardTypes.LastCycle).Count());
        }

        /// <summary>
        /// Verifies that a single card can be placed in a fixed position. No conflict checking here.
        /// </summary>
        [TestMethod]
        public void ColorettoDeckProvider_FixedCardPosition()
        {
            ColorettoDeckProvider target = new ColorettoDeckProvider();
            Deck<ColorettoCard> deck = target.CreateDeck(7, true);

            ColorettoCard lastCycleCard = DeckEnumeration(deck).Where(cc=>cc.CardType == ColorettoCardTypes.LastCycle).FirstOrDefault();
            Assert.IsNotNull(lastCycleCard);
            Assert.IsTrue(lastCycleCard.FixedDeckPosition.HasValue);

            StringBuilder sb = new StringBuilder();
            foreach (var item in DeckEnumeration(deck))
            {
                sb.AppendLine(item.CardType.ToString());
            }

            int count=0;
            foreach (ColorettoCard card in DeckEnumeration(deck))
            {
                if (card.CardType == ColorettoCardTypes.LastCycle)
                    break;
                else
                    count += 1;
            }

            Assert.AreEqual<int>(lastCycleCard.FixedDeckPosition.Value, count);
        }

        /// <summary>
        /// Enumerate through a deck
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        private static IEnumerable<ColorettoCard> DeckEnumeration(Deck<ColorettoCard> deck)
        {
            if (deck != null && !deck.IsEmpty)
            {
                do
                {
                    ColorettoCard card = null;
                    deck = deck.Draw(out card);
                    yield return card;
                } while (!deck.IsEmpty);
            }
        }
    }
}
