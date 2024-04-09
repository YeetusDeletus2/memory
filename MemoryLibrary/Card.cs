using System.Windows.Controls;

namespace MemoryLibrary;

public class Card
{
    public string Image
    {
        get
        {
            return $@"images\{Id}.png";
        }
    }

    public int Id { get; set; }
    public bool IsFlipped { get; set; }
    
    public Card(int id)
    {
        // for the wpf version, this allows for an image to be added to the constuctor
        Id = id;
    }

    public void flipCard()
    {
        IsFlipped = !IsFlipped;
    }
}