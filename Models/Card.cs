

namespace CardGame.Models {
    
    public class Card
    {
        public string ImageName; //Used so the view can show correct card image
        public int ID; //used to sort the deck

        public Card(string imageName, int id)
        {
            ImageName = imageName;
            ID = id;
        }
    }
}
