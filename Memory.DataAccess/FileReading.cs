namespace MemoryLibrary;

public class FileReading
{
    public List<int> ReadScoresFromFile(string filePath)
    {
        List<int> scores = new List<int>();
        if (File.Exists(filePath))
        {
            // Read existing scores from the file
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (int.TryParse(line, out int score))
                {
                    scores.Add(score);
                }
            }
        }

        File.Delete(filePath);
        return scores;
    }

    public void AddScoreToFile(List<int> scores, int newScore, string filePath)
    {
        // Add the new score
        scores.Add(newScore);

        // Sort the scores in descending order
        scores.Sort((a, b) => b.CompareTo(a));

        // Keep only the top 10 scores
        if (scores.Count > 10)
        {
            scores.RemoveAt(10);
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (int score in scores)
            {
                writer.WriteLine(score);
            }
        }
    }
}