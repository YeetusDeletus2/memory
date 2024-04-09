using MemoryLibrary;

namespace MemoryConsoleApplication;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();

        while (!game.isFinished)
        {
            switch (game.stage)
            {
                case Stage.PressFirstCard:
                    Console.WriteLine("What card do you want to press? (format example: 0,4)");
                    string input = Console.ReadLine();
                    Console.Clear();
                    string[] firstCardPressed = input.Split(',');
                    int firstLocation = (5 * Int32.Parse(firstCardPressed[0])) + Int32.Parse(firstCardPressed[1]);
                    game.firstCard = game.Cardslist[firstLocation];

                    game.changeStage(game.firstCard, game.secondCard, true);
                    break;

                case Stage.PressSecondCard:
                    Console.WriteLine("What card do you want to press? (format example: 0,4)");
                    input = Console.ReadLine();
                    Console.Clear();
                    string[] secondCardPressed = input.Split(',');
                    int secondLocation = (5 * Int32.Parse(secondCardPressed[0])) + Int32.Parse(secondCardPressed[1]);
                    game.secondCard = game.Cardslist[secondLocation];

                    game.changeStage(game.firstCard, game.secondCard, true);
                    break;

                case Stage.BothCardsFlipped:
                    Console.WriteLine("Press enter to continue.");
                    input = Console.ReadLine();
                    Console.Clear();
                    
                    game.changeStage(game.firstCard, game.secondCard, true);
                    break;

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
                            if (game.Cardslist[location].IsFlipped)
                            {
                                Console.Write("\t|" + game.Cardslist[location].Id + "|");
                            }
                            else
                            {
                                Console.Write("\t| |");
                            }
                        }

                        Console.Write("\n");
                    }

                    game.changeStage(game.firstCard, game.secondCard, false);
                    break;
            }
        }

        Console.WriteLine("Game is finished!");
        // save highscore
    }
}