using System;
using System.Collections.Generic;
using System.Text;

namespace CardManagement.Standard
{
    /// <summary>
    /// The types of Standard cards. (Heart, spade, etc)
    /// </summary>
    public enum StandardCardTypes : int
    {
        Unknown = 0,
        Heart = 1,
        Spade = 2,
        Diamond = 3,
        Clover = 4,
        Joker = 5
    }

    /// <summary>
    /// The possible values that a StandardCard can have
    /// </summary>
    public enum StandardCardValues 
    {
        None = 0,
        Ace = 1,   // 0000 0001
        Two = 2,   // 0000 0010
        Three = 3, // 0000 0011
        Four = 4,  // 0000 0100
        Five = 5,  // ...
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,  // 0000 1010

        /*
         * The jack, queen, and king should all have the value of 10 however because 
         * of how enumerations work if you were to set the values to 10 they would
         * always look like "Ten" and never like "Jack", "Queen", or "King" simply because
         * Ten is listed first in this enumeration.
         * 
         * To work around this we are going to keep the right 4 bits equal to 10 and then use the bits 
         * bits to the left of it to indicate if it is a Jack, Queen, or King. When doing math
         * we'll have to be sure to set this bits to 0 to end up with 10.
         */
        Jack = 26, // 0001 1010
        Queen = 42,// 0010 1010
        King = 74  // 0100 1010
    }
}
