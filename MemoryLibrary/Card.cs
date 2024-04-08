namespace MemoryLibrary;

public class Card
{
    public string Image { get; set; }
    public int Id { get; set; }
    public bool IsFlipped { get; set; }
    
    public Card(string image, int id)
    {
        // for the wpf version, this allows for an image to be added to the constuctor
        this.Image = image;
        this.Id = id;
    }

    public Card(int id)
    {
        // standard card, image is empty
        this.Image = "";
        this.Id = id;
    }

    public void flipCard()
    {
        IsFlipped = !IsFlipped;
    }
     
    override public string ToString()
    {
        // helper function for better printing while debugging
        return "Card Id: " + Id + " || Image: " + Image;
    }
}