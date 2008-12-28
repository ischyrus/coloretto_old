using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coloretto.Actions;
using Coloretto.Game;

namespace Coloretto.State
{
    public interface IGameExecuter
    {
        ActionResult DrawCard(GameStateController controller);
        ActionResult PlaceCard(GameStateController controller, int pile);
        ActionResult PickPile(GameStateController controller, int pile);
    }

    public class LocalGameExecuter : IGameExecuter
    {
        public ActionResult DrawCard(GameStateController controller)
        {
            ActionResult result = controller.CurrentGame + DrawCardAction.DefaultAction;
            if (result.Success)
            {
                controller.CurrentGame = result.Game;
            }
            return result;
        }

        public ActionResult PlaceCard(GameStateController controller, int pile)
        {
            ActionResult result = controller.CurrentGame + PlaceCardAction.Action(pile);
            if (result.Success)
            {
                controller.CurrentGame = result.Game;
            }
            return result;
        }

        public ActionResult PickPile(GameStateController controller, int pile)
        {
            ActionResult result = controller.CurrentGame + PickupPileAction.Action(pile);
            if (result.Success)
            {
                controller.CurrentGame = result.Game;
            }
            return result;
        }
    }
}
