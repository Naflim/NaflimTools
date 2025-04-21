using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NaflimTools.WPF.Model
{
    /// <summary>
    /// 快速操作视图模型
    /// </summary>
    public partial class QuickOperationViewModel : ObservableObject
    {
        /// <summary>
        /// 网页媒体
        /// </summary>
        [ObservableProperty]
        private ShortcutKeysViewModel _webMedia;

        /// <summary>
        /// 网页媒体开关
        /// </summary>
        [ObservableProperty]
        private bool _isOnWebMedia = true;

        public QuickOperationViewModel()
        {
            _webMedia = new ShortcutKeysViewModel();
            _webMedia.ShortcutKeys = new ObservableCollection<Key> { Key.LeftCtrl, Key.Space };
        }
    }
}
