using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

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
    ///     <MyNamespace:TK_Button/>
    ///
    /// </summary>

    public enum ButtonShowType
    {
        [Description("文本和图标")]
        TextAndIcon,
        [Description("仅文本")]
        Text,
        [Description("仅图标")]
        Icon
    }

    public class TK_Button : Button
    {

        static TK_Button()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_Button), new FrameworkPropertyMetadata(typeof(TK_Button)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }




        #region 属性
        /// <summary>
        /// 鼠标放置时的背景色
        /// </summary>
        public Brush MouseOverBack
        {
            get { return (Brush)GetValue(MouseOverBackProperty); }
            set { SetValue(MouseOverBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverBack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackProperty =
            DependencyProperty.Register("MouseOverBack", typeof(Brush), typeof(TK_Button), new PropertyMetadata());

        /// <summary>
        /// 鼠标放置时的字体色
        /// </summary>
        public Brush MouseOverFore
        {
            get { return (Brush)GetValue(MouseOverForeProperty); }
            set { SetValue(MouseOverForeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverFore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverForeProperty =
            DependencyProperty.Register("MouseOverFore", typeof(Brush), typeof(TK_Button), new PropertyMetadata());


        /// <summary>
        /// 按钮圆角
        /// </summary>
        public CornerRadius Corner
        {
            get { return (CornerRadius)GetValue(CornerProperty); }
            set { SetValue(CornerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Corner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerProperty =
            DependencyProperty.Register("Corner", typeof(CornerRadius), typeof(TK_Button), new PropertyMetadata());

        /// <summary>
        /// 字体图标库
        /// </summary>
        public string IconBase
        {
            get { return (string)GetValue(IconBaseProperty); }
            set { SetValue(IconBaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconBase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconBaseProperty =
            DependencyProperty.Register("IconBase", typeof(string), typeof(TK_Button), new PropertyMetadata());


        /// <summary>
        /// 图标
        /// </summary>
        public IconCode Icon
        {
            get { return (IconCode)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(IconCode), typeof(TK_Button), new PropertyMetadata());


        /// <summary>
        /// 按钮显示方式
        /// </summary>
        public ButtonShowType ButtonShowType
        {
            get { return (ButtonShowType)GetValue(ButtonShowTypeProperty); }
            set { SetValue(ButtonShowTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonShowType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonShowTypeProperty =
            DependencyProperty.Register("ButtonShowType", typeof(ButtonShowType), typeof(TK_Button), new PropertyMetadata());



        /// <summary>
        /// 长按时间
        /// </summary>
        public int LongPressTime
        {
            get { return (int)GetValue(LongPressTimeProperty); }
            set { SetValue(LongPressTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LongPressTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LongPressTimeProperty =
            DependencyProperty.Register("LongPressTime", typeof(int), typeof(TK_Button), new PropertyMetadata(400));
        /// <summary>
        /// 按钮松开命令
        /// </summary>
        public ICommand UnPressCommand
        {
            get { return (ICommand)GetValue(UnPressCommandProperty); }
            set { SetValue(UnPressCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonUpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnPressCommandProperty =
            DependencyProperty.Register("UnPressCommand", typeof(ICommand), typeof(TK_Button), new PropertyMetadata(default(ICommand)));
        /// <summary>
        /// 松开按钮命令参数
        /// </summary>
        public object UnPressCommandParameper
        {
            get { return (object)GetValue(UnPressCommandParameperProperty); }
            set { SetValue(UnPressCommandParameperProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnPressCommandParamper.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnPressCommandParameperProperty =
            DependencyProperty.Register("UnPressCommandParameper", typeof(object), typeof(TK_Button), new PropertyMetadata());
        /// <summary>
        /// 按钮按下命令
        /// </summary>
        public ICommand PressCommand
        {
            get { return (ICommand)GetValue(PressCommandProperty); }
            set { SetValue(PressCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonUpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressCommandProperty =
            DependencyProperty.Register("PressCommand", typeof(ICommand), typeof(TK_Button), new PropertyMetadata(default(ICommand)));
        /// <summary>
        /// 按下按钮命令参数
        /// </summary>
        public object PressCommandParameper
        {
            get { return (object)GetValue(PressCommandParameperProperty); }
            set { SetValue(PressCommandParameperProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnPressCommandParamper.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressCommandParameperProperty =
            DependencyProperty.Register("PressCommandParameper", typeof(object), typeof(TK_Button), new PropertyMetadata());
        /// <summary>
        /// 按下按钮事件
        /// </summary>
        public static readonly RoutedEvent PressEvent
            = EventManager.RegisterRoutedEvent("Press",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(TK_Button));

        public event RoutedEventHandler Press
        {
            add => AddHandler(PressEvent, value);
            remove => RemoveHandler(PressEvent, value);
        }
        /// <summary>
        /// 松开按钮事件
        /// </summary>
        public static readonly RoutedEvent UnPressEvent
            = EventManager.RegisterRoutedEvent("UnPress",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(TK_Button));

        public event RoutedEventHandler UnPress
        {
            add => AddHandler(UnPressEvent, value);
            remove => RemoveHandler(UnPressEvent, value);
        }
        #endregion


        private DispatcherTimer _pressDispatcherTimer = new DispatcherTimer();
        private bool isLongPress = false;
        public TK_Button()
        {
            _pressDispatcherTimer.Tick += OnDispatcherTimeOut;
        }

        /// <summary>
        /// 时间到置位长按标志位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDispatcherTimeOut(object sender, EventArgs e)
        {
            isLongPress = true;
            PressCommand?.Execute(PressCommandParameper);
            RaiseEvent(new RoutedEventArgs(PressEvent));
            _pressDispatcherTimer.Stop();
        }
        /// <summary>
        /// 按下开始计时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            isLongPress = false;
            _pressDispatcherTimer.Stop();
            _pressDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, LongPressTime);
            _pressDispatcherTimer.Start();
        }
        /// <summary>
        /// 松开结束计时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            _pressDispatcherTimer.Stop();
        }
        /// <summary>
        /// 按钮点击事件，区分长按或点击
        /// </summary>
        protected override void OnClick()
        {
            if (!isLongPress)
            {
                base.OnClick();
            }
            else
            {
                UnPressCommand?.Execute(UnPressCommandParameper);
                RaiseEvent(new RoutedEventArgs(UnPressEvent));
                isLongPress = false;
            }
        }
    }
}
