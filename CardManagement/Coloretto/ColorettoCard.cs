/*
 * Created by SharpDevelop.
 * User: sscherm
 * Date: 6/30/2008
 * Time: 2:21 PM
 */

using System;

namespace CardManagement.Coloretto
{
    /// <summary>
    /// Description of ColorettoCard.
    /// </summary>
    [Serializable]
    public class ColorettoCard : Card
    {
        /// <summary>
        /// Get the color of the card
        /// </summary>
        public ColorettoCardColors Color
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the type of the card
        /// </summary>
        public ColorettoCardTypes CardType
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the fixed position in the deck for this card.
        /// The card will be in this position everytime that the deck is shuffled.
        /// </summary>
        public int? FixedDeckPosition
        {
            get;
            private set;
        }

        /// <summary>
        /// private default to prevent uninitizlied instances.
        /// </summary>
        private ColorettoCard()
            : base()
        {
        }

        /// <summary>
        /// Create a card of type and color.
        /// </summary>
        /// <param name="type">The type of card. (The Unknown type will cause an ArgumentException)</param>
        /// <param name="color">For cards of color type this will be the color of the card. For non-color type cards use the 'none' value.</param>
        public ColorettoCard(ColorettoCardTypes type, ColorettoCardColors color)
            : this()
        {
            CardType = type;
            Color = color;

            ValidateCard();
        }

        /// <summary>
        /// Create a card that is of type and in a fixed position
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fixedPosition"></param>
        public ColorettoCard(ColorettoCardTypes type, int fixedPosition)
            : this(type)
        {
            FixedDeckPosition = fixedPosition;
        }

        /// <summary>
        /// Create a card of color.
        /// </summary>
        /// <param name="color">The color of the card. (The color enum 'none' is not valid in this scenerio)</param>
        public ColorettoCard(ColorettoCardColors color)
            : this()
        {
            CardType = ColorettoCardTypes.Color;
            Color = color;

            ValidateCard();
        }

        /// <summary>
        /// Create a card of type.
        /// NOTE: Do not use this overload if you are creating a 'color' type card.
        /// </summary>
        /// <param name="type">The type of card.</param>
        public ColorettoCard(ColorettoCardTypes type)
            : this()
        {
            CardType = type;
            Color = ColorettoCardColors.None;

            ValidateCard();
        }

        /// <summary>
        /// Clone this card.
        /// </summary>
        /// <returns></returns>
        public override Card Clone()
        {
            ColorettoCard clone = new ColorettoCard();
            base.InternalClone(clone);
            clone.Color = Color;
            clone.CardType = CardType;
            clone.FixedDeckPosition = FixedDeckPosition;
            return clone;
        }

        /// <summary>
        /// Validate the state of the card.
        /// </summary>
        private void ValidateCard()
        {
            if (CardType == ColorettoCardTypes.Color)
            {
                if (Color == ColorettoCardColors.None)
                {
                    throw new ArgumentException("A ColorettoCard cannot be created that is of color type 'None'");
                }
            }
            else if (CardType == ColorettoCardTypes.Unknown)
            {
                throw new ArgumentException("Unable to create ColorettoCard because the type of the card is unknown.");
            }
            else if (Color != ColorettoCardColors.None)
            {
                throw new ArgumentException("Coloretto +2 and Wild cards cannot have a color associated to them.");
            }
        }

        public override string ToString()
        {
            switch (this.CardType)
            {
                case ColorettoCardTypes.Color:
                    return string.Format("{0} card ({1})", Color, Id);
                case ColorettoCardTypes.Plus2:
                    return string.Format("Plus 2 ({0})", Id);
                case ColorettoCardTypes.Wild:
                    return string.Format("Wild ({0})", Id);
                case ColorettoCardTypes.LastCycle:
                    return string.Format("Last cycle ({0})", Id);
                default:
                    return string.Format("Unknown card type. ({0})", Id);
            }
        }

        public string ToShortString()
        {
            switch (this.CardType)
            {
                case ColorettoCardTypes.Color:
                    return string.Format("{0} card", Color);
                case ColorettoCardTypes.Plus2:
                    return "Plus 2";
                case ColorettoCardTypes.Wild:
                    return "Wild";
                case ColorettoCardTypes.LastCycle:
                    return "Last Cycle";
                default:
                    return "Unknown card";
            }
        }
    }
}
