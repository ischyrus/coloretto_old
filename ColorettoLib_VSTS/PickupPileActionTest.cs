using Coloretto.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coloretto.Game;
using Coloretto.Player;
using System;
using System.Linq;
using CardManagement.Coloretto;

namespace ColorettoLib_VSTS
{
    [TestClass()]
    public class PickupPileActionTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod()]
        public void PickupPileActionTest_GameAddition()
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
            Assert.AreEqual<int>(3, target.PreviousActions.Count);
            Assert.AreEqual<Type>(typeof(PlaceCardAction), target.PreviousActions[0].Actions[0].GetType());
            Assert.AreEqual<Type>(typeof(PlaceCardAction), target.PreviousActions[1].Actions[0].GetType());
            Assert.AreEqual<Type>(typeof(PlaceCardAction), target.PreviousActions[2].Actions[0].GetType());
            Assert.AreEqual<int>(3, target.Piles[targetPile].Count);

            ActionResult result = target + PickupPileAction.Action(targetPile);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Game);
            Assert.AreSame(result.Data, result.Game.Hands[target.CurrentPlayerIndex]);
            Assert.IsInstanceOfType(result.Data, typeof(CardCollection));
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Game.Piles[targetPile]);

            foreach (var card in target.Piles[targetPile])
            {
                Assert.IsTrue(result.Game.Hands[target.CurrentPlayerIndex].Contains(card));
            }
        }

        [TestMethod]
        public void InvalidPickupPileActionTest_GameAddition()
        {
            Random random = new Random();
            int targetPile = random.Next(0, 4);

            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            ActionResult result = target + PickupPileAction.Action(targetPile);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Game);
            Assert.AreSame(target, result.Game);
            Assert.IsFalse(result.Success);
            Assert.IsInstanceOfType(result.Data, typeof(Exception));
        }
    }
}
