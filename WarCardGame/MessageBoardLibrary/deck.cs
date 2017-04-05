using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardLibrary
{
    [DataContract]
    public class Deck
    {
        [DataMember]
        private List<Card> cards = new List<Card>();
        [DataMember]
        private int cardIdx;

        // Contructor 

        public Deck()
        {
            // Initialize member variables
            cardIdx = 0;
            repopulate();
        }

        // Public methods / properties

        public Card Draw()
        {
            // Return the next card if there is one and increment the card index
            if (cardIdx == cards.Count)
            {
                throw new System.IndexOutOfRangeException("The Shoe is empty. Please reset.");
            }

            return cards[cardIdx++];
        }

        public void Shuffle()
        {
            // Randomize the sequence of Card objects in the cards collection

            Random rng = new Random();

            Card temp;

            for (int i = 0; i < cards.Count; i++)
            {
                // Choose a random index
                int randIdx = rng.Next(cards.Count);

                if (randIdx != i)
                {
                    // Swap cards
                    temp = cards[i];
                    cards[i] = cards[randIdx];
                    cards[randIdx] = temp;
                }
            }

            // Reset the card index
            cardIdx = 0;
        }

        public int NumCards
        {
            get
            {
                // Return the number of cards remaining in the shoe (that haven't been drawn)
                return cards.Count - cardIdx;
            }
        }

       

        // Helper methods

        private void repopulate()
        {
            // Remove "old" cards
            cards.Clear();

            // Populate with new cards

            // For each suit...
            foreach (Card.SuitID s in Enum.GetValues(typeof(Card.SuitID)))
            {
                // For each rank with the current suit...
                foreach (Card.RankID r in Enum.GetValues(typeof(Card.RankID)))
                {
                    // Add a card with the current suit and rank
                    cards.Add(new Card(s, r));

                }
            }
            

            // Randomize the cards
            Shuffle();
        }
    }
}
