using MahApps.Metro.Controls;
using NaflimTools.WPF.Model;
using NaflimTools.WPF.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NaflimTools.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly ToolsConfig _config;

        public MainWindow()
        {
            _config = ToolsConfig.Load();
            InitializeComponent();
        }

        private void BatchRenameClick(object sender, RoutedEventArgs e)
        {
            BatchRenameWin win = new();
            win.ShowDialog();
        }

        private void ChatGPTClick(object sender, RoutedEventArgs e)
        {
            ChatGPTWin win = new();
            win.ShowDialog();
        }

        private void QuickOperationClick(object sender, RoutedEventArgs e)
        {
            QuickOperationWin win = new(_config.QuickOperationViewModel);
            win.ShowDialog();
        }
    }
}