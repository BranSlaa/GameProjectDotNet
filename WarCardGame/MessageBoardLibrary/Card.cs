using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; // WCF Data Contract Types

namespace MessageBoardLibrary
{
    [DataContract]
    public class Card
    {
        // Enumeration types
        
        public enum SuitID
        {
            Clubs, Diamonds, Hearts, Spades
        };

        public enum RankID
        {
            Ace, King, Queen, Jack, Ten, Nine, Eight, Seven, Six, Five, Four, Three, Two
        };

        // Public properties / methods
        [DataMember]
        public SuitID Suit { get; private set; }
        [DataMember]
        public RankID Rank { get; private set; }

        [DataMember]
        public String Name
        {
            get
            {
                return Rank.ToString() + " of " + Suit.ToString();
            }
            private set { }
        }

        // Constructor

        public Card(SuitID s, RankID r)
        {
            Suit = s;
            Rank = r;
        }
    }
}
