using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coloretto.State
{
    public class NetworkGameExecuter : IGameExecuter
    {
        public Coloretto.Actions.ActionResult DrawCard(GameStateController controller)
        {
            return null;
        }

        public Coloretto.Actions.ActionResult PlaceCard(GameStateController controller, int pile)
        {
            return null;
        }

        public Coloretto.Actions.ActionResult PickPile(GameStateController controller, int pile)
        {
            return null;
        }
    }
}
