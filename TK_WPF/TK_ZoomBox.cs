using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    ///     <MyNamespace:TK_ZoomBox/>
    ///
    /// </summary>
    [TemplatePart(Name = contentName, Type = typeof(ContentPresenter))]
    public class TK_ZoomBox : GroupBox
    {
        static TK_ZoomBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_ZoomBox), new FrameworkPropertyMetadata(typeof(TK_ZoomBox)));
        }
        public ContentPresenter content;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SizeChanged += TK_ZoomBox_SizeChanged;
            if (GetTemplateChild(contentName) is ContentPresenter content)
            {
                this.content = content;
            }
            if (GetTemplateChild(buttonName) is ToggleButton button)
            {
                button.Checked += Button_Checked;
                button.Unchecked += Button_Unchecked;
            }
        }

        private void Button_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CanExpander)
            {
                this.content.Tag = this.content.ActualHeight;
                Storyboard begin = new Storyboard();
                DoubleAnimation height = new DoubleAnimation()
                {
                    From = (double)this.content.Tag,
                    To = 0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                Storyboard.SetTargetName(height, this.content.Name);
                Storyboard.SetTargetProperty(height, new PropertyPath("Height"));
                begin.Children.Add(height);
                this.content.BeginStoryboard(begin);
            }
        }

        private void Button_Checked(object sender, RoutedEventArgs e)
        {
            Storyboard begin = new Storyboard();
            DoubleAnimation height = new DoubleAnimation()
            {
                From = 0,
                To = (double)this.content.Tag,
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTargetName(height, this.content.Name);
            Storyboard.SetTargetProperty(height, new PropertyPath("Height"));
            begin.Children.Add(height);
            this.content.BeginStoryboard(begin);
        }

        private void TK_ZoomBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is TK_ZoomBox box)
            {
                box.content.Tag = box.content.ActualHeight;
                if (box.IsOpen)
                {
                    this.Button_Checked(null, null);
                }
                else
                {
                    this.Button_Unchecked(null, null);
                }
                this.SizeChanged -= TK_ZoomBox_SizeChanged;
            }
        }

        #region 属性
        /// <summary>
        /// 容器名称
        /// </summary>
        private const string contentName = "PART_Content";
        /// <summary>
        /// 切换开关名称
        /// </summary>
        private const string buttonName = "PART_Button";

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
            DependencyProperty.Register("Icon", typeof(IconCode), typeof(TK_ZoomBox), new PropertyMetadata());
        /// <summary>
        /// 字体图标
        /// </summary>
        public string IconBase
        {
            get { return (string)GetValue(IconBaseProperty); }
            set { SetValue(IconBaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconBase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconBaseProperty =
            DependencyProperty.Register("IconBase", typeof(string), typeof(TK_ZoomBox), new PropertyMetadata());

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(TK_ZoomBox), new PropertyMetadata());


        /// <summary>
        /// 圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TK_ZoomBox), new PropertyMetadata());


        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(TK_ZoomBox), new PropertyMetadata(true));







        /// <summary>
        /// 是否允许展开
        /// </summary>
        public bool CanExpander
        {
            get { return (bool)GetValue(CanExpanderProperty); }
            set { SetValue(CanExpanderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanExpander.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanExpanderProperty =
            DependencyProperty.Register("CanExpander", typeof(bool), typeof(TK_ZoomBox), new PropertyMetadata());


        #endregion
    }
}
