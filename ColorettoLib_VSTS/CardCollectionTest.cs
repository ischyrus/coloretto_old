using Coloretto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardManagement.Coloretto;

namespace ColorettoLib_VSTS
{
    [TestClass()]
    public class CardCollectionTest
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
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void CardCollection_RemoveTest()
        {
            CardCollection target = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            target = target.Add(card1);
            target = target.Add(card2);
            target = target.Add(card3);

            target = target.Remove(card2);

            Assert.AreEqual<ColorettoCard>(card1, target[0]);
            Assert.AreEqual<ColorettoCard>(card3, target[1]);
            Assert.AreEqual<int>(2, target.Count);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void CardCollection_AddTest()
        {
            CardCollection target = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            target = target.Add(card1);
            target = target.Add(card2);
            target = target.Add(card3);

            Assert.AreEqual<ColorettoCard>(card1, target[0]);
            Assert.AreEqual<ColorettoCard>(card2, target[1]);
            Assert.AreEqual<ColorettoCard>(card3, target[2]);
        }
        
        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void CardCollection_BasicScoreCheck()
        {
            CardCollection target = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            target = target.Add(card1);
            Assert.AreEqual<int>(1, target.Score);

            target = target.Add(card2);
            Assert.AreEqual<int>(2, target.Score);

            target = target.Add(card3);
            Assert.AreEqual<int>(3, target.Score);

            target = target.Add((ColorettoCard)card1.Clone());
            Assert.AreEqual<int>(5, target.Score);

            target = target.Add((ColorettoCard)card1.Clone());
            Assert.AreEqual<int>(8, target.Score);

            target = target.Add((ColorettoCard)card2.Clone());
            Assert.AreEqual<int>(10, target.Score);
        }

        [TestMethod]
        public void CardCollection_TooManyPiles()
        {
            CardCollection target = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);
            ColorettoCard card4 = new ColorettoCard(ColorettoCardColors.Green);
            ColorettoCard card5 = new ColorettoCard(ColorettoCardColors.Orange);

            ColorettoCard wild = new ColorettoCard(ColorettoCardTypes.Wild);

            target = target.Add(card1);
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add(wild);

            target = target.Add(card2);
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());

            target = target.Add(card3);
            target = target.Add(card4);

            Assert.AreEqual<int>(16, target.Score);
        }

        [TestMethod]
        public void CardCollection_WildCardScores()
        {
            CardCollection target = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            ColorettoCard wild = new ColorettoCard( ColorettoCardTypes.Wild);

            target = target.Add(card1);
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add(wild);

            target = target.Add(card2);
            target = target.Add(card3);

            Assert.AreEqual<int>(12, target.Score);
        }

        [TestMethod]
        public void CardCollection_WildCardFallsOverToPileTwo()
        {
            CardCollection target = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            ColorettoCard wild = new ColorettoCard(ColorettoCardTypes.Wild);

            target = target.Add(card1);
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add(wild);

            target = target.Add(card2);
            target = target.Add(card3);

            Assert.AreEqual<int>(25, target.Score);
        }

        [TestMethod]
        public void CardCollection_WildCardFallsOverToPileThree()
        {
            CardCollection target = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            ColorettoCard wild = new ColorettoCard(ColorettoCardTypes.Wild);

            target = target.Add(card1);
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add(wild);

            target = target.Add(card2);
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());

            target = target.Add(card3);

            Assert.AreEqual<int>(45, target.Score);
        }

        [TestMethod]
        public void CardCollection_WildCardMaxsOut()
        {
            CardCollection target = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            ColorettoCard wild = new ColorettoCard(ColorettoCardTypes.Wild);

            target = target.Add(card1);
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add((ColorettoCard)card1.Clone());
            target = target.Add(wild);

            target = target.Add(card2);
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());
            target = target.Add((ColorettoCard)card2.Clone());

            target = target.Add(card3);
            target = target.Add((ColorettoCard)card3.Clone());
            target = target.Add((ColorettoCard)card3.Clone());
            target = target.Add((ColorettoCard)card3.Clone());
            target = target.Add((ColorettoCard)card3.Clone());
            target = target.Add((ColorettoCard)card3.Clone());

            Assert.AreEqual<int>(63, target.Score);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod()]
        public void CardCollection_AdditionTest()
        {
            CardCollection left = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            left = left.Add(card1);
            left = left.Add(card2);
            left = left.Add(card3);

            CardCollection right = new CardCollection();

            card1 = new ColorettoCard(ColorettoCardColors.Blue);
            card2 = new ColorettoCard(ColorettoCardColors.Brown);
            card3 = new ColorettoCard(ColorettoCardColors.Gray);

            right = right.Add(card1);
            right = right.Add(card2);
            right = right.Add(card3);

            CardCollection added = left + right;
            Assert.AreEqual<int>(6, added.Count);
            Assert.AreEqual<int>(9, added.Score);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void CardCollection_EqualityTest1()
        {
            CardCollection left = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            left = left.Add(card1);
            left = left.Add(card2);
            left = left.Add(card3);

            CardCollection right = new CardCollection();

            card1 = new ColorettoCard(ColorettoCardColors.Blue);
            card2 = new ColorettoCard(ColorettoCardColors.Brown);
            card3 = new ColorettoCard(ColorettoCardColors.Gray);

            right = right.Add(card1);
            right = right.Add(card2);
            right = right.Add(card3);

            bool areEqual = left == right;
            Assert.IsTrue(areEqual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void CardCollection_EqualityTest2()
        {
            CardCollection left = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            left = left.Add(card1);
            left = left.Add(card2);
            left = left.Add(card3);

            CardCollection right = new CardCollection();

            card1 = new ColorettoCard(ColorettoCardColors.Blue);
            card2 = new ColorettoCard(ColorettoCardColors.Orange);
            card3 = new ColorettoCard(ColorettoCardColors.Gray);

            right = right.Add(card1);
            right = right.Add(card2);
            right = right.Add(card3);

            bool areEqual = left == right;
            Assert.IsTrue(areEqual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void CardCollection_EqualityTest3()
        {
            CardCollection left = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            left = left.Add(card1);
            left = left.Add(card2);
            left = left.Add(card3);

            CardCollection right = new CardCollection();

            card1 = new ColorettoCard(ColorettoCardColors.Blue);
            card2 = new ColorettoCard(ColorettoCardColors.Gray);
            card3 = new ColorettoCard(ColorettoCardColors.Gray);

            right = right.Add(card1);
            right = right.Add(card2);
            right = right.Add(card3);

            bool areEqual = left == right;
            Assert.IsTrue(areEqual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void CardCollection_UnEqualityTest()
        {
            CardCollection left = new CardCollection();

            ColorettoCard card1 = new ColorettoCard(ColorettoCardColors.Blue);
            ColorettoCard card2 = new ColorettoCard(ColorettoCardColors.Brown);
            ColorettoCard card3 = new ColorettoCard(ColorettoCardColors.Gray);

            left = left.Add(card1);
            left = left.Add(card2);
            left = left.Add(card3);

            CardCollection right = new CardCollection();

            card1 = new ColorettoCard(ColorettoCardColors.Blue);
            card2 = new ColorettoCard(ColorettoCardColors.Blue);
            card3 = new ColorettoCard(ColorettoCardColors.Gray);

            right = right.Add(card1);
            right = right.Add(card2);
            right = right.Add(card3);

            bool areEqual = left == right;
            Assert.IsFalse(areEqual);
        }
    }
}
