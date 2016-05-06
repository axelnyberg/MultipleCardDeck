using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardGame.Models;

namespace CardGame.Controllers
{   
    // controller which handles all the buttons and images of view
    public class CardDeckController : Controller
    {
      
        static Deck deck;
        static Card card; 
        static string previousCardImageName; // needed when the last drawn card's image must be set again

        // creates the deck and sets the images the view should display
        public ActionResult Index()
        {
            deck = new Deck();
            ViewData["totalCards"] = deck.countCards(); 
            ViewData["deckImageName"] = "back.jpg"; // Backside image of a deck 
            ViewData["cardImageName"] = "White.png"; //card image of view is white when applications start because no cards are drawn
            previousCardImageName = "White.png"; // if not set, it won't reset everytime the application restarts 
            return View("Index");     
        }
        
        // Calls method of the same name of deck object and sets new images if deck is not emty or no card left
        public ActionResult DrawTopCard()
        {
            card = deck.DrawTopCard();
            ViewData["totalCards"] = deck.countCards();

            if (card != null && deck.countCards() > 0)
            {
                ViewData["cardImageName"] = card.ImageName;
                ViewData["deckImageName"] = "back.jpg";
                previousCardImageName = card.ImageName;
            }
            else if(card != null && deck.countCards() == 0)
            {
                ViewData["cardImageName"] = card.ImageName;
                ViewData["deckImageName"] = "White.png";
                previousCardImageName = card.ImageName;
            }
            else
            {
                ViewData["cardImageName"] = previousCardImageName;  
                ViewData["deckImageName"] = "White.png";
            }
            return View("Index");
        }

        // Calls method of the same name of deck object 
        public ActionResult ShuffleCards()
        {
            if(deck.countCards() == 0)
                ViewData["deckImageName"] = "White.png";
            else
                ViewData["deckImageName"] = "back.jpg";
            ViewData["cardImageName"] = previousCardImageName;
            ViewData["totalCards"] = deck.countCards();
            deck.ShuffleCards();
            return View("Index");
        }

        // Calls method of the same name of deck object
        public ActionResult AddOneDeck()
        {
            ViewData["deckImageName"] = "back.jpg";
            ViewData["cardImageName"] = previousCardImageName;
            deck.addOneDeck();
            ViewData["totalCards"] = deck.countCards();
            return View("Index");
        }

        // Calls method of the same name of deck object
        public ActionResult SortCards()
        {
            ViewData["deckImageName"] = "back.jpg";
            ViewData["cardImageName"] = "White.png";
            deck.sortCards();
            ViewData["totalCards"] = deck.countCards();
            return View("Index");
        }
    }
}