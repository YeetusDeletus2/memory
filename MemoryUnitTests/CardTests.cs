using MemoryLibrary;

namespace MemoryUnitTests;

public class CardTests
{
    [Test]
    public void Image_Property_ReturnsCorrectPath()
    {
        // Arrange
        int id = 5;
        Card card = new Card(id);
        string expectedImagePath = $"images\\{id}.png";

        // Act
        string imagePath = card.Image;

        // Assert
        Assert.That(imagePath, Is.EqualTo(expectedImagePath));
    }

    [Test]
    public void FlipCard_WhenIsFlippedIsFalse_SetsIsFlippedToTrue()
    {
        // Arrange
        Card card = new Card(1);
        card.IsFlipped = false;

        // Act
        card.flipCard();

        // Assert
        Assert.IsTrue(card.IsFlipped);
    }

    [Test]
    public void FlipCard_WhenIsFlippedIsTrue_SetsIsFlippedToFalse()
    {
        // Arrange
        Card card = new Card(1);
        card.IsFlipped = true;

        // Act
        card.flipCard();

        // Assert
        Assert.IsFalse(card.IsFlipped);
    }
}