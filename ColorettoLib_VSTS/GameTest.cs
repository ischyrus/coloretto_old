using Coloretto.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Coloretto.Player;
using System;
using Coloretto;
using CardManagement.Coloretto;
using System.IO.Compression;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Coloretto.Actions;

namespace ColorettoLib_VSTS
{
    [TestClass()]
    public class GameTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        ///A test for Game Constructor
        ///</summary>
        [TestMethod()]
        public void Game_BasicConstructorTest()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            using(FileStream fs = new FileStream(@"test.zip", FileMode.Create))
            using (GZipStream stream = new GZipStream(fs, CompressionMode.Compress))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, target);
            }

            Profile[] loadedProfiles = target.Players.ToArray();
            Assert.AreEqual<int>(4, loadedProfiles.Length);
            Assert.AreSame(player1, loadedProfiles[0]);
            Assert.AreSame(player2, loadedProfiles[1]);
            Assert.AreSame(player3, loadedProfiles[2]);
            Assert.AreSame(player4, loadedProfiles[3]);
        }

        /// <summary>
        /// Makes sure that the only action availble on game start is to draw
        /// </summary>
        [TestMethod]
        public void Game_AvailableActions1()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            Assert.AreEqual<GameActions>(GameActions.Draw, target.AvailableActions);
        }

        /// <summary>
        /// Makes sure that after a single card has been placed that the player can pick a pile if they want as an action
        /// </summary>
        [TestMethod]
        public void Game_AvaliableActions2()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(2);

            Assert.AreEqual<GameActions>(GameActions.DrawOrPickupPile, target.AvailableActions);
        }

        /// <summary>
        /// Makes sure that once all piles are full that the only available action is to pick a pile
        /// </summary>
        [TestMethod]
        public void Game_AvaliableActions3()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);

            target = target.DrawCard();
            target = target.PlaceCardOnPile(1);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(1);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(1);

            target = target.DrawCard();
            target = target.PlaceCardOnPile(2);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(2);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(2);

            target = target.DrawCard();
            target = target.PlaceCardOnPile(3);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(3);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(3);

            Assert.AreEqual<GameActions>(GameActions.PickupPile, target.AvailableActions);
        }

        /// <summary>
        ///A test for PlaceCardOnPile
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Game_PlaceCardOnPileTest()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            target = target.PlaceCardOnPile(0);
            target = target.PlaceCardOnPile(0);
            target = target.PlaceCardOnPile(0);
            target = target.PlaceCardOnPile(0);
        }

        /// <summary>
        ///A test for PickupPile
        ///</summary>
        [TestMethod()]
        public void Game_PickupPileTest()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);

            int numberOfPlayersWithNonZeroScore = target.Hands.Where(cc => cc.Score > 0).Count();
            Assert.IsTrue(numberOfPlayersWithNonZeroScore == 0, "Somehow someone started with a non-zero score.");

            target = target.PickupPile(0);
            
            CardCollection nullPileBecauseItWasPickedUp = target.Piles.First();
            Assert.IsNull(nullPileBecauseItWasPickedUp);

            numberOfPlayersWithNonZeroScore = target.Hands.Where(cc => cc.Score > 0).Count();
            Assert.IsTrue(numberOfPlayersWithNonZeroScore > 0, "Someone picked up a pile but nobody has a non-zero score");
        }

        /// <summary>
        /// A test to make sure previous actions are bineg set
        /// </summary>
        [TestMethod]
        public void Game_PreviousActionsTest()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();

            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);
            target = target.DrawCard();
            target = (target + PlaceCardAction.Action(0)).Game;
            target = target.DrawCard();
            target = (target + PlaceCardAction.Action(1)).Game;
            target = target.DrawCard();
            target = (target + PlaceCardAction.Action(2)).Game;

            Assert.AreEqual<int>(3, target.PreviousActions.Count);
        }

        /// <summary>
        /// Fills up all of the piles and then picks up each pile to verify that the next cycle begins as expected
        /// </summary>
        [TestMethod]
        public void Game_FullCycle()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(0);

            target = target.DrawCard();
            target = target.PlaceCardOnPile(1);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(1);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(1);

            target = target.DrawCard();
            target = target.PlaceCardOnPile(2);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(2);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(2);

            target = target.DrawCard();
            target = target.PlaceCardOnPile(3);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(3);
            target = target.DrawCard();
            target = target.PlaceCardOnPile(3);

            Assert.AreEqual<int>(1, target.Cycle);
            Assert.AreEqual<int>(13, target.Turn);
            Assert.AreEqual<GameActions>(GameActions.PickupPile, target.AvailableActions);

            target = target.PickupPile(0);
            target = target.PickupPile(1);
            target = target.PickupPile(2);
            target = target.PickupPile(3);

            // Make sure that a new cycle is created
            Assert.AreEqual<int>(2, target.Cycle);
            Assert.AreEqual<int>(17, target.Turn);
            Assert.AreEqual<GameActions>(GameActions.Draw, target.AvailableActions);
        }

        [TestMethod]
        public void Game_FullRound()
        {
            Profile player1 = new Profile();
            Profile player2 = new Profile();
            Profile player3 = new Profile();
            Profile player4 = new Profile();
            ColorettoGame target = new ColorettoGame(player1, player2, player3, player4);

            // Loop through 5 times. 
            // There are 76 cards. The last cycle card should be the 61st.
            // Each cycle, which is in this for loop, uses 12 cards. 
            // 12 * 5 = 60 cards. So, the next cycle will be in the 6th loop
            for (int i = 0; i < 6; i++)
            {
                if (i == 5)
                {
                    Assert.AreEqual<int>(1, target.Round);
                }

                target = target.DrawCard();
                target = target.PlaceCardOnPile(0);
                target = target.DrawCard();
                target = target.PlaceCardOnPile(0);

                if (i == 5)
                {
                    Assert.AreEqual<int>(1, target.Round);
                    Assert.IsTrue(target.IsLastCycleForRound);
                }

                target = target.DrawCard();
                target = target.PlaceCardOnPile(0);

                target = target.DrawCard();
                target = target.PlaceCardOnPile(1);
                target = target.DrawCard();
                target = target.PlaceCardOnPile(1);
                target = target.DrawCard();
                target = target.PlaceCardOnPile(1);

                target = target.DrawCard();
                target = target.PlaceCardOnPile(2);
                target = target.DrawCard();
                target = target.PlaceCardOnPile(2);
                target = target.DrawCard();
                target = target.PlaceCardOnPile(2);

                target = target.DrawCard();
                target = target.PlaceCardOnPile(3);
                target = target.DrawCard();
                target = target.PlaceCardOnPile(3);
                target = target.DrawCard();
                target = target.PlaceCardOnPile(3);

                target = target.PickupPile(0);
                target = target.PickupPile(1);
                target = target.PickupPile(2);
                target = target.PickupPile(3);

                if (i == 5)
                {
                    Assert.AreEqual<int>(2, target.Round);
                }
            }
        }
    }
}
