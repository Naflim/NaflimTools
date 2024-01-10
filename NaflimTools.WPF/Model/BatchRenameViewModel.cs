using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaflimTools.WPF.Model
{
    public class BatchRenameViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FileModel> _fileList;
        private ObservableCollection<FileModel> _selectedFiles;
        private string _prefix;
        private int _startIndex;

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

        public BatchRenameViewModel()
        {
            _fileList = new ObservableCollection<FileModel>();
            _selectedFiles = new ObservableCollection<FileModel>();
            _prefix = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
