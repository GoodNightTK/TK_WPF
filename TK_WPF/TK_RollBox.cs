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
using System.Windows.Media.Animation;
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
    ///     <MyNamespace:TK_RollBox/>
    ///
    /// </summary>
    [TemplatePart(Name = currContentName, Type = typeof(ContentControl))]
    [TemplatePart(Name = nextContentName, Type = typeof(ContentControl))]
    [TemplatePart(Name = leftButtonName, Type = typeof(TK_Button))]
    [TemplatePart(Name = rightButtonName, Type = typeof(TK_Button))]
    public class TK_RollBox : ContentControl
    {
        private const string currContentName = "PART_CURR_Content";
        private const string nextContentName = "PART_NEXT_Content";
        private const string leftButtonName = "PART_LeftButton";
        private const string rightButtonName = "PART_RightButton";



        private DispatcherTimer timer = new DispatcherTimer();
        private ContentControl CURR_Content;
        private ContentControl NEXT_Content;

        static TK_RollBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_RollBox), new FrameworkPropertyMetadata(typeof(TK_RollBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild(currContentName) is ContentControl cc)
            {
                this.CURR_Content = cc;
            }
            if (GetTemplateChild(nextContentName) is ContentControl nc)
            {
                this.NEXT_Content = nc;
            }
            if (GetTemplateChild(leftButtonName) is TK_Button lb)
            {
                lb.Click += (s, e) =>
                {
                    this.timer.Stop();
                    this.IndexChange(false);
                    this.timer.Start();
                };
            }
            if (GetTemplateChild(rightButtonName) is TK_Button rb)
            {
                rb.Click += (s, e) =>
                {
                    this.timer.Stop();
                    this.IndexChange(true);
                    this.timer.Start();
                };
            }
            this.IndexChange(true);
            this.timer.Interval = new TimeSpan(0, 0, this.DelayTime);
            this.timer.Tick += (s, e) =>
            {
                if (this.AutoRoll)
                {
                    this.IndexChange(true);
                }
            };
            this.timer.Start();
        }



        int index = 0;

        int preindex = 0;


        private void IndexChange(bool from)
        {
            if (from)
            {
                preindex = index;
                index++;
                if (index >= Items.Count)
                {
                    index = 0;
                }
            }
            else
            {
                preindex = index;
                index--;
                if (index < 0)
                {
                    index = this.Items.Count - 1;
                }
            }
            if (Items.Count == 0)
                return;


            if (CURR_Content.Content == null)
            {
                CURR_Content.Content = Items[index];
                return;//首次不需要动画
            }

            dhStart(from);
        }

        private void dhStart(bool from)
        {
            NEXT_Content.Content = Items[index];
            CURR_Content.Content = Items[preindex];

            ThicknessAnimation Curr_marginAnimation = new ThicknessAnimation();
            if (from)
            {
                Curr_marginAnimation.From = new Thickness(0, 0, 0, 0);
                Curr_marginAnimation.To = new Thickness(-this.ActualWidth, 0, this.ActualWidth, 0);
            }
            else
            {
                Curr_marginAnimation.From = new Thickness(0, 0, 0, 0);
                Curr_marginAnimation.To = new Thickness(this.ActualWidth, 0, -this.ActualWidth, 0);
            }
            Curr_marginAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));


            ThicknessAnimation Next_marginAnimation = new ThicknessAnimation();
            if (from)
            {
                Next_marginAnimation.From = new Thickness(this.ActualWidth, 0, -this.ActualWidth, 0);
                Next_marginAnimation.To = new Thickness(0, 0, 0, 0);
            }
            else
            {
                Next_marginAnimation.From = new Thickness(-this.ActualWidth, 0, this.ActualWidth, 0);
                Next_marginAnimation.To = new Thickness(0, 0, 0, 0);
            }
            Next_marginAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            NEXT_Content.BeginAnimation(ContentControl.MarginProperty, Next_marginAnimation);

            CURR_Content.BeginAnimation(ContentControl.MarginProperty, Curr_marginAnimation);

        }



        #region 依赖属性
        /// <summary>
        /// 自动轮播
        /// </summary>
        public bool AutoRoll
        {
            get { return (bool)GetValue(AutoRollProperty); }
            set { SetValue(AutoRollProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoRoll.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoRollProperty =
            DependencyProperty.Register("AutoRoll", typeof(bool), typeof(TK_RollBox), new PropertyMetadata());


        /// <summary>
        /// 轮播延时
        /// </summary>
        public int DelayTime
        {
            get { return (int)GetValue(DelayTimeProperty); }
            set { SetValue(DelayTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DelayTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayTimeProperty =
            DependencyProperty.Register("DelayTime", typeof(int), typeof(TK_RollBox), new PropertyMetadata());


        /// <summary>
        /// 轮播集合
        /// </summary>
        public List<FrameworkElement> Items
        {
            get { return (List<FrameworkElement>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<FrameworkElement>), typeof(TK_RollBox), new PropertyMetadata(new List<FrameworkElement>()));
        #endregion
    }
}
