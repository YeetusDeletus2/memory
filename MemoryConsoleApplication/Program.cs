using MemoryLibrary;

namespace MemoryConsoleApplication;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.printCardslist();

        switch (game.stage)
        {
            case Stage.ShowScreen:
                Console.Write("\t ");
                for (int i = 0; i < game.Cardslist.Count / 2; i++)
                {
                    Console.Write(i + "\t");
                }

                Console.Write("\n");
                for (int i = 0; i < 2; i++)
                {
                    Console.Write(i);
                    for (int j = 0; j < game.Cardslist.Count / 2; j++)
                    {
                        int location = (5 * i) + j;
                        if (game.Cardslist[location].isFlipped)
                        {
                            Console.WriteLine("\t" + game.Cardslist[location].Id);
                        }
                        else
                        {
                            Console.Write("\t| |");
                        }
                    }

                    Console.Write("\n");
                }

                break;
            case Stage.FirstCardPressed:
                Console.WriteLine("What card do you want to press? (format example: 0,4)");
                string input = Console.ReadLine();
                string[] firstCardPressed = input.Split(',');
                int firstLocation = (5 * Int32.Parse(firstCardPressed[0])) + Int32.Parse(firstCardPressed[1]);
                Card firstCard = game.Cardslist[firstLocation];
                break;
            
            case Stage.SecondCardPressed:
                Console.WriteLine("What card do you want to press? (format example: 0,4)");
                input = Console.ReadLine();
                string[] secondCardPressed = input.Split(',');
                int secondLocation = (5 * Int32.Parse(secondCardPressed[0])) + Int32.Parse(secondCardPressed[1]);
                Card secondCard = game.Cardslist[secondLocation];
                break;
            
            case Stage.GameFinished:
                break;
        }
    }
}