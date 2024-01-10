using System.ComponentModel;
using System.Drawing;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        /// 图标
        /// </summary>
        public ImageSource? Logo 
        {
            get 
            {
                var fileIcon = Icon.ExtractAssociatedIcon(Path);

                if (fileIcon != null)
                    return Imaging.CreateBitmapSourceFromHIcon(fileIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                return null;
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
