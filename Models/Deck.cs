using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;




namespace CardGame.Models
{
    public class Deck
    {
        private List<Card> deck; // contains all cards
        private List<Card> drawnCards; // contains all drawn cards
        private List<Card> deckToAdd; // used if user add more decks
        private Random randomNumber; // used to Shuffle cards

        
        public Deck()
        {
            string[] numbers = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };
            string[] suits = { "hearts", "diamonds", "clubs", "spades" }; 
            string imageName;
            deck = new List<Card>();
            deckToAdd = new List<Card>();
            drawnCards = new List<Card>();
            randomNumber = new Random();
          

            for (int i = 0; i < 52; i++)
            { 
                imageName = numbers[i % 13] + "_of_" + suits[i / 13] + ".png"; // filename example: ace_of_hearts.png
                deck.Add(new Card(imageName, i));
            }

            deckToAdd.AddRange(deck);
        }

        //swaps every card in deck to random index
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

        // moves cards from deck to drawnCards
        public Card DrawTopCard()
        {
            if(deck.Count > 0) 
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

        //adds all cards of deckToAdd to top of deck
        public void  addOneDeck()
        {
            deck.AddRange(deckToAdd); 
        }

       // Copies each card, starting with smallest cardID to bottom of deck then upwards
       // then deletes the old location of the copied card
        public void sortCards()
        {
            int cardID = 0;
            int index = 0;
            int indexToRemove;
            if(drawnCards != null)
                deck.AddRange(drawnCards);
            drawnCards.Clear();

            //sorts the decks separately cardID: 1,2,3,1,2,3 etc
            while (deck.Count > index)
            {
                var cardPlaceholder = deck.Where(i => i.ID == cardID).LastOrDefault();
                deck.Insert(index, cardPlaceholder);
                indexToRemove = deck.FindLastIndex(i => i.ID == deck[index].ID);
                deck.RemoveAt(indexToRemove);
                index++;
                cardID = ((cardID+1) % 52);
            }

            //sorts the decks together cardID: 1,1,2,2,3,3 etc (no test)
            /*currentCard = 0;
            var newDeck = deck.OrderBy(x => x.OrderNumber).ToList();
            deck = newDeck; */
        }

        // for the controller
        public int countCards()
        {
            return deck.Count();
        }
    }
}
