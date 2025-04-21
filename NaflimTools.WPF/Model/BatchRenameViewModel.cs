using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace NaflimTools.WPF.Model
{
    /// <summary>
    /// 批量重命名视图模型
    /// </summary>
    public partial class BatchRenameViewModel : ObservableObject
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<FileModel> _fileList;

        /// <summary>
        /// 选中文件列表
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<FileModel> _selectedFiles;

        /// <summary>
        /// 前缀
        /// </summary>
        [ObservableProperty]
        private string _prefix;

        /// <summary>
        /// 起始索引
        /// </summary>
        [ObservableProperty]
        private int _startIndex;

        /// <summary>
        /// 是否启用排序
        /// </summary>
        [ObservableProperty]
        private bool _isEnabledSort;

        /// <summary>
        /// 是否启用导入导出
        /// </summary>
        [ObservableProperty]
        private bool _isEnabledImportExport = true;

        private bool _isEnabledPicturePreview;

        /// <summary>
        /// 是否启用图片预览
        /// </summary>
        public bool IsEnabledPicturePreview
        {
            get { return _isEnabledPicturePreview; }
            set
            {
                _isEnabledPicturePreview = value;

                OnPropertyChanged(nameof(IsEnabledPicturePreview));
                OnPropertyChanged(nameof(ListBoxColumnSpan));
                OnPropertyChanged(nameof(ImgVisibility));
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
            SelectedFiles.CollectionChanged += (s, e) => OnPropertyChanged(nameof(PreviewSource));
        }
    }
}
