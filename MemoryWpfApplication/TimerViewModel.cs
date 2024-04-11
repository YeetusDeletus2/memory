using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace MemoryWpfApplication;

public class TimerViewModel : INotifyPropertyChanged
{
    private TimeSpan _elapsedTime;
    public event PropertyChangedEventHandler PropertyChanged;

    public TimeSpan ElapsedTime
    {
        get { return _elapsedTime; }
        set
        {
            if (_elapsedTime != value)
            {
                _elapsedTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ElapsedTime)));
            }
        }
    }
}