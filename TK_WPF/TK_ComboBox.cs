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
    ///     <MyNamespace:TK_ComboBox/>
    ///
    /// </summary>
    [TemplatePart(Name =ClearButtonName,Type =(typeof(TK_Button)))]
    public class TK_ComboBox : ComboBox
    {
        private const string ClearButtonName = "PART_ClearButton";
        static TK_ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_ComboBox), new FrameworkPropertyMetadata(typeof(TK_ComboBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if(GetTemplateChild(ClearButtonName) is TK_Button button )
            {
                button.Click += Button_Click;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedIndex = -1;
        }

        #region 属性
        /// <summary>
        /// 边框圆角
        /// </summary>
        public CornerRadius Corner
        {
            get { return (CornerRadius)GetValue(CornerProperty); }
            set { SetValue(CornerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Corner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerProperty =
            DependencyProperty.Register("Corner", typeof(CornerRadius), typeof(TK_ComboBox), new PropertyMetadata());

        /// <summary>
        /// 水印
        /// </summary>
        public string TextMark
        {
            get { return (string)GetValue(TextMarkProperty); }
            set { SetValue(TextMarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextMark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextMarkProperty =
            DependencyProperty.Register("TextMark", typeof(string), typeof(TK_ComboBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 前缀
        /// </summary>
        public object FirstContent
        {
            get { return (object)GetValue(FirstContentProperty); }
            set { SetValue(FirstContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FirstContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstContentProperty =
            DependencyProperty.Register("FirstContent", typeof(object), typeof(TK_ComboBox), new PropertyMetadata());

        /// <summary>
        /// 后缀
        /// </summary>
        public object LastContent
        {
            get { return (object)GetValue(LastContentProperty); }
            set { SetValue(LastContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastContentProperty =
            DependencyProperty.Register("LastContent", typeof(object), typeof(TK_ComboBox), new PropertyMetadata());



        /// <summary>
        /// 鼠标放置边框颜色
        /// </summary>
        public Brush MouseOverBorBrush
        {
            get { return (Brush)GetValue(MouseOverBorBrushProperty); }
            set { SetValue(MouseOverBorBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverBorBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBorBrushProperty =
            DependencyProperty.Register("MouseOverBorBrush", typeof(Brush), typeof(TK_ComboBox), new PropertyMetadata());






        #endregion
    }
}
