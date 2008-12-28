using System;
using System.Collections.Generic;
using System.Text;
using Coloretto.Game;

namespace Coloretto.Actions
{
    /// <summary>
    /// Represents the action of placing a card on a pile
    /// </summary>
    [Serializable]
    public class PlaceCardAction : BaseAction
    {
        public const string DefaultPlaceCardActionName = "Place card";

        private int _targetPile;

        public override bool EndsTurn
        {
            get { return true; }
        }

        public PlaceCardAction(int targetPile)
            : base(DefaultPlaceCardActionName + " on pile " + targetPile)
        {
            _targetPile = targetPile;
        }

        protected override ActionResult Execute(ColorettoGame game)
        {
            try
            {
                ColorettoGame newGame = game.PlaceCardOnPile(_targetPile);
                ActionResult result = new ActionResult(this.Name, newGame, game.CurrentPlayer, newGame.Piles[_targetPile], true);
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
            catch (Exception)
            {
                throw;
            }
        }

        public static PlaceCardAction Action(int targetPile)
        {
            return new PlaceCardAction(targetPile);
        }
    }
}
