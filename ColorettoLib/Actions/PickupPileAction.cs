using System;
using System.Collections.Generic;
using System.Text;
using Coloretto.Game;
using Coloretto.Player;

namespace Coloretto.Actions
{
    /// <summary>
    /// Represents the action of picking up a pile
    /// </summary>
    [Serializable]
    public class PickupPileAction : BaseAction
    {
        public const string DefaultPickupPileActionName = "Pickup Pile";

        private int _targetPile;

        public override bool EndsTurn
        {
            get { return true; }
        }

        public PickupPileAction(int pileToPickup)
            : base(DefaultPickupPileActionName + pileToPickup)
        {
            _targetPile = pileToPickup;
        }

        protected override ActionResult Execute(Coloretto.Game.ColorettoGame game)
        {
            try
            {
                int playerIndex = game.CurrentPlayerIndex;
                ColorettoGame newGame = game.PickupPile(_targetPile);
                ActionResult result = new ActionResult(this.Name, newGame, game.CurrentPlayer, newGame.Hands[playerIndex], true);
                return result;
            }
            catch (ArgumentException ex)
            {
                return new ActionResult(this.Name, game, game.CurrentPlayer, ex, false);
            }
            catch (InvalidOperationException ex)
            {
                return new ActionResult(this.Name, game, game.CurrentPlayer, ex, false);
            }
            catch
            {
                throw;
            }
        }

        public static PickupPileAction Action(int targetPile)
        {
            return new PickupPileAction(targetPile);
        }
    }
}
