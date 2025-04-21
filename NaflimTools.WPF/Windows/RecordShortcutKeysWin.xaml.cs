using CommunityToolkit.Mvvm.ComponentModel;
using MahApps.Metro.Controls;
using NaflimTools.WPF.Model;
using System.Windows.Input;

namespace NaflimTools.WPF.Windows
{
    /// <summary>
    /// RecordShortcutKeysWin.xaml 的交互逻辑
    /// </summary>
    [ObservableObject]
    public partial class RecordShortcutKeysWin : MetroWindow
    {
        private Timer? _timer;

        public RecordShortcutKeysWin()
        {
            InitializeComponent();
        }

        [ObservableProperty]
        private ShortcutKeysViewModel _shortcutKeys = new ShortcutKeysViewModel();

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(_timer == null)
            {
                _timer = new Timer(TimeOut);
                _timer.Change(500, 500);
                ShortcutKeys.ShortcutKeys.Clear();
            }
            else
            {
                _timer.Change(500, 500);
            }

            ShortcutKeys.ShortcutKeys.Add(e.Key);
        }

        private void TimeOut(object? obj)
        {
            _timer?.Dispose();

            Dispatcher.Invoke(() => 
            {
                DialogResult = true;
                Close();
            });
        }
    }
}
