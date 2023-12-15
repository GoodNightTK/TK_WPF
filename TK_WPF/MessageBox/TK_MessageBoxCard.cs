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

namespace TK_WPF
{
    public enum MessageBoxType
    {
        [Description("消息")]
        Info,

        [Description("疑问")]
        Question,

        [Description("成功")]
        Success,

        [Description("警告")]
        Warning,

        [Description("错误")]
        Error
    }


    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.MessageBox"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.MessageBox;assembly=TK_WPF.MessageBox"
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
    ///     <MyNamespace:TK_MessageBoxCard/>
    ///
    /// </summary>
    [TemplatePart(Name = YestButtonName, Type = typeof(TK_Button))]
    [TemplatePart(Name = NoButtonName, Type = typeof(TK_Button))]
    [TemplatePart(Name = EnterButtonName, Type = typeof(TK_Button))]
    [TemplatePart(Name = CancelButtonName, Type = typeof(TK_Button))]
    public class TK_MessageBoxCard : ContentControl
    {
        const string YestButtonName = "PART_YesButton";
        const string NoButtonName = "PART_NoButton";
        const string EnterButtonName = "PART_EnterButton";
        const string CancelButtonName = "PART_CancelButton";
        public static readonly RoutedEvent CloseEvent;

        static TK_MessageBoxCard()
        {
            CloseEvent = EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TK_MessageBoxCard));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_MessageBoxCard), new FrameworkPropertyMetadata(typeof(TK_MessageBoxCard)));
        }

        #region 事件
        // 关闭消息事件
        public event RoutedEventHandler Close
        {
            add { base.AddHandler(TK_MessageBoxCard.CloseEvent, value); }
            remove { base.RemoveHandler(TK_MessageBoxCard.CloseEvent, value); }
        }
        #endregion

        /// <summary>
        /// 结果
        /// </summary>
        public MessageBoxResult Result { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_CancelButton") is TK_Button cancel)
            {
                cancel.Click += (s, e) => { Result = MessageBoxResult.Cancel;
                    if (ShowMessageBox)
                    {
                        ShowMessageBox = false;
                    }
                };
            }
            if (GetTemplateChild("PART_YesButton") is TK_Button yes)
            {
                yes.Click += (s, e) => { Result = MessageBoxResult.Yes;
                    if (ShowMessageBox)
                    {
                        ShowMessageBox = false;
                    }
                };
            }
            if (GetTemplateChild("PART_NoButton") is TK_Button no)
            {
                no.Click += (s, e) => { Result = MessageBoxResult.No;
                    if (ShowMessageBox)
                    {
                        ShowMessageBox = false;
                    }
                };
            }
            if (GetTemplateChild("PART_EnterButton") is TK_Button enter)
            {
                enter.Click += (s, e) => { Result = MessageBoxResult.OK;
                    if (ShowMessageBox)
                    {
                        ShowMessageBox = false;
                    }
                };
            }
        }

        #region 属性

        /// <summary>
        /// 消息窗口类型
        /// </summary>
        public MessageBoxType MessageBoxType
        {
            get { return (MessageBoxType)GetValue(MessageBoxTypeProperty); }
            set { SetValue(MessageBoxTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageBoxType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageBoxTypeProperty =
            DependencyProperty.Register("MessageBoxType", typeof(MessageBoxType), typeof(TK_MessageBoxCard), new PropertyMetadata());


        /// <summary>
        /// 消息窗口按钮类型
        /// </summary>
        public MessageBoxButton MessageBoxButton
        {
            get { return (MessageBoxButton)GetValue(MessageBoxButtonProperty); }
            set { SetValue(MessageBoxButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageBoxButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageBoxButtonProperty =
            DependencyProperty.Register("MessageBoxButton", typeof(MessageBoxButton), typeof(TK_MessageBoxCard), new PropertyMetadata());


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
            DependencyProperty.Register("Message", typeof(string), typeof(TK_MessageBoxCard), new PropertyMetadata());



        public bool ShowMessageBox
        {
            get { return (bool)GetValue(ShowMessageBoxProperty); }
            set { SetValue(ShowMessageBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowMessageBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowMessageBoxProperty =
            DependencyProperty.Register("ShowMessageBox", typeof(bool), typeof(TK_MessageBoxCard), new PropertyMetadata());



        public bool Closed
        {
            get { return (bool)GetValue(ClosedProperty); }
            set { SetValue(ClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Closed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClosedProperty =
            DependencyProperty.Register("Closed", typeof(bool), typeof(TK_MessageBoxCard), new PropertyMetadata(false,new PropertyChangedCallback(OnClosedChange)));

        private static void OnClosedChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if((bool)(e.NewValue))
            {
                if(d is TK_MessageBoxCard card)
                {
                    RoutedEventArgs args = new RoutedEventArgs(CloseEvent);
                    card.RaiseEvent(args);
                }
            }
        }
        #endregion

    }
}
