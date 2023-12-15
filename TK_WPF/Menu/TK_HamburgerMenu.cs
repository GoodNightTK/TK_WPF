using System;
using System.Collections.Generic;
using System.Linq;
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
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Menu"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Menu;assembly=TK_WPF.Menu"
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
    ///     <MyNamespace:TK_HamburgerMenu/>
    ///
    /// </summary>
    [TemplatePart(Name =borderName, Type = typeof(Border))]
    [TemplatePart(Name =buttonName, Type = typeof(ToggleButton))]
    public class TK_HamburgerMenu : TabControl
    {
        private const string borderName = "PART_Border";
        private const string buttonName = "PART_MenuButton";
        public Border border;
        static TK_HamburgerMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_HamburgerMenu), new FrameworkPropertyMetadata(typeof(TK_HamburgerMenu)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
           if(GetTemplateChild(borderName)is Border border)
            {
                this.border = border;
                if(!IsOpen)
                {
                    this.Button_Unchecked(null,null);
                }
               else
                {
                    this.Button_Checked(null, null);
                }
            }
           if(GetTemplateChild(buttonName)is ToggleButton button)
            {
                button.Checked += Button_Checked;
                button.Unchecked += Button_Unchecked;
            }
        }

        private void Button_Unchecked(object sender, RoutedEventArgs e)
        {
            Storyboard begin = new Storyboard();
            DoubleAnimation width = new DoubleAnimation()
            {
                From = OpenWidth,
                To = CloseWidth,
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTargetName(width, this.border.Name);
            Storyboard.SetTargetProperty(width, new PropertyPath("Width"));
            begin.Children.Add(width);
            this.border.BeginStoryboard(begin);
        }

        private void Button_Checked(object sender, RoutedEventArgs e)
        {
            Storyboard begin = new Storyboard();
            DoubleAnimation width = new DoubleAnimation()
            {
                From = CloseWidth,
                To = OpenWidth,
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTargetName(width, this.border.Name);

            Storyboard.SetTargetProperty(width, new PropertyPath("Width"));
            begin.Children.Add(width);
            this.border.BeginStoryboard(begin);
        }

        #region 属性

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
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(TK_HamburgerMenu), new PropertyMetadata(false));

       


        /// <summary>
        /// 展开宽度
        /// </summary>
        public double OpenWidth
        {
            get { return (double)GetValue(OpenWidthProperty); }
            set { SetValue(OpenWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenWidthProperty =
            DependencyProperty.Register("OpenWidth", typeof(double), typeof(TK_HamburgerMenu), new PropertyMetadata());


        /// <summary>
        /// 关闭宽度
        /// </summary>
        public double CloseWidth
        {
            get { return (double)GetValue(CloseWidthProperty); }
            set { SetValue(CloseWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseWidthProperty =
            DependencyProperty.Register("CloseWidth", typeof(double), typeof(TK_HamburgerMenu), new PropertyMetadata());


        /// <summary>
        /// 选择后的背景颜色
        /// </summary>
        public Brush SelectBrushBack
        {
            get { return (Brush)GetValue(SelectBrushBackProperty); }
            set { SetValue(SelectBrushBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectBrushBack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectBrushBackProperty =
            DependencyProperty.Register("SelectBrushBack", typeof(Brush), typeof(TK_HamburgerMenu), new PropertyMetadata());


        /// <summary>
        /// 选择后的字体颜色
        /// </summary>
        public Brush SelectBrushFor
        {
            get { return (Brush)GetValue(SelectBrushForProperty); }
            set { SetValue(SelectBrushForProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectBrushFor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectBrushForProperty =
            DependencyProperty.Register("SelectBrushFor", typeof(Brush), typeof(TK_HamburgerMenu), new PropertyMetadata());




        public string Home
        {
            get { return (string)GetValue(HomeProperty); }
            set { SetValue(HomeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Home.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HomeProperty =
            DependencyProperty.Register("Home", typeof(string), typeof(TK_HamburgerMenu), new PropertyMetadata());




        #endregion
    }
}
