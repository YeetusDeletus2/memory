using System;
using System.Windows;
using System.Windows.Controls;

namespace MemoryWpfApplication
{
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            for (int i = 5; i <= 20; i++)
            {
                comboBoxPairs.Items.Add(i);
            }
            comboBoxPairs.SelectedIndex = 0; // Select the first item by default
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int numberOfPairs = (int)comboBoxPairs.SelectedItem;
            if (numberOfPairs < 5 || numberOfPairs > 20)
            {
                MessageBox.Show("Please enter a number of pairs between 5 and 20.");
                return;
            }

            // Start the game with the chosen number of pairs
            MainWindow mainWindow = new MainWindow(numberOfPairs);
            mainWindow.Show();
            this.Close();
        }

        private void btnHighScores_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the high scores screen
            // HighScoresWindow highScoresWindow = new HighScoresWindow();
            // highScoresWindow.Show();
        }
    }
}