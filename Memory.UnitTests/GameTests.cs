using MemoryLibrary;

namespace MemoryUnitTests;

public class GameTests
{
    [Test]
    public void Constructor_DefaultConstructor_InitializesGameWithStandardDeck()
    {
        // Arrange & Act
        Game game = new Game();

        // Assert
        Assert.That(game.Cardslist.Count, Is.EqualTo(10));
        Assert.That(game.Cardslist.Select(c => c.Id).Distinct().Count(),
            Is.EqualTo(5)); // Ensure all cardpairs are distinct
        Assert.IsFalse(game.isFinished);
        Assert.IsNotNull(game.stage);
        Assert.That(game.TotalTries, Is.EqualTo(0));
        Assert.That(game.FilePath, Is.EqualTo("scores.txt"));
    }

    [Test]
    public void Constructor_CustomConstructor_InitializesGameWithSpecifiedNumberOfPairs()
    {
        // Arrange & Act
        int pairs = 6;
        Game game = new Game(pairs);

        // Assert
        Assert.That(game.Cardslist.Count, Is.EqualTo(pairs * 2));
        Assert.That(game.Cardslist.Select(c => c.Id).Distinct().Count(),
            Is.EqualTo(pairs)); // equal to pairs because of card matches
        Assert.IsFalse(game.isFinished);
        Assert.IsNotNull(game.stage);
        Assert.That(game.TotalTries, Is.EqualTo(0));
        Assert.That(game.FilePath, Is.EqualTo("scores.txt"));
    }

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
    public void ChangeStage_UpdatesStageAccordingToConditions()
    {
        // Arrange
        Game game = new Game();
        Card firstCard = new Card(1);
        Card secondCard = new Card(2);

        // Act & Assert
        game.changeStage(null, null, true);
        Assert.That(game.stage, Is.EqualTo(Stage.ShowScreen));

        game.changeStage(null, null, false);
        Assert.That(game.stage, Is.EqualTo(Stage.PressFirstCard));

        game.changeStage(firstCard, null, false);
        Assert.That(game.stage, Is.EqualTo(Stage.PressSecondCard));

        game.changeStage(firstCard, secondCard, false);
        Assert.That(game.stage, Is.EqualTo(Stage.BothCardsFlipped));
    }

    [Test]
    public void ResetBothCards_ClearsFirstAndSecondCardAndChecksGameCompletion()
    {
        // Arrange
        Game game = new Game();
        Card firstCard = new Card(1);
        Card secondCard = new Card(2);
        game.firstCard = firstCard;
        game.secondCard = secondCard;

        // Act
        game.ResetBothCards(firstCard, secondCard);

        // Assert
        Assert.IsNull(game.firstCard);
        Assert.IsNull(game.secondCard);
        Assert.IsFalse(game.isFinished);
    }

    [Test]
    public void CheckIfCardsAreEqual_ReturnsTrueIfCardsAreEqual()
    {
        // Arrange
        Game game = new Game();
        Card firstCard = new Card(1);
        Card secondCard = new Card(1);

        // Act & Assert
        Assert.IsTrue(game.checkIfCardsAreEqual(firstCard, secondCard));
    }

    [Test]
    public void CheckIfCardsAreEqual_ReturnsFalseIfCardsAreNotEqual()
    {
        // Arrange
        Game game = new Game();
        Card firstCard = new Card(1);
        Card secondCard = new Card(2);

        // Act & Assert
        Assert.IsFalse(game.checkIfCardsAreEqual(firstCard, secondCard));
    }

    [Test]
    public void CheckIfGameFinished_ReturnsTrueIfAllCardsAreFlipped()
    {
        // Arrange
        Game game = new Game();
        foreach (var card in game.Cardslist)
        {
            card.IsFlipped = true;
        }

        // Act & Assert
        Assert.IsTrue(game.checkIfGameFinished());
    }

    [Test]
    public void ScrambleCards_CatchesException_WhenCardsListIsEmpty()
    {
        var cards = new List<Card>();
        var game = new Game();

        Assert.Throws<Exception>(() => game.ScrambleCards(cards));
    }
}