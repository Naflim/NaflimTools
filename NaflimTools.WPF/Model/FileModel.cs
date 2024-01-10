using System.ComponentModel;

namespace NaflimTools.WPF.Model
{
    public class FileModel(string path) : INotifyPropertyChanged
    {
        private string _path = path;

        /// <summary>
        /// 绝对路径
        /// </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        /// <summary>
        /// 目录名
        /// </summary>
        public string? DirectoryName => System.IO.Path.GetDirectoryName(Path);

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension => System.IO.Path.GetExtension(Path);

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
