﻿using MahApps.Metro.Controls;
using NaflimTools.WPF.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace NaflimTools.WPF.Windows
{
    /// <summary>
    /// BatchRename.xaml 的交互逻辑
    /// </summary>
    public partial class BatchRenameWin : MetroWindow
    {
        /// <summary>
        /// 视图模型
        /// </summary>
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
                    if (!ViewModel.FileList.Any(v => v.Path == file))
                        ViewModel.FileList.Add(new FileModel(file));
                }
            }
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ViewModel.FileList.Count; i++)
            {
                var file = ViewModel.FileList[i];
                Debug.Assert(file != null, "空文件！");
                var index = i + ViewModel.StartIndex;
                string newPath = Path.Combine(file.DirectoryName ?? string.Empty, $"{ViewModel.Prefix}{index}{file.Extension}");
                File.Move(file.Path, newPath);
                file.Path = newPath;
            }
        }

        private void FileExport(object sender, MouseButtonEventArgs e)
        {
            var obj = e.Source;
            if (ViewModel.SelectedFiles == null || ViewModel.SelectedFiles.Count == 0)
                return;

            string[] files = ViewModel.SelectedFiles.Select(v => v.Path).ToArray();
            if (obj is UIElement)
            {
                DragDrop.DoDragDrop(obj as UIElement, new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
    }
}
