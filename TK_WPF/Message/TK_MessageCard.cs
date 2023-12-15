using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Message"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Message;assembly=TK_WPF.Message"
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
    ///     <MyNamespace:TK_Message/>
    ///
    /// </summary>

    public enum MessageType
    {
        [Description("消息")]
        Info,

        [Description("成功")]
        Success,

        [Description("警告")]
        Warning,

        [Description("错误")]
        Error
    }
    [TemplatePart(Name =CloseButtonName,Type =typeof(TK_Button))]
    public class TK_MessageCard : Control
    {
        public static readonly RoutedEvent CloseEvent;
        const string CloseButtonName = "PART_CloseButton";
        static TK_MessageCard()
        {
            CloseEvent = EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TK_MessageCard));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_MessageCard), new FrameworkPropertyMetadata(typeof(TK_MessageCard)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
          if(  GetTemplateChild(CloseButtonName) is TK_Button btn)
            {
                btn.Click += (s, e) =>
                {
                    if(ShowMessage)
                    {
                        ShowMessage = false;
                    }
                };
            }
        }

        #region 事件
        // 关闭消息事件
        public event RoutedEventHandler Close
        {
            add { base.AddHandler(TK_MessageCard.CloseEvent, value); }
            remove { base.RemoveHandler(TK_MessageCard.CloseEvent, value); }
        }
        #endregion


        #region 依赖属性
        /// <summary>
        /// 消息类型
        /// </summary>

        public static readonly DependencyProperty MessageTypeProperty =
            DependencyProperty.Register("MessageType", typeof(MessageType), typeof(TK_MessageCard), new PropertyMetadata(default(MessageType)));

        public MessageType MessageType
        {
            get { return (MessageType)GetValue(MessageTypeProperty); }
            set { SetValue(MessageTypeProperty, value); }
        }


        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(TK_MessageCard), new PropertyMetadata());



        public bool ShowMessage
        {
            get { return (bool)GetValue(ShowMessageProperty); }
            set { SetValue(ShowMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowMessageProperty =
            DependencyProperty.Register("ShowMessage", typeof(bool), typeof(TK_MessageCard), new PropertyMetadata());



        public bool Closed
        {
            get { return (bool)GetValue(ClosedProperty); }
            set { SetValue(ClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Closed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClosedProperty =
            DependencyProperty.Register("Closed", typeof(bool), typeof(TK_MessageCard), new PropertyMetadata(false, new PropertyChangedCallback(OnClosedChange)));

        private static void OnClosedChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)(e.NewValue))
            {
                if (d is TK_MessageCard card)
                {
                    RoutedEventArgs args = new RoutedEventArgs(CloseEvent);
                    card.RaiseEvent(args);
                }
            }
        }


        #endregion

    }
}
