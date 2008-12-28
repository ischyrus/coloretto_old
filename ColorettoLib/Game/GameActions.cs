using System;
namespace Coloretto.Game
{
    /// <summary>
    /// A list of actions that can be performedin the game.
    /// </summary>
    [Flags]
    public enum GameActions : int
    {
        None = 0,
        Draw = 1,
        PickupPile = 2,
        DrawOrPickupPile = 3,
        PlaceCard = 4
    }
}
