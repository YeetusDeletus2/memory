using System.Net.Mime;

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
        Id = id;
    }

    public void flipCard()
    {
        IsFlipped = !IsFlipped;
    }
}