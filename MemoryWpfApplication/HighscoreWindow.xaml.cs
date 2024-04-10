using System.Windows;
using System.Windows.Controls;
using MemoryLibrary;

namespace MemoryWpfApplication;

public partial class HighscoreWindow : Window
{
    public HighscoreWindow()
    {
        InitializeComponent();
        PopulateHighScores();
    }
    
    private void PopulateHighScores()
    {
        // Sample high scores data (replace with your actual data)
        FileReading fileReading = new FileReading();
        List<int> highScores = fileReading.ReadScoresFromFile($@"scores.txt");

        // Add high scores to the listbox
        foreach (int score in highScores)
        {
            Label label = new Label();
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.Content = score;
            HighScoresStackPanel.Children.Add(label);
        }
    }

    private void ReturnClick(object sender, RoutedEventArgs e)
    {
        // Open another window
        StartupWindow startupWindow = new StartupWindow();
        startupWindow.Show();
        this.Close();
    }
}