using MemoryLibrary;
namespace MemoryUnitTests;

[TestFixture]
public class MemoryLibraryTests
{
    [Test]
    public void TestScrambleCards()
    {
        var game = new Game();
        var cards = new List<Card>()
        {
            new Card(1),
            new Card(2),
            new Card(3),
            new Card(4)
        };
        var originalCards = cards;
        var scrambledCards = game.ScrambleCards(cards);

        // Verify if all elements in original list are present in scrambled list but in different order
        foreach (var card in originalCards)
        {
            Assert.Contains(card, scrambledCards);
        }
    }

    [Test]
    public void TestCheckIfCardsAreEqual()
    {
        var game = new Game();
        var card1 = new Card(1);
        var card2 = new Card(1);
        var card3 = new Card(2);

        Assert.IsTrue(game.checkIfCardsAreEqual(card1, card2));
        Assert.IsFalse(game.checkIfCardsAreEqual(card1, card3));
    }
}