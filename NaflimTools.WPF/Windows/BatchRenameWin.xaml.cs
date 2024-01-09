using MahApps.Metro.Controls;
using NaflimTools.WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NaflimTools.WPF.Windows
{
    /// <summary>
    /// BatchRename.xaml 的交互逻辑
    /// </summary>
    public partial class BatchRenameWin : MetroWindow
    {
        public BatchRenameViewModel ViewModel { get; set; }

        public BatchRenameWin()
        {
            ViewModel = new BatchRenameViewModel();
            InitializeComponent();
        }

        private void FileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                {
                   if (!ViewModel.FileList.Any(v => v == file))
                        ViewModel.FileList.Add(file);   
                }
            }
        }
    }
}
