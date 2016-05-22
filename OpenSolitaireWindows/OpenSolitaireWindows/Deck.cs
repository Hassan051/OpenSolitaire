﻿/* ©2016 Hathor Gaia 
 * http://HathorsLove.com
 * 
 * Licensed Under GNU GPL 3:
 * http://www.gnu.org/licenses/gpl-3.0.html
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OpenSolitaireMG {


    
    class Deck {
        
        public List<Card> cards = new List<Card>();
        private Texture2D _cardBack;
        private SpriteBatch _spriteBatch;
        


        #region methods
        public Deck(Texture2D cardBack, SpriteBatch sb) {
            _cardBack = cardBack;
            _spriteBatch = sb;
        }

        public void Clear() { cards.Clear(); }


        //populate your deck with a typical set of cards
        public void freshDeck() {

            cards.Clear();

            foreach (Suit mySuit in Enum.GetValues(typeof(Suit))) {
                
                foreach (Rank myRank in Enum.GetValues(typeof(Rank))) {

                    cards.Add(new Card(myRank, mySuit, _cardBack, _spriteBatch));

                }

            }

        }

        public void debugDeck() {

            Console.WriteLine("===");

            if (cards.Count > 0) { 
                foreach (Card card in cards) {

                    String strFaceUp = (card.faceUp ? "face up" : "face down");
                    Console.WriteLine(card.ZIndex.ToString("00") + ": " + card.rank + " of " + card.suit + " (" + strFaceUp + ")");

                }
            }
            else { Console.WriteLine("(empty hand)"); }

        }

        //makes a smaller random deck for testing
        public void testDeck(int numCards) {

            cards.Clear();

            Deck subDeck = new Deck(_cardBack, _spriteBatch);
            subDeck.freshDeck();
            subDeck.shuffle();

            if (numCards <= subDeck.Count) {

                for (int i=0; i < numCards; i++) {
                    cards.Add(subDeck.drawCard());
                }

            }

            subDeck = null;

        }

        public void shuffle() {

            //wait a few ms to avoid seed collusion
            Thread.Sleep(30);

            Random rand = new Random();
            for (int i = cards.Count - 1; i > 0; i--) {
                int randomIndex = rand.Next(i + 1);
                Card tempCard = cards[i];
                cards[i] = cards[randomIndex];
                cards[randomIndex] = tempCard;
            }
        }

        /// <summary>
        /// just picks the top card on the deck and returns it
        /// </summary>
        /// <returns></returns>
        public Card drawCard() {

            if (cards.Count > 0) {

                Card topCard = cards[cards.Count - 1];
                cards.RemoveAt(cards.Count - 1);
                return topCard;

            }
            else { return null; }

        }

        /// <summary>
        /// adds card to your hand or deck
        /// </summary>
        /// <param name="card"></param>
        public void addCard(Card card) {

            cards.Add(card);

        }

        /// <summary>
        /// pulls a specific card from your hand
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <returns>Card if found, null otherwise</returns>
        public Card playCard(Rank rank, Suit suit) {
            
            foreach (Card card in cards){
                
                if ((card.rank == rank) && (card.suit == suit)) {

                    cards.Remove(card);
                    return card;

                }

            }

            return null;            

        }

        // TODO: have no idea if this works or not
        public Card playCard(int cardIndex) {
            
            if (cards.Contains(cards[cardIndex])) {
                Card card = cards[cardIndex];
                cards.RemoveAt(cardIndex);
                return card;
            }
            else { return null; }
        }


        #endregion


        #region properties

        public int Count { get { return cards.Count; } }
        public Texture2D cardBack {

            get { return _cardBack; }

        }

        #endregion

    }
    

}
