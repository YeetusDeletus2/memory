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
using System.Windows.Threading;
using MemoryLibrary;

namespace MemoryWpfApplication;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static int _maxAmountOfColumns = 4;
    public int Rows { get; set; } = 2;
    public int Columns { get; set; }
    public int AmountOfCards { get; set; } = 10;

    private List<Button> _buttons = new List<Button>();
    private Game _game;
    private TimeSpan _elapsedTime;
    private DispatcherTimer _timer;

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        _game = new Game(AmountOfCards / 2);

        _elapsedTime = TimeSpan.Zero;
        TimeLabel.Text = _elapsedTime + " seconds.";

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromSeconds(1); // Update every second
        _timer.Tick += Timer_Tick;
        _timer.Start();

        // Calculate button size
        double buttonSize = GetButtonSize(ButtonUniformGrid.ActualWidth, ButtonUniformGrid.ActualHeight);
        // Add buttons
        for (int i = 0; i < AmountOfCards; i++)
        {
            Button button = new Button();

            button.Width = buttonSize;
            button.Height = buttonSize;
            button.Margin = new Thickness(5); // Set margin for padding around the buttons

            _buttons.Add(button);
            button.Click += OnButtonClick;
            ButtonUniformGrid.Children.Add(button); // Add button to UniformGrid
        }

        _game.changeStage(_game.firstCard, _game.secondCard, false);
    }

    public async void OnButtonClick(object sender, RoutedEventArgs e)
    {
        Button button = (Button)sender;
        Card currCard = null;
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (button == _buttons[i])
            {
                currCard = _game.Cardslist[i];
            }
        }

        switch (_game.stage)
        {
            case Stage.PressFirstCard:
                _game.firstCard = currCard;
                _game.firstCard.flipCard();
                _game.changeStage(_game.firstCard, _game.secondCard, true);
                break;
            case Stage.PressSecondCard:
                _game.secondCard = currCard;
                _game.secondCard.flipCard();
                _game.changeStage(_game.firstCard, _game.secondCard, true);
                break;
            // case Stage.BothCardsFlipped:
            //     _game.changeStage(_game.firstCard, _game.secondCard, true);
            //     break;
        }

        if (_game.stage == Stage.ShowScreen)
        {
            showImageOfCard(button, currCard);
            _game.changeStage(_game.firstCard, _game.secondCard, false);
        }

        if (_game.stage == Stage.BothCardsFlipped)
        {
            foreach (var but in _buttons)
            {
                but.IsEnabled = false;
            }

            await Task.Delay(1000);
            foreach (var but in _buttons)
            {
                but.IsEnabled = true;
            }

            if (!_game.checkIfCardsAreEqual(_game.firstCard, _game.secondCard))
            {
                // hide both cards
                Button firstButton = null;
                Button secondButton = null;
                for (int i = 0; i < _game.Cardslist.Count; i++)
                {
                    if (_game.firstCard == _game.Cardslist[i])
                    {
                        firstButton = _buttons[i];
                    }

                    if (_game.secondCard == _game.Cardslist[i])
                    {
                        secondButton = _buttons[i];
                    }
                }

                _game.TotalTries++;
                _game.firstCard.flipCard();
                _game.secondCard.flipCard();
                showImageOfCard(firstButton, _game.firstCard);
                showImageOfCard(secondButton, _game.secondCard);
            }

            _game.ResetBothCards(_game.firstCard, _game.secondCard);
            _game.changeStage(_game.firstCard, _game.secondCard, false);
        }

        if (_game.isFinished) 
        {
            _timer.Stop();
            List<int> scores = _game.ReadScoresFromFile(_game.FilePath);
            int newScore = ((AmountOfCards ^ 2 / (int)_elapsedTime.TotalSeconds * _game.TotalTries) * 1000);
            _game.AddScoreToFile(scores, newScore, _game.FilePath);
            MessageBox.Show("Congratulations! You've won.\nYou're time is: " + _elapsedTime);
        }
    }

    private void showImageOfCard(Button button, Card currCard)
    {
        if (!currCard.IsFlipped)
        {
            button.Content = null;
        }
        else
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(currCard.Image, UriKind.Relative));
            img.Width = button.Width;
            img.Height = button.Height;

            button.Content = img;
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

    private void Timer_Tick(object sender, EventArgs e)
    {
        // Increment the elapsed time by one second
        _elapsedTime = _elapsedTime.Add(TimeSpan.FromSeconds(1));
        TimeLabel.Text = _elapsedTime + " seconds.";
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