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
    ///     <MyNamespace:TK_ProgressBar_Circle/>
    ///
    /// </summary>
    public class TK_ProgressBar_Circle : ProgressBar
    {
        static TK_ProgressBar_Circle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_ProgressBar_Circle), new FrameworkPropertyMetadata(typeof(TK_ProgressBar_Circle)));
        }


        #region 属性
        /// <summary>
        /// 进度条颜色
        /// </summary>
        public Brush ProgressBarBrush
        {
            get { return (Brush)GetValue(ProgressBarBrushProperty); }
            set { SetValue(ProgressBarBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressBarBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarBrushProperty =
            DependencyProperty.Register("ProgressBarBrush", typeof(Brush), typeof(TK_ProgressBar_Circle), new PropertyMetadata());


        /// <summary>
        /// 显示百分比信息
        /// </summary>
        public bool ShowPercent
        {
            get { return (bool)GetValue(ShowPercentProperty); }
            set { SetValue(ShowPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPercent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowPercentProperty =
            DependencyProperty.Register("ShowPercent", typeof(bool), typeof(TK_ProgressBar_Circle), new PropertyMetadata());

        /// <summary>
        /// 进度条宽度(圆形进度条)
        /// </summary>
        public double ProgressBarThickness
        {
            get { return (double)GetValue(ProgressBarThicknessProperty); }
            set { SetValue(ProgressBarThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressBarThiness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarThicknessProperty =
            DependencyProperty.Register("ProgressBarThickness", typeof(double), typeof(TK_ProgressBar_Circle), new PropertyMetadata());


        /// <summary>
        /// 进度条起始位置(圆形进度条)
        /// </summary>
        public Dock ProgressBarAlignment
        {
            get { return (Dock)GetValue(ProgressBarAlignmentProperty); }
            set { SetValue(ProgressBarAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressBarAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarAlignmentProperty =
            DependencyProperty.Register("ProgressBarAlignment", typeof(Dock), typeof(TK_ProgressBar_Circle), new PropertyMetadata());
        #endregion
    }
}
