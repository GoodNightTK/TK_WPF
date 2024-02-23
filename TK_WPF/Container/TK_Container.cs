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
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Container"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Container;assembly=TK_WPF.Container"
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
    ///     <MyNamespace:TK_Container/>
    ///
    /// </summary>
    [TemplatePart(Name = MessageStackPanelName, Type = typeof(StackPanel))]
    [TemplatePart(Name = NotificationStackPanelName, Type = typeof(StackPanel))]
    [TemplatePart(Name = CenterContentName, Type = typeof(ContentControl))]
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    public class TK_Container : ContentControl
    {
        static TK_Container()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_Container), new FrameworkPropertyMetadata(typeof(TK_Container)));
        }


        /// <summary>
        /// 消息堆叠面板名称
        /// </summary>
        public const string MessageStackPanelName = "PART_MessageStackPanel";
        /// <summary>
        /// 通知堆叠面板名称
        /// </summary>
        public const string NotificationStackPanelName = "PART_NotificationStackPanel";
        /// <summary>
        /// 居中面板名称
        /// </summary>
        public const string CenterContentName = "PART_CenterContent";
        /// <summary>
        /// 显示面板名称
        /// </summary>
        public const string ContentPresenterName = "PART_ContentPresenter";

        private StackPanel messageStackPanel;
        private StackPanel notificationStackPanel;
        public ContentControl centerContent;


        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            messageStackPanel = GetTemplateChild(MessageStackPanelName) as StackPanel;
            notificationStackPanel = GetTemplateChild(NotificationStackPanelName) as StackPanel;
            centerContent = GetTemplateChild(CenterContentName) as ContentControl;
            if (GetTemplateChild(ContentPresenterName) is ContentPresenter contentPresenter)
            {
                contentPresenter.PreviewKeyDown += ContentPresenter_PreviewKeyDown;
            }
            SizeChanged += TK_Container_SizeChanged;
        }

        private void TK_Container_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TK_Container container)
            {
                centerContent.MaxHeight = container.ActualHeight * 9 / 10;
                centerContent.MaxWidth = container.ActualWidth * 9 / 10;
            }
        }

        private void ContentPresenter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key == Key.System ? e.SystemKey : e.Key;
            if (key == Key.Tab && IsBusy)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 标识
        /// </summary>
        public static readonly DependencyProperty IdentifierProperty =
            DependencyProperty.Register("Identifier", typeof(string), typeof(TK_Container), new PropertyMetadata(default(string), OnIdentifierChanged));

        /// <summary>
        /// 标识
        /// </summary>
        public string Identifier
        {
            get { return (string)GetValue(IdentifierProperty); }
            set { SetValue(IdentifierProperty, value); }
        }

        private static void OnIdentifierChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TK_Container container)
            {
                string identify = e.NewValue.ToString();
                TK_Message.UpdateContainer(container, identify);
            }
        }





        /// <summary>
        /// 居中弹窗运行中
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(TK_Container), new PropertyMetadata());









        /// <summary>
        /// 添加消息卡片
        /// </summary>
        /// <param name="messageCard">消息卡片</param>
        internal void AddMessageCard(TK_MessageCard messageCard)
        {
            _ = messageStackPanel?.Children.Add(messageCard);
        }

        /// <summary>
        /// 移除消息卡片
        /// </summary>
        /// <param name="messageCard"></param>
        internal void RemoveMessageCard(TK_MessageCard messageCard)
        {
            messageStackPanel?.Children.Remove(messageCard);
        }
        /// <summary>
        /// 添加通知卡片
        /// </summary>
        /// <param name="notificationCard"></param>
        internal void AddNotificationCard(TK_NotificationCard notificationCard)
        {
            _ = notificationStackPanel?.Children.Add(notificationCard);
        }
        /// <summary>
        /// 移除通知卡片
        /// </summary>
        /// <param name="notificationCard"></param>
        internal void RemoveNotificationCard(TK_NotificationCard notificationCard)
        {
            notificationStackPanel?.Children.Remove(notificationCard);
        }
        /// <summary>
        /// 添加居中通知
        /// </summary>
        /// <param name="win"></param>
        internal void AddCenter(object win)
        {
            centerContent.Content = win;
        }
        /// <summary>
        /// 移除居中通知
        /// </summary>
        /// <param name="win"></param>
        internal void RemoveCenter(object win)
        {
            this.TK_Container_SizeChanged(this, null);
            centerContent.Content = null;
        }
    }
}
