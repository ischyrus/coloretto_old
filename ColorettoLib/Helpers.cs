using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using CardManagement.Coloretto;

namespace Coloretto
{
    public static class Helpers
    {
        public static ReadOnlyCollection<T> CloneAndAppend<T>(ReadOnlyCollection<T> source, T addition)
        {
            if (source == null)
            {
                List<T> list = new List<T>(1);
                list.Add(addition);
                return list.AsReadOnly();
            }
            else
            {
                List<T> list = new List<T>(source.Count + 1);
                list.AddRange(source);
                list.Add(addition);
                return list.AsReadOnly();
            }
        }

        public static bool AllPilesPickedUp(IList<CardCollection> piles)
        {
            foreach (CardCollection collection in piles)
            {
                if (collection != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}