using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Dialog"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Dialog;assembly=TK_WPF.Dialog"
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
    ///     <MyNamespace:TK_DialogCard/>
    ///
    /// </summary>
    [TemplatePart(Name = CloseButtonPartName, Type = typeof(TK_Button))]
    [TemplatePart(Name = BackgroundBorderPartName, Type = typeof(Border))]
    [TemplatePart(Name = ContentName, Type = (typeof(ContentControl)))]
    [TemplatePart(Name = TitleName, Type = (typeof(Border)))]
    public class TK_DialogCard : ContentControl
    {
        public static readonly RoutedEvent CloseEvent;
        static TK_DialogCard()
        {
            CloseEvent = EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TK_DialogCard));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_DialogCard), new FrameworkPropertyMetadata(typeof(TK_DialogCard)));
        }

        /// <summary>
        /// 转换动画名称
        /// </summary>
        public const string TransitionName = "Path_Transition";

        /// <summary>
        /// 关闭按钮名称
        /// </summary>
        public const string CloseButtonPartName = "PART_CloseButton";

        /// <summary>
        /// 背景 Border 名称
        /// </summary>
        public const string BackgroundBorderPartName = "PART_BackgroundBorder";
        public const string ContentName = "PART_Content";
        public const string TitleName = "PART_Title";
        /// <summary>
        /// 关闭事件
        /// </summary>
        public Action<object> CloseHandler;
        public ContentControl ContentControl;
        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TK_Button closeButton = GetTemplateChild(CloseButtonPartName) as TK_Button;
            closeButton.Click += (sender, args) =>
            {
                CloseHandler?.Invoke(null);
            };
        }

        #region 事件
        // 关闭消息事件
        public event RoutedEventHandler Close
        {
            add { base.AddHandler(TK_DialogCard.CloseEvent, value); }
            remove { base.RemoveHandler(TK_DialogCard.CloseEvent, value); }
        }
        #endregion

        #region 依赖属性
        /// <summary>
        /// 对话框内容
        /// </summary>
        public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register(
            "DialogContent", typeof(object), typeof(TK_DialogCard), new PropertyMetadata(default(object)));

        /// <summary>
        /// 对话框内容
        /// </summary>
        public object DialogContent
        {
            get { return GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(TK_DialogCard), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(TK_DialogCard), new PropertyMetadata(default(CornerRadius)));

        /// <summary>
        /// 圆角半径
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowCloseButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(TK_DialogCard), new PropertyMetadata());

        public bool ShowDialog
        {
            get { return (bool)GetValue(ShowDialogProperty); }
            set { SetValue(ShowDialogProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowMessageBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDialogProperty =
            DependencyProperty.Register("ShowDialog", typeof(bool), typeof(TK_DialogCard), new PropertyMetadata());



        public bool Closed
        {
            get { return (bool)GetValue(ClosedProperty); }
            set { SetValue(ClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Closed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClosedProperty =
            DependencyProperty.Register("Closed", typeof(bool), typeof(TK_DialogCard), new PropertyMetadata(false, new PropertyChangedCallback(OnClosedChange)));

        private static void OnClosedChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)(e.NewValue))
            {
                if (d is TK_DialogCard card)
                {
                    RoutedEventArgs args = new RoutedEventArgs(CloseEvent);
                    card.RaiseEvent(args);
                }
            }
        }
        #endregion 依赖属性
    }


    /// <summary>
    /// 对话框 view model 接口
    /// </summary>
    public interface IDialogViewModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 请求关闭委托
        /// </summary>
        event Action<object> RequestClose;

        /// <summary>
        /// 当对话框打开完成方法
        /// </summary>
        /// <param name="parameters">参数</param>
        void OnDialogOpened(object parameters);
    }
}
