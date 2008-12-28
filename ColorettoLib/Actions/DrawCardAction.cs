using System;
using System.Collections.Generic;
using System.Text;
using Coloretto.Game;
using CardManagement.Coloretto;

namespace Coloretto.Actions
{
    /// <summary>
    /// Performs the action of drawing a card
    /// </summary>
    [Serializable]
    public class DrawCardAction : BaseAction
    {
        public const string DefaultDrawCardActionName = "Draw Card";
        private static DrawCardAction s_defaultAction = new DrawCardAction();

        public static DrawCardAction DefaultAction { get { return s_defaultAction; } }

        public override bool EndsTurn
        {
            get { return false; }
        }

        public DrawCardAction() : base(DefaultDrawCardActionName) { }

        protected override ActionResult Execute(ColorettoGame game)
        {
            ColorettoGame cloneGame = game.DrawCard();
            ActionResult result = new ActionResult(this.Name, cloneGame, game.CurrentPlayer, cloneGame.VisibleCard, cloneGame.VisibleCard != null);
            return result;
        }
    }
}
