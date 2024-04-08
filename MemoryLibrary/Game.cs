using System.Collections;

namespace MemoryLibrary;

public class Game
{
    public Card firstCard = null;
    public Card secondCard = null;
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
            Card firstCard = new Card(i);
            Card secondCard = new Card(i);
            Cardslist.Add(firstCard);
            Cardslist.Add(secondCard);
        }

        Cardslist = ScrambleCards(Cardslist);
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

    public void changeStage(Card firstCard, Card secondCard, bool showScreen)
    {
        if (showScreen)
        {
            // the screen must be shown
            stage = Stage.ShowScreen;
        }
        else if (secondCard != null)
        {
            // both cards turned, go to logic
            stage = Stage.BothCardsFlipped;
        }
        else if (firstCard != null)
        {
            // first card turned
            stage = Stage.PressSecondCard;
        }
        else
        {
            // no cards turned
            stage = Stage.PressFirstCard;
        }
    }

    public void checkIfCardsAreEqual(Card firstCard, Card secondCard)
    {
        if (!(firstCard.Id == secondCard.Id))
        {
            firstCard.flipCard();
            secondCard.flipCard();
        }

        this.firstCard = null;
        this.secondCard = null;
    }

    public bool checkIfGameFinished()
    {
        foreach (Card card in Cardslist)
        {
            if (!card.IsFlipped)
            {
                return false;
            }
        }

        return true;
    }
}