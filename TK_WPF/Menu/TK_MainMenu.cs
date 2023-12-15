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
    ///     <MyNamespace:TK_MainMenu/>
    ///
    /// </summary>
    [TemplatePart(Name = ContentName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ExpanderButtonName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = MenuName, Type = typeof(Grid))]
    [TemplatePart(Name =DefaultButtonName, Type = typeof(TK_Button))]
    public class TK_MainMenu : TreeView
    {
        private const string ContentName = "PART_Content";
        private const string ExpanderButtonName = "PART_ExpanderButton";
        private const string MenuName = "PART_Menu";
        private const string DefaultButtonName = "PART_DefaultButton";
        private ContentPresenter contentControl;
        private Grid grid;
        static TK_MainMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_MainMenu), new FrameworkPropertyMetadata(typeof(TK_MainMenu)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild(ContentName) is ContentPresenter contetn)
            {
                this.contentControl = contetn;
            }
            if (GetTemplateChild(ExpanderButtonName) is ToggleButton button)
            {
                button.Checked += Button_Checked;
                button.Unchecked += Button_Unchecked;
            }
            if (GetTemplateChild(MenuName) is Grid grid)
            {
                this.grid = grid;
            }
            if(GetTemplateChild(DefaultButtonName)is TK_Button bt)
            {
                bt.Click += (s, e) =>
                {
                    this.contentControl.Content = null;
                    if (this.DefaultContent != null)
                    {
                        this.contentControl.Content = this.DefaultContent;
                    }
                    else
                    {
                        this.contentControl.Content = new TK_Icon() { IconCode = IconCode.System_Home_Fill, Foreground = Brushes.Black, FontSize = 80 };
                    }
                };
            }
            this.SelectedItemChanged += OnSelectedItemChanged;
            this.OnSelectedItemChanged(null, null);
        }

        private void Button_Unchecked(object sender, RoutedEventArgs e)
        {
            Storyboard begin = new Storyboard();
            DoubleAnimation width = new DoubleAnimation()
            {
                To = this.MenuWidth,
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTargetName(width, this.grid.Name);
            Storyboard.SetTargetProperty(width, new PropertyPath("Width"));
            begin.Children.Add(width);
            this.grid.BeginStoryboard(begin);
        }

        private void Button_Checked(object sender, RoutedEventArgs e)
        {
            Storyboard begin = new Storyboard();
            DoubleAnimation width = new DoubleAnimation()
            {
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTargetName(width, this.grid.Name);
            Storyboard.SetTargetProperty(width, new PropertyPath("Width"));
            begin.Children.Add(width);
            this.grid.BeginStoryboard(begin);
        }

        private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.SelectedItem is TK_WindowMenu windowMenu)
            {
                if (windowMenu.Children.Count == 0 && windowMenu.Content != null && windowMenu.Content is UserControl)
                {
                    this.contentControl.Content = null;
                    this.contentControl.Content = windowMenu.Content;
                    return;
                }
            }
            //this.contentControl.Content = null;
            //if (this.DefaultContent != null)
            //{
            //    this.contentControl.Content = this.DefaultContent;
            //}
            //else
            //{
            //    this.contentControl.Content = new TK_Icon() { IconCode = IconCode.System_Home_Fill ,Foreground=Brushes.Black,FontSize=80};
            //}
        }

        #region 属性

        /// <summary>
        /// 功能栏宽度
        /// </summary>
        public double MenuWidth
        {
            get { return (double)GetValue(MenuWidthProperty); }
            set { SetValue(MenuWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuWidthProperty =
            DependencyProperty.Register("MenuWidth", typeof(double), typeof(TK_MainMenu), new PropertyMetadata());


        ///// <summary>
        ///// 功能高度
        ///// </summary>
        //public double MenuItemHeigth
        //{
        //    get { return (double)GetValue(MenuItemHeigthProperty); }
        //    set { SetValue(MenuItemHeigthProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MenuItemHeigth.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MenuItemHeigthProperty =
        //    DependencyProperty.Register("MenuItemHeigth", typeof(double), typeof(TK_MainMenu), new PropertyMetadata());


        /// <summary>
        /// 上方标题内容
        /// </summary>
        public object TopTitelContent
        {
            get { return (object)GetValue(TopTitelContentProperty); }
            set { SetValue(TopTitelContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopTitelContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopTitelContentProperty =
            DependencyProperty.Register("TopTitelContent", typeof(object), typeof(TK_MainMenu), new PropertyMetadata());


        /// <summary>
        /// 功能栏下方内容
        /// </summary>
        public object MenuBottonContent
        {
            get { return (object)GetValue(MenuBottonContentProperty); }
            set { SetValue(MenuBottonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuBottonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuBottonContentProperty =
            DependencyProperty.Register("MenuBottonContent", typeof(object), typeof(TK_MainMenu), new PropertyMetadata());




        /// <summary>
        /// 下方标题内容
        /// </summary>
        public object BottonTitelContent
        {
            get { return (object)GetValue(BottonTitelContentProperty); }
            set { SetValue(BottonTitelContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottonTitelContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottonTitelContentProperty =
            DependencyProperty.Register("BottonTitelContent", typeof(object), typeof(TK_MainMenu), new PropertyMetadata());


        /// <summary>
        /// 聚焦后的背景颜色
        /// </summary>
        public Brush MouseOverBackBrush
        {
            get { return (Brush)GetValue(MouseOverBackBrushProperty); }
            set { SetValue(MouseOverBackBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverBackBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackBrushProperty =
            DependencyProperty.Register("MouseOverBackBrush", typeof(Brush), typeof(TK_MainMenu), new PropertyMetadata());


        /// <summary>
        /// 聚焦后的前景色
        /// </summary>
        public Brush MouseOverFroBrush
        {
            get { return (Brush)GetValue(MouseOverFroBrushProperty); }
            set { SetValue(MouseOverFroBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverFroBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverFroBrushProperty =
            DependencyProperty.Register("MouseOverFroBrush", typeof(Brush), typeof(TK_MainMenu), new PropertyMetadata());


        /// <summary>
        /// 功能栏上方内容
        /// </summary>
        public object MenuTopContent
        {
            get { return (object)GetValue(MenuTopContentProperty); }
            set { SetValue(MenuTopContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuTopContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuTopContentProperty =
            DependencyProperty.Register("MenuTopContent", typeof(object), typeof(TK_MainMenu), new PropertyMetadata());



        /// <summary>
        /// 默认主页内容
        /// </summary>
        public object DefaultContent
        {
            get { return (object)GetValue(DefaultContentProperty); }
            set { SetValue(DefaultContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultContentProperty =
            DependencyProperty.Register("DefaultContent", typeof(object), typeof(TK_MainMenu), new PropertyMetadata());
        #endregion
    }
}
