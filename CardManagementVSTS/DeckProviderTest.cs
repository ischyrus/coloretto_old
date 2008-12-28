using CardManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CardManagement.Standard;
using System.Linq;

namespace CardManagementVSTS
{
    [TestClass()]
    public class DeckProviderTest
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

        [TestMethod()]
        [DeploymentItem("CardManagement.dll")]
        public void ShuffleTest()
        {
            StandardDeckProvider provider = new StandardDeckProvider();
            IEnumerable<StandardCard> orderedCards = provider.OrderedSetOfCards;

            int numberOfCards = provider.OrderedSetOfCards.Count();
            Assert.IsTrue(numberOfCards > 0);

            IEnumerable<StandardCard> shuffledCards1 = provider.ShuffledCards;
            bool different = VerifyDifferentOrder(orderedCards, shuffledCards1);
            Assert.IsTrue(different);
            Assert.AreEqual<int>(numberOfCards, shuffledCards1.Count());

            IEnumerable<StandardCard> shuffledCards2 = provider.ShuffledCards;
            bool shuffledDifferent = VerifyDifferentOrder(shuffledCards1, shuffledCards2);
            Assert.IsTrue(shuffledDifferent);
            Assert.AreEqual<int>(numberOfCards, shuffledCards2.Count());
        }

        [TestMethod]
        [DeploymentItem("CardManagement.dll")]
        public void ShuffleTest_SameContent()
        {
            StandardDeckProvider provider = new StandardDeckProvider();
            List<StandardCard> orderedCards = provider.OrderedSetOfCards.ToList();

            int numberOfCards = provider.OrderedSetOfCards.Count();
            Assert.IsTrue(numberOfCards > 0);

            IEnumerable<StandardCard> shuffledCards = provider.ShuffledCards;
            foreach (StandardCard card in shuffledCards)
            {
                orderedCards.Remove(card);
            }

            Assert.AreEqual<int>(0, orderedCards.Count);
        }


        /// <summary>
        /// Given two enumerable collections, verify that they are not int he same order
        /// </summary>
        /// <param name="orderedCards"></param>
        /// <param name="shuffledCards1"></param>
        /// <returns></returns>
        private bool VerifyDifferentOrder(IEnumerable<StandardCard> orderedCards, IEnumerable<StandardCard> shuffledCards1)
        {
            IEnumerator<StandardCard> orderedEnum = orderedCards.GetEnumerator();
            IEnumerator<StandardCard> suffledEnum = shuffledCards1.GetEnumerator();

            while (orderedEnum.MoveNext() && suffledEnum.MoveNext())
            {
                if (orderedEnum.Current != suffledEnum.Current)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
