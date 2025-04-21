using CommunityToolkit.Mvvm.ComponentModel;
using MahApps.Metro.Controls;
using NaflimTools.WPF.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace NaflimTools.WPF.Windows
{
    /// <summary>  
    /// QuickOperationWin.xaml 的交互逻辑  
    /// </summary>  
    [ObservableObject]
    public partial class QuickOperationWin : MetroWindow
    {
        /// <summary>  
        /// 定时器，用于检测快捷键输入超时  
        /// </summary>  
        private Timer? _timer;

        /// <summary>  
        /// 存储当前输入的快捷键列表  
        /// </summary>  
        private List<Key> _shortcutKeys = new List<Key>();

        /// <summary>  
        /// 键盘钩子的常量值  
        /// </summary>  
        private const int WH_KEYBOARD_LL = 13;

        /// <summary>  
        /// 键盘按下消息的常量值  
        /// </summary>  
        private const int WM_KEYDOWN = 0x0100;

        /// <summary>  
        /// 钩子句柄  
        /// </summary>  
        private IntPtr _hookID = IntPtr.Zero;

        /// <summary>  
        /// 键盘钩子回调函数  
        /// </summary>  
        private LowLevelKeyboardProc _proc;

        /// <summary>  
        /// 定义低级键盘钩子回调函数的委托  
        /// </summary>  
        /// <param name="nCode">钩子代码</param>  
        /// <param name="wParam">消息参数</param>  
        /// <param name="lParam">消息附加信息</param>  
        /// <returns>处理后的消息指针</returns>  
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>  
        /// 构造函数，初始化窗口和键盘钩子  
        /// </summary>  
        /// <param name="viewModel">视图模型</param>  
        public QuickOperationWin(QuickOperationViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        /// <summary>  
        /// 视图模型  
        /// </summary>  
        [ObservableProperty]
        private QuickOperationViewModel _viewModel;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>  
        /// 设置键盘钩子  
        /// </summary>  
        /// <param name="proc">钩子回调函数</param>  
        /// <returns>钩子句柄</returns>  
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule? curModule = curProcess.MainModule)
            {
                string moduleName = curModule?.ModuleName ?? string.Empty;
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(moduleName), 0);
            }
        }

        /// <summary>  
        /// 键盘钩子回调函数  
        /// </summary>  
        /// <param name="nCode">钩子代码</param>  
        /// <param name="wParam">消息参数</param>  
        /// <param name="lParam">消息附加信息</param>  
        /// <returns>处理后的消息指针</returns>  
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Key key = KeyInterop.KeyFromVirtualKey(vkCode);

                if (_timer == null)
                {
                    _timer = new Timer(TimeOut);
                    _timer.Change(500, 500);
                    _shortcutKeys.Clear();
                }
                else
                {
                    _timer.Change(500, 500);
                }

                _shortcutKeys.Add(key);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        /// <summary>  
        /// 窗口关闭时取消键盘钩子  
        /// </summary>  
        /// <param name="e">事件参数</param>  
        protected override void OnClosed(EventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            base.OnClosed(e);
        }

        /// <summary>  
        /// 定时器超时回调函数  
        /// </summary>  
        /// <param name="obj">回调参数</param>  
        private void TimeOut(object? obj)
        {
            _timer?.Dispose();
            _timer = null;

            if (ViewModel.IsOnWebMedia && IsKeysEqual(ViewModel.WebMedia.ShortcutKeys))
            {
                MessageBox.Show("!");
            }
        }

        /// <summary>  
        /// 比较两个快捷键列表是否相等  
        /// </summary>  
        /// <param name="keys">目标快捷键列表</param>  
        /// <returns>是否相等</returns>  
        private bool IsKeysEqual(IList<Key> keys)
        {
            if (keys.Count != _shortcutKeys.Count)
                return false;

            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] != _shortcutKeys[i])
                    return false;
            }

            return true;
        }

        /// <summary>  
        /// 记录快捷键按钮点击事件处理  
        /// </summary>  
        /// <param name="sender">事件发送者</param>  
        /// <param name="e">事件参数</param>  
        private void RecordShortcutKeysClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is not ContentControl control)
                return;

            if (control.DataContext is ShortcutKeysViewModel shortcutKeys)
            {
                // 打开一个窗口或对话框以捕获用户输入的快捷键  
                RecordShortcutKeysWin win = new RecordShortcutKeysWin();
                if (win.ShowDialog() == true)
                {
                    shortcutKeys.ShortcutKeys = win.ShortcutKeys.ShortcutKeys;
                    shortcutKeys.Title = win.ShortcutKeys.Title;
                }
            }
        }
    }
}
