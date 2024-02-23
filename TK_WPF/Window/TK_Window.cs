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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TK_WPF
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF;assembly=TK_WPF"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:TK_Window/>
    ///
    /// </summary>
    public class TK_Window : Window
    {
        public TK_Window()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_Window), new FrameworkPropertyMetadata(typeof(TK_Window)));
            DefaultStyleKey = typeof(TK_Window);
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, MaximizeWindow, CanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, MinimizeWindow, CanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, RestoreWindow, CanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, ShowSystemMenu));
            this.Closed += TK_Window_Closed;
        }

        private void TK_Window_Closed(object sender, EventArgs e)
        {
            TK_WPF.TK_Message.RemoveContainer(this.Title);
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (SizeToContent == SizeToContent.WidthAndHeight)
                InvalidateMeasure();
        }

        #region 窗体命令
        private void CanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void CanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private async void CloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is Window win)
            {
                if (await TK_WPF.TK_MessageBox.Question(win.Title,"是否确认关闭窗口？", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    win.Close();
                }
            }
        }

        private void MaximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            if (int.Parse(e.Parameter.ToString()) == 1)
            {
                this.WindowStyle = System.Windows.WindowStyle.None;
                SystemCommands.MaximizeWindow(this);
            }
            else
            {
                this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                SystemCommands.MaximizeWindow(this);
            }
        }

        private void MinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void RestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        private void ShowSystemMenu(object sender, ExecutedRoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element == null)
                return;

            var point = WindowState == WindowState.Maximized ? new Point(0, element.ActualHeight)
                : new Point(Left + BorderThickness.Left, element.ActualHeight + Top + BorderThickness.Top);
            point = element.TransformToAncestor(this).Transform(point);
            SystemCommands.ShowSystemMenu(this, point);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 标题栏内容
        /// </summary>
        public static readonly DependencyProperty TitleBarContentProperty =
            DependencyProperty.Register("TitleBarContent", typeof(object), typeof(TK_Window), new PropertyMetadata(default(object)));

        /// <summary>
        /// 标题栏内容
        /// </summary>
        public object TitleBarContent
        {
            get { return (object)GetValue(TitleBarContentProperty); }
            set { SetValue(TitleBarContentProperty, value); }
        }

        /// <summary>
        /// 是否显示标题栏阴影
        /// </summary>
        public static readonly DependencyProperty TitleShadowProperty =
            DependencyProperty.Register("TitleShadow", typeof(bool), typeof(TK_Window), new PropertyMetadata(default(bool)));

        /// <summary>
        /// 是否显示标题栏阴影
        /// </summary>
        public bool TitleShadow
        {
            get { return (bool)GetValue(TitleShadowProperty); }
            set { SetValue(TitleShadowProperty, value); }
        }

        /// <summary>
        /// 标题栏高度
        /// </summary>
        public static readonly DependencyProperty TitleHeightProperty =
            DependencyProperty.Register("TitleHeight", typeof(double), typeof(TK_Window), new PropertyMetadata());

        /// <summary>
        /// 标题栏高度
        /// </summary>
        public double TitleHeight
        {
            get { return (double)GetValue(TitleHeightProperty); }
            set { SetValue(TitleHeightProperty, value); }
        }

        /// <summary>
        /// 标题前景色
        /// </summary>
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(TK_Window), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 标题前景色
        /// </summary>
        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        /// <summary>
        /// Window 非活动边框颜色
        /// </summary>
        public static readonly DependencyProperty InactiveBorderBrushProperty =
            DependencyProperty.Register("InactiveBorderBrush", typeof(Brush), typeof(TK_Window), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Window 非活动边框颜色
        /// </summary>
        public Brush InactiveBorderBrush
        {
            get { return (Brush)GetValue(InactiveBorderBrushProperty); }
            set { SetValue(InactiveBorderBrushProperty, value); }
        }
        #endregion
    }
}
