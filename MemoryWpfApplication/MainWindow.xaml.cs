using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryWpfApplication;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static int _maxAmountOfColumns = 5;
    public int Rows { get; set; }
    public int Columns { get; set; }
    public int AmountOfCards { get; set; }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (Rows == 0)
        {
            Rows = 2; // default value
        }

        if (AmountOfCards == 0)
        {
            AmountOfCards = 10;
        }

        // Calculate button size
        double buttonSize = GetButtonSize(ButtonUniformGrid.ActualWidth, ButtonUniformGrid.ActualHeight);
        // Add buttons
        for (int i = 0; i < AmountOfCards; i++)
        {
            Button button = new Button();
            button.Content = "Button " + (i + 1); // later hier afbeelding
            button.Width = buttonSize;
            button.Height = buttonSize;
            button.Margin = new Thickness(5); // Set margin for padding around the buttons
            ButtonUniformGrid.Children.Add(button); // Add button to UniformGrid
        }
    }

    // Function to calculate button size based on available space and number of buttons
    private double GetButtonSize(double width, double height)
    {
        // Calculate maximum button size that fits the available space without overlapping
        double maxButtonSize = Math.Min(width / Columns, height / Rows);

        // Adjust button size to make buttons square
        double buttonSize = maxButtonSize - 10; // Subtracting padding

        return buttonSize;
    }

    public MainWindow(int numberOfPairs)
    {
        AmountOfCards = numberOfPairs * 2;
        Columns = _maxAmountOfColumns;
        if (AmountOfCards % Columns == 0)
        {
            Rows = AmountOfCards / Columns;
        }
        else
        {
            Rows = (AmountOfCards / Columns) + 1;
        }

        InitializeComponent();
    }
}