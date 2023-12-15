using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
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
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Icon"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Icon;assembly=TK_WPF.Icon"
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
    ///     <MyNamespace:TK_Icon/>
    ///
    /// </summary>
    public class TK_Icon : Control
    {
        static TK_Icon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_Icon), new FrameworkPropertyMetadata(typeof(TK_Icon)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateIcon();
        }

        public string Base
        {
            get { return (string)GetValue(BaseProperty); }
            set { SetValue(BaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Base.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BaseProperty =
            DependencyProperty.Register("Base", typeof(string), typeof(TK_Icon), new PropertyMetadata());



        public IconCode IconCode
        {
            get { return (IconCode)GetValue(IconCodeProperty); }
            set { SetValue(IconCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconCode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconCodeProperty =
            DependencyProperty.Register("IconCode", typeof(IconCode), typeof(TK_Icon), new PropertyMetadata(IconCodeChange));

        private static void IconCodeChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TK_Icon)d).UpdateIcon();
        }

        public string IconBase
        {
            get { return (string)GetValue(IconBaseProperty); }
            set { SetValue(IconBaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconBase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconBaseProperty =
            DependencyProperty.Register("IconBase", typeof(string), typeof(TK_Icon), new PropertyMetadata());



        private void UpdateIcon()
        {
            if (TK_IconData.IconDatas.TryGetValue(this.IconCode, out string data))
            {
                if (this.IconCode != IconCode.Node)
                {
                    this.Base = data;
                }
            }
            else
            {
                this.Base = this.IconBase;
            }
        }
    }
}
