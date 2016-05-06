using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardGame.Models;
using System.Diagnostics;

namespace CardGame.Models 
{
    public class Deck
    {
        public List<Card> deck; // The list which will contain every card
        private List<Card> drawnCards; // The list which will contain every drawn card
        private List<Card> deckToAdd; // needed if user adds a deck
        public int numberOfDecks = 1; // must be known so it's impossible to remove a deck if it's only one left
        private Random randomNumber; // used to Shuffle cards

        
        public Deck()
        {
            string[] numbers = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" }; // used to set imageName
            string[] suits = { "hearts", "diamonds", "clubs", "spades" }; // used to set imageName
            string imageName;
            deck = new List<Card>();
            
            deckToAdd = new List<Card>();
            drawnCards = new List<Card>();
            randomNumber = new Random();

            for (int i = 0; i < 52; i++)
            { 
                imageName = numbers[i % 13] + "_of_" + suits[i / 13] + ".png"; // filename example: ace_of_hearts.png
                deck.Add(new Card(/*numbers[i % 13], suits[i / 13],*/ imageName, i));  
                deckToAdd.Add(new Card(/*numbers[i % 13], suits[i / 13],*/ imageName, i));
            }
            
        }

        //swapping every card in cards with a card from a random index number of cards
        public void ShuffleCards()
        {
            for ( int i = 0; i < deck.Count; i++)
            {
                int randomCardNr = randomNumber.Next(deck.Count);
                var cardPlaceHolder = deck[i];
                deck[i] = deck[randomCardNr];
                deck[randomCardNr] = cardPlaceHolder;
            }
        }

        // checks if cards is left in deck and depending upon that then returns null or currentcard
        public Card DrawTopCard()
        {
            if(deck.Count - 1 > -1)
            {
                drawnCards.Add(deck[deck.Count-1]);
                deck.RemoveAt(deck.Count-1);
                return drawnCards[drawnCards.Count-1];
            }
            else
            {
                return null;
            }
        }

        //adds all cards of deckToAdd to cards and add 1 to numberOfDecks
        public void  addOneDeck()
        {
            numberOfDecks++;
            deck.AddRange(deckToAdd);
           
        }

        // Removes every type of 52 cards one time aslong there is more than one deck in cards and subtract numberOfDecks
        public void RemoveOneDeck()
        {
            

            // makes sure so there is always at least 1 deck in cards
            if (numberOfDecks <= 1)
                return;

            numberOfDecks--;
            sortCards(); // to remove, the cards must be sorted
            int? index = null; // used to know which cards are not to be removed

            // Removes every card ID one time
            for (int i = 0; i < deck.Count; i++)
            {
                if (index != deck[i].ID)
                {
                    index = deck[i].ID;
                    deck.RemoveAt(i);
                };
            };   
        }

        // sorts every card accourding to their ID
        public void sortCards()
        {
           var newDeck = deck.OrderBy(x => x.ID).ToList();
           deck = newDeck;
        }
    }
}
