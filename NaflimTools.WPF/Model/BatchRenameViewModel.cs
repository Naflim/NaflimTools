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
        private ObservableCollection<string> _fileList;

        public ObservableCollection<string> FileList
        {
            get { return _fileList; }
            set
            {
                _fileList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileList)));
            }
        }

        public BatchRenameViewModel()
        {
            _fileList = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
