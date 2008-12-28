using System;
using System.Collections.Generic;
using System.Text;

namespace CardManagement
{
    /// <summary>
    /// Manages a deck of cards. Essentially  this is an immutable queue specially for cards
    /// </summary>
    /// <typeparam name="T">The type of card that this deck holds.</typeparam>
    [Serializable]
    public class Deck<T> where T : Card
    {
        #region DeckNode -- Private class
        [Serializable]
        private class DeckNode<U> where U : T
        {
            public U Card;
            public DeckNode<U> Next;

            public DeckNode(U card)
            {
                Card = card;
            }

            public DeckNode(U card, DeckNode<U> next)
                : this(card)
            {
                Next = next;
            }
        }
        #endregion

        #region Private Variables
        /// <summary>
        /// A reference to the first card in the deck.
        /// The <seealso cref="DeckNode"/> property Card has the reference to the card on the top of the deck.
        /// The <seealso cref="DeckNode"/> property Next has the reference to the next DeckNode which contains the reference to the second card on the deck.
        /// </summary>
        private DeckNode<T> _firstDeckNode;

        /// <summary>
        /// The original starting size of the deck.
        /// <remarks>Set after the deck has been created using the public constructor.</remarks>
        /// </summary>
        private int _originalSize;

        /// <summary>
        /// The size of the deck
        /// </summary>
        private int _size; 
        #endregion

        #region Properties
        /// <summary>
        /// Get a flag indicating if the deck is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return _firstDeckNode == null; }
        }

        /// <summary>
        /// Get the card on top of the deck
        /// </summary>
        public T PreviewDraw
        {
            get { return _firstDeckNode != null ? _firstDeckNode.Card : null; }
        }

        /// <summary>
        /// Get the original size of the deck
        /// </summary>
        public int OriginalSize
        {
            get { return _originalSize; }
            protected set { _originalSize = value; }
        }

        /// <summary>
        /// Get the size of the deck;
        /// </summary>
        public int Size
        {
            get { return _size; }
            private set { _size = value; }
        } 
        #endregion

        #region Constructors
        /// <summary>
        /// Private constructor preventing creation without data.
        /// </summary>
        private Deck() { }

        /// <summary>
        /// Constructor that creates a new deck from an enumeration
        /// </summary>
        public Deck(IEnumerable<T> createDeck)
        {
            IEnumerator<T> enumerator = createDeck.GetEnumerator();
            if (createDeck != null && enumerator.MoveNext())
            {
                Deck<T>.DeckNode<T> lastNode = new Deck<T>.DeckNode<T>(enumerator.Current);
                _firstDeckNode = lastNode;
                Size++;

                while (enumerator.MoveNext())
                {
                    lastNode.Next = new Deck<T>.DeckNode<T>(enumerator.Current);
                    lastNode = lastNode.Next;
                    Size++;
                }

                OriginalSize = Size;
            }
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// Draw a card
        /// </summary>
        /// <returns>The drawn card</returns>
        public Deck<T> Draw(out T card)
        {
            card = _firstDeckNode.Card;
            Deck<T> clone = Clone();

            // Remove the front one and assign the next one.
            clone._firstDeckNode = _firstDeckNode.Next;
            clone.Size -= 1;

            return clone;
        } 
        #endregion

        #region Private Methods
        /// <summary>
        /// Clone the deck
        /// </summary>
        /// <returns></returns>
        private Deck<T> Clone()
        {
            Deck<T> deck = new Deck<T>();
            deck._firstDeckNode = _firstDeckNode;
            deck.OriginalSize = OriginalSize;
            deck.Size = Size;
            return deck;
        } 
        #endregion
    }
}
