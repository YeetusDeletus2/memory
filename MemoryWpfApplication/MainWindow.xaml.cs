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
    public int Rows { get; set; }
    public int Columns { get; set; }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (Rows == 0)
        {
            Rows = 2; // default value
        }

        if (Columns == 0)
        {
            Columns = 5;
        }
        // Calculate button size
        int totalButtons = Rows * Columns;
        double buttonSize = GetButtonSize(totalButtons, ButtonUniformGrid.ActualWidth, ButtonUniformGrid.ActualHeight);
        Console.WriteLine(totalButtons);
        // Add buttons
        for (int i = 0; i < totalButtons; i++)
        {
            Button button = new Button();
            button.Content = "Button " + (i + 1);
            button.Width = buttonSize;
            button.Height = buttonSize;
            button.Margin = new Thickness(5); // Set margin for padding around the buttons
            ButtonUniformGrid.Children.Add(button); // Add button to UniformGrid
        }
    }
    
    // Function to calculate button size based on available space and number of buttons
    private double GetButtonSize(int totalButtons, double width, double height)
    {
        // Calculate maximum button size that fits the available space without overlapping
        double maxButtonSize = Math.Min(width / Columns, height / Rows);

        // Adjust button size to make buttons square
        double buttonSize = maxButtonSize - 10; // Subtracting padding

        return buttonSize;
    }
    
    public MainWindow()
    {
        InitializeComponent();
    }
}