using System;
using System.Collections.Generic;
using System.Text;
using CardManagement.Coloretto;
using System.Collections;
using System.Collections.ObjectModel;

namespace CardManagement.Coloretto
{
    /// <summary>
    /// A collection of coloretto cards
    /// </summary>
    [Serializable]
    public class CardCollection : IEnumerable<ColorettoCard>
    {
        #region Private variables
        /// <summary>
        /// A reference to a card collection that is empty
        /// </summary>
        private static readonly CardCollection _empty = new CardCollection();

        /// <summary>
        /// The internal array of cards
        /// </summary>
        private ColorettoCard[] _internalArray;

        /// <summary>
        /// The score of the collection
        /// </summary>
        private int _score = 0;
        #endregion

        #region Properties
        /// <summary>
        /// The number of cards on the list
        /// </summary>
        public int Count
        {
            get { return _internalArray.Length; }
        }

        /// <summary>
        /// An empty card collection
        /// </summary>
        public static CardCollection Empty
        {
            get { return _empty; }
        }

        /// <summary>
        /// Get if this collection is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        /// <summary>
        /// Get or set the score of the collection
        /// </summary>
        public int Score
        {
            get { return _score; }
            private set { _score = value; }
        }

        /// <summary>
        /// get or set the value at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ColorettoCard this[int index]
        {
            get
            {
                return _internalArray[index];
            }
            set
            {
                _internalArray[index] = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create an empty collection
        /// </summary>
        public CardCollection()
        {
            _internalArray = new ColorettoCard[0];
        }

        /// <summary>
        /// Constructor used when adding a card to the collection
        /// </summary>
        /// <param name="old"></param>
        /// <param name="addedCard"></param>
        private CardCollection(CardCollection old, ColorettoCard addedCard)
        {
            _internalArray = new ColorettoCard[old.Count + 1];
            _internalArray[old.Count] = addedCard;

            old._internalArray.CopyTo(_internalArray, 0);

            UpdateScore();
        }

        /// <summary>
        /// Constructor used when removing a card from the collection
        /// </summary>
        /// <param name="old"></param>
        /// <param name="indexToRemove"></param>
        private CardCollection(CardCollection old, int indexToRemove)
        {
            _internalArray = new ColorettoCard[old.Count - 1];
            int index = 0;
            for (int i = 0; i < indexToRemove; i++, index++)
                _internalArray[index] = old._internalArray[i];
            for (int i = indexToRemove + 1; i < old._internalArray.Length; i++, index++)
                _internalArray[index] = old._internalArray[i];
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Add item to the list
        /// </summary>
        /// <param name="item"></param>
        public CardCollection Add(ColorettoCard item)
        {
            CardCollection newCollection = new CardCollection(this, item);
            return newCollection;
        }

        /// <summary>
        /// Check to see if obj is equal to this collection.
        /// NOTE: This only checks the score and not the hand configuration.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            CardCollection other = obj as CardCollection;
            if (other == null)
                return false;

            bool result = this == other;
            return result;
        }

        /// <summary>
        /// Get the enumerator for the cards
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ColorettoCard> GetEnumerator()
        {
            foreach (ColorettoCard card in _internalArray)
                yield return card;
        }

        /// <summary>
        /// Get the non-generic enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalArray.GetEnumerator();
        }

        /// <summary>
        /// Get the has
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Remove a card from the collection
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public CardCollection Remove(ColorettoCard card)
        {
            int index = IndexOf(card);
            if (index != -1)
            {
                CardCollection newCollection = new CardCollection(this, index);
                return newCollection;
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ColorettoCard card in this)
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(card.ToShortString());
            }
            return sb.ToString();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Get the index of card in the collection
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private int IndexOf(ColorettoCard card)
        {
            for (int i = 0; i < _internalArray.Length; i++)
                if (_internalArray[i].Equals(card))
                    return i;
            return -1;
        }

        /// <summary>
        /// Update the score
        /// </summary>
        private void UpdateScore()
        {
            // Counter variables used for taling up the score
            List<int> colorCounts = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0 });
            int newScore = 0;
            int numberOfWilds = 0;

            // Count the colors, +2, and wild cards
            for (int i = 0; i < _internalArray.Length; i++)
            {
                if (_internalArray[i].CardType == ColorettoCardTypes.Color)
                    colorCounts[(int)_internalArray[i].Color - 1] += 1; // Index is found by -1 for color value offset and -1 to offset for
                else if (_internalArray[i].CardType == ColorettoCardTypes.Plus2)
                    newScore += 2;
                else if (_internalArray[i].CardType == ColorettoCardTypes.Wild)
                    numberOfWilds++;
            }

            // sort the color tallies from highest to lowest
            colorCounts.Sort((Comparison<int>)delegate(int l, int r) { return r - l; });

            // Distribute the wild cards in a manner that maximizes the score it gives us.
            for (int i = 0; i < 3; i++)
            {
                int numberToUse = Math.Min(Constants.DefaultMaxNumberOfScoredCardsPerColor - colorCounts[i], numberOfWilds);
                if (numberToUse > 0)
                {
                    colorCounts[i] += numberToUse;
                    numberOfWilds -= numberToUse;
                }
            }

            // Any remaining wilds will be placed on the first pile.
            if (numberOfWilds > 0)
            {
                colorCounts[0] += numberOfWilds;
                numberOfWilds = 0;
            }

            // Add the positives
            for (int i = 0; i < 3; i++)
                newScore += CalculateScore(colorCounts[i]);

            // Subtract the negatives
            for (int i = 3; i < colorCounts.Count; i++)
                newScore -= CalculateScore(colorCounts[i]);

            // set the new score
            Score = newScore;
        }

        /// <summary>
        /// Determines the score for cardCount
        /// </summary>
        /// <param name="cardCount"></param>
        /// <returns></returns>
        private static int CalculateScore(int cardCount)
        {
            if (cardCount > Constants.DefaultMaxNumberOfScoredCardsPerColor)
                return Constants.DefaultMaxNumberOfScoredCardsPerColor + CalculateScore(Constants.DefaultMaxNumberOfScoredCardsPerColor - 1);
            if (cardCount > 0)
                return cardCount + CalculateScore(cardCount - 1);
            else
                return 0;
        }
        #endregion

        #region Operator overloads
        /// <summary>
        /// Allows for the addition of two CardCollections.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static CardCollection operator +(CardCollection left, CardCollection right)
        {
            CardCollection newCollection = new CardCollection();

            newCollection._internalArray = new ColorettoCard[left.Count + right.Count];
            left._internalArray.CopyTo(newCollection._internalArray, 0);
            right._internalArray.CopyTo(newCollection._internalArray, left.Count);

            newCollection.UpdateScore();
            return newCollection;
        }

        /// <summary>
        /// Performs and equal comparison. 
        /// NOTE: This comparison is based on score and not hand configuration
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(CardCollection left, CardCollection right)
        {
            bool isRightNull = object.ReferenceEquals(right, null);
            bool isLeftNull = object.ReferenceEquals(left, null);
            if (isRightNull != isLeftNull)
            {
                return false;
            }
            else if (isRightNull == true)
            {
                return true;
            }

            bool result = left.Score == right.Score;
            return result;
        }

        /// <summary>
        /// Peforms a not qual comparison
        /// NOTE: This comparison is based on score and not hand configuration
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(CardCollection left, CardCollection right)
        {
            return !(left == right);
        }
        #endregion
    }
}
