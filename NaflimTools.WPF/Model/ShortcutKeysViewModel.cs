using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NaflimTools.WPF.Model
{
    /// <summary>
    /// 快捷键视图模型
    /// </summary>
    public partial class ShortcutKeysViewModel : ObservableObject
    {
        private const string DEFAULT_TITLE = "请输入快捷键";

        private ObservableCollection<Key> _shortcutKeys;

        /// <summary>
        /// 标题
        /// </summary>
        [ObservableProperty]
        private string _title = DEFAULT_TITLE;

        public ShortcutKeysViewModel()
        {
            _shortcutKeys = new ObservableCollection<Key>();
            _shortcutKeys.CollectionChanged += (s, e) => InitTitle();
        }

        /// <summary>
        /// 快捷键列表
        /// </summary>
        public ObservableCollection<Key> ShortcutKeys
        {
            get => _shortcutKeys;
            set 
            {
                SetProperty(ref _shortcutKeys, value);
                _shortcutKeys.CollectionChanged += (s, e) => InitTitle();
                InitTitle();
            }
        }

        private void InitTitle()
        {
            if (ShortcutKeys.Count == 0)
                Title = DEFAULT_TITLE;
            else
                Title = string.Join("+ ", ShortcutKeys.Select(x => x.ToString()));
        }
    }
}
