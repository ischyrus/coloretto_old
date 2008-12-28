using Coloretto.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coloretto.Game;
using Coloretto.Player;
using CardManagement.Coloretto;
using System.Linq;
using System;

namespace ColorettoLib_VSTS
{
    [TestClass()]
    public class PlaceCardActionTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        ///A test for PerformAction
        ///</summary>
        [TestMethod()]
        public void PlaceCardActionTest_GameAddition()
        {
            Random random = new Random();
            int targetPile = random.Next(0, 4);

            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            target = target.DrawCard();
            ActionResult result = target + PlaceCardAction.Action(targetPile); 
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Game);
            Assert.IsInstanceOfType(result.Data, typeof(CardCollection));
            Assert.AreSame(result.Data, result.Game.Piles[targetPile]);
            Assert.IsTrue(result.Success);

            Assert.IsNotNull(target.VisibleCard);
            Assert.IsNull(result.Game.VisibleCard);
            Assert.AreNotEqual<int>(target.Piles[targetPile].Count, result.Game.Piles[targetPile].Count);
            Assert.AreNotEqual<int>(target.CurrentPlayerIndex, result.Game.CurrentPlayerIndex);
        }

        [TestMethod]
        public void InvalidPlaceCardActionTest_GameAddition()
        {
            Random random = new Random();
            int targetPile = random.Next(0, 4);

            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);
            target = target.DrawCard();
            target = (target + PlaceCardAction.Action(targetPile)).Game;
            target = target.DrawCard();
            target = (target + PlaceCardAction.Action(targetPile)).Game;
            target = target.DrawCard();
            target = (target + PlaceCardAction.Action(targetPile)).Game;
            Assert.AreEqual<int>(3, target.Piles[targetPile].Count);

            target = target.DrawCard();
            ActionResult result = target + PlaceCardAction.Action(targetPile);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Game);
            Assert.AreSame(target, result.Game);
            Assert.IsFalse(result.Success);
            Assert.IsInstanceOfType(result.Data, typeof(Exception));
        }
    }
}
