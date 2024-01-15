using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NaflimTools.WPF.Model
{
    public class BatchRenameViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FileModel> _fileList;
        private ObservableCollection<FileModel> _selectedFiles;
        private string _prefix;
        private int _startIndex;
        private bool _isEnabledSort;
        private bool _isEnabledImportExport = true;
        private bool _isEnabledPicturePreview;

        /// <summary>
        /// 文件列表
        /// </summary>
        public ObservableCollection<FileModel> FileList
        {
            get { return _fileList; }
            set
            {
                _fileList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileList)));
            }
        }

        /// <summary>
        /// 选中文件列表
        /// </summary>
        public ObservableCollection<FileModel> SelectedFiles
        {
            get { return _selectedFiles; }
            set
            {
                _selectedFiles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFiles)));
            }
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix
        {
            get { return _prefix; }
            set
            {
                _prefix = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prefix)));
            }
        }

        /// <summary>
        /// 起始索引
        /// </summary>
        public int StartIndex
        {
            get { return _startIndex; }
            set
            {
                _startIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StartIndex)));
            }
        }

        /// <summary>
        /// 是否启用排序
        /// </summary>
        public bool IsEnabledSort
        {
            get { return _isEnabledSort; }
            set
            {
                _isEnabledSort = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabledSort)));
            }
        }

        /// <summary>
        /// 是否启用导入导出
        /// </summary>
        public bool IsEnabledImportExport
        {
            get { return _isEnabledImportExport; }
            set
            {
                _isEnabledImportExport = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabledImportExport)));
            }
        }


        /// <summary>
        /// 是否启用图片预览
        /// </summary>
        public bool IsEnabledPicturePreview
        {
            get { return _isEnabledPicturePreview; }
            set
            {
                _isEnabledPicturePreview = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabledPicturePreview)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListBoxColumnSpan)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImgVisibility)));
            }
        }

        /// <summary>
        /// 列表行占用
        /// </summary>
        public int ListBoxColumnSpan => IsEnabledPicturePreview ? 7 : 9;

        /// <summary>
        /// 预览控件是否隐藏
        /// </summary>
        public Visibility ImgVisibility => IsEnabledPicturePreview ? Visibility.Visible : Visibility.Hidden;

        /// <summary>
        /// 预览图片源
        /// </summary>
        public ImageSource? PreviewSource => SelectedFiles.FirstOrDefault()?.Logo;

        public BatchRenameViewModel()
        {
            _fileList = new ObservableCollection<FileModel>();
            _selectedFiles = new ObservableCollection<FileModel>();
            _prefix = string.Empty;
            SelectedFiles.CollectionChanged += (s, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PreviewSource)));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
