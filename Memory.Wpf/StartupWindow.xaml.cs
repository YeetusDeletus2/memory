using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MemoryWpfApplication
{
    public partial class StartupWindow : Window
    {
        private int _customImageIndex = 0;

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
            MainWindow mainWindow = new MainWindow(numberOfPairs, NameTextBox.Text);
            mainWindow.Show();
            this.Close();
        }

        private void btnHighScores_Click(object sender, RoutedEventArgs e)
        {
            HighscoreWindow highscoreWindow = new HighscoreWindow();
            highscoreWindow.Show();
            this.Close();
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png) | *.png;";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Rename the file
                    string newFileName = $"{_customImageIndex}.png";
                    _customImageIndex++;
                    string directory = $@"images/";
                    string newFilePath = Path.Combine(directory, newFileName);

                    // Copy the file to a new location with the new name
                    File.Copy(filePath, newFilePath, true);

                    MessageBox.Show("Image uploaded successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading image: {ex.Message}");
                }
            }
        }
    }
}