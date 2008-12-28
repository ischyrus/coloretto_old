using Coloretto.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coloretto.Game;
using Coloretto.Player;

namespace ColorettoLib_VSTS
{
    [TestClass()]
    public class DrawCardActionTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod()]
        public void DrawCardActionTest_GameAddition()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            ActionResult result = target + DrawCardAction.DefaultAction;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Game);
            Assert.IsTrue(result.Success);
            Assert.AreSame(result.Game.VisibleCard, result.Data);
            Assert.AreEqual<int>(target.CurrentPlayerIndex, result.Game.CurrentPlayerIndex);
        }
    }
}
