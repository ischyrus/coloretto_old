/*
 * Created by SharpDevelop.
 * User: sscherm
 * Date: 6/30/2008
 * Time: 2:22 PM
 */

using System;

namespace CardManagement.Coloretto
{
    /// <summary>
    /// An enumeration of all the colors available 
    /// </summary>
    public enum ColorettoCardColors : int
    {
        None = 0,
        Green = 1,
        Brown = 2,
        Gray = 3,
        Blue = 4,
        Orange = 5,
        Yellow = 6,
        Pink = 7
    }

    /// <summary>
    /// An enumeration of the card types
    /// </summary>
    public enum ColorettoCardTypes : int
    {
        Unknown = 0,
        Color = 1,
        Plus2 = 2,
        Wild = 3,
        LastCycle = 4
    }
}
