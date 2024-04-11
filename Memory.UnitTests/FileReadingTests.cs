using MemoryLibrary;

namespace MemoryUnitTests;

public class FileReadingTests
{
    private readonly string testFilePath = "test_scores.txt";

    [Test]
    public void ReadScoresFromFile_FileExists_ReturnsScores()
    {
        // Arrange
        File.WriteAllLines(testFilePath, new string[] { "10", "20", "30" });
        FileReading fileReading = new FileReading();

        // Act
        List<int> scores = fileReading.ReadScoresFromFile(testFilePath);

        // Assert
        Assert.That(scores.Count, Is.EqualTo(3));
        CollectionAssert.AreEqual(new List<int> { 10, 20, 30 }, scores);

        // Cleanup
        File.Delete(testFilePath);
    }

    [Test]
    public void AddScoreToFile_AddNewScore_WritesToFile()
    {
        // Arrange
        FileReading fileReading = new FileReading();
        List<int> scores = new List<int> { 10, 20, 30 };

        // Act
        fileReading.AddScoreToFile(scores, 25, testFilePath);

        // Assert
        List<int> updatedScores = fileReading.ReadScoresFromFile(testFilePath);
        CollectionAssert.AreEqual(new List<int> { 30, 25, 20, 10 }, updatedScores);

        // Cleanup
        File.Delete(testFilePath);
    }

    [Test]
    public void AddScoreToFile_OverTenScores_KeepsTopTenScores()
    {
        // Arrange
        File.WriteAllLines(testFilePath, new string[] { "10", "20", "30", "40", "50", "60", "70", "80", "90", "100" });
        FileReading fileReading = new FileReading();
        List<int> scores = fileReading.ReadScoresFromFile(testFilePath);

        // Act
        fileReading.AddScoreToFile(scores, 55, testFilePath);

        // Assert
        List<int> updatedScores = fileReading.ReadScoresFromFile(testFilePath);
        CollectionAssert.AreEqual(new List<int> { 100, 90, 80, 70, 60, 55, 50, 40, 30, 20 }, updatedScores);

        // Cleanup
        File.Delete(testFilePath);
    }
}