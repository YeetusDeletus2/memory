using System.Collections;

namespace MemoryLibrary;

public class Game
{
    public List<Card> Cardslist { get; set; }
    public bool isFinished { get; set; }
    public Stage stage { get; set; }

    public Game()
    {
        // standard game, 5 distinct cards.
        Cardslist = new List<Card>();
        for (int i = 0; i < 5; i++)
        {
            Card firstCard = new Card(i);
            Card secondCard = new Card(i);
            Cardslist.Add(firstCard);
            Cardslist.Add(secondCard);
        }

        Cardslist = ScrambleCards(Cardslist);
    }

    public Game(int amountOfPairs)
    {
        // amount of cards can be defined
        Cardslist = new List<Card>();
        for (int i = 0; i < amountOfPairs; i++)
        {
            Card card = new Card(i);
            Cardslist.Add(card);
        }
    }

    public List<Card> ScrambleCards(List<Card> cards)
    {
        // scramble the list of cards.
        List<Card> scrambledList = new List<Card>();
        Random random = new Random();

        while (cards.Count != 0)
        {
            int randomNumber = random.Next(cards.Count);
            try
            {
                scrambledList.Add(cards[randomNumber]);
                cards.RemoveAt(randomNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        return scrambledList;
    }

    public void printCardslist()
    {
        // helper function for debugging
        for (int i = 0; i < Cardslist.Count; i++)
        {
            Console.WriteLine(Cardslist[i]);
        }
    }

    
}