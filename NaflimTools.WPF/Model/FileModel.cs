using System.ComponentModel;
using System.Drawing;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace NaflimTools.WPF.Model
{
    public class FileModel : INotifyPropertyChanged
    {
        private string _path;

        public FileModel(string path) 
        {
            _path = path;
        }

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
                if (IsImage)
                {
                    var data = File.ReadAllBytes(Path);
                    var stream = new MemoryStream(data);
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
                else
                {
                    var fileIcon = Icon.ExtractAssociatedIcon(Path);

                    if (fileIcon != null)
                        return Imaging.CreateBitmapSourceFromHIcon(fileIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                    return null;
                }
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

        public bool IsImage => Extension == ".jpg" || Extension == ".png";

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
