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
    ///     <MyNamespace:TK_Transition/>
    ///
    /// </summary>
    public class TK_Transition : ContentControl
    {
        static TK_Transition()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_Transition), new FrameworkPropertyMetadata(typeof(TK_Transition)));
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Loaded += Transition_Loaded;
        }

        private void Transition_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsShow && Type != TransitionType.None)
            {
                ShowAnimation(this);

                if (!PlayEveryTime)
                {
                    Loaded -= Transition_Loaded;
                }
            }
            else if (Type >= TransitionType.CollapseUp && Type <= TransitionType.CollapseRight && CollapsedSize > 0)
            {
                if (Content is FrameworkElement element)
                {
                    Progress = CollapsedSize / element.ActualWidth;
                }
            }

            Loaded -= Transition_Loaded;
        }

        #region 命令

        /// <summary>
        /// 显示完命令
        /// </summary>
        public static readonly DependencyProperty ShowedCommandProperty =
            DependencyProperty.Register("ShowedCommand", typeof(ICommand), typeof(TK_Transition), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 显示完命令
        /// </summary>
        public ICommand ShowedCommand
        {
            get { return (ICommand)GetValue(ShowedCommandProperty); }
            set { SetValue(ShowedCommandProperty, value); }
        }

        /// <summary>
        /// 关闭完命令
        /// </summary>
        public static readonly DependencyProperty ClosedCommandProperty =
            DependencyProperty.Register("ClosedCommand", typeof(ICommand), typeof(TK_Transition), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 关闭完命令
        /// </summary>
        public ICommand ClosedCommand
        {
            get { return (ICommand)GetValue(ClosedCommandProperty); }
            set { SetValue(ClosedCommandProperty, value); }
        }

        #endregion 命令

        #region 事件

        /// <summary>
        /// 显示完事件
        /// </summary>
        public static readonly RoutedEvent ShowedEvent =
           EventManager.RegisterRoutedEvent("Showed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TK_Transition));

        /// <summary>
        /// 显示完事件 handler
        /// </summary>
        public event RoutedEventHandler Showed
        {
            add { AddHandler(ShowedEvent, value); }
            remove { RemoveHandler(ShowedEvent, value); }
        }

        /// <summary>
        /// 关闭完事件
        /// </summary>
        public static readonly RoutedEvent ClosedEvent =
          EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TK_Transition));

        /// <summary>
        /// 关闭完事件 handler
        /// </summary>
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        #endregion 事件

        #region 依赖属性

        /// <summary>
        /// 是否显示
        /// </summary>
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(TK_Transition), new PropertyMetadata(false, OnIsShowChanged));

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        /// <summary>
        /// 每次加载显示动画
        /// </summary>
        public static readonly DependencyProperty PlayEveryTimeProperty =
            DependencyProperty.Register("PlayEveryTime", typeof(bool), typeof(TK_Transition), new PropertyMetadata(false));

        /// <summary>
        /// 每次加载显示动画
        /// </summary>
        public bool PlayEveryTime
        {
            get { return (bool)GetValue(PlayEveryTimeProperty); }
            set { SetValue(PlayEveryTimeProperty, value); }
        }

        /// <summary>
        /// 转换类型
        /// </summary>
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(TransitionType), typeof(TK_Transition), new PropertyMetadata(default(TransitionType)));

        /// <summary>
        /// 转换类型
        /// </summary>
        public TransitionType Type
        {
            get { return (TransitionType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// 动画时长
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(TK_Transition), new PropertyMetadata(default(Duration)));

        /// <summary>
        /// 动画时长
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// 动画位置偏移
        /// </summary>
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(double), typeof(TK_Transition), new PropertyMetadata(default(double)));

        /// <summary>
        /// 动画位置偏移
        /// </summary>
        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        /// <summary>
        /// 动画初始缩放
        /// </summary>
        public static readonly DependencyProperty InitialScaleProperty =
            DependencyProperty.Register("InitialScale", typeof(double), typeof(TK_Transition), new PropertyMetadata(default(double)));

        /// <summary>
        /// 动画初始缩放
        /// </summary>
        public double InitialScale
        {
            get { return (double)GetValue(InitialScaleProperty); }
            set { SetValue(InitialScaleProperty, value); }
        }

        /// <summary>
        /// 显示缓动
        /// </summary>
        public static readonly DependencyProperty ShowEasingFunctionProperty =
            DependencyProperty.Register("ShowEasingFunction", typeof(IEasingFunction), typeof(TK_Transition), new PropertyMetadata(default(IEasingFunction)));

        /// <summary>
        /// 显示缓动
        /// </summary>
        public IEasingFunction ShowEasingFunction
        {
            get { return (IEasingFunction)GetValue(ShowEasingFunctionProperty); }
            set { SetValue(ShowEasingFunctionProperty, value); }
        }

        /// <summary>
        /// 关闭缓动
        /// </summary>
        public static readonly DependencyProperty CloseEasingFunctionProperty =
            DependencyProperty.Register("CloseEasingFunction", typeof(IEasingFunction), typeof(TK_Transition), new PropertyMetadata(default(IEasingFunction)));

        /// <summary>
        /// 关闭缓动
        /// </summary>
        public IEasingFunction CloseEasingFunction
        {
            get { return (IEasingFunction)GetValue(CloseEasingFunctionProperty); }
            set { SetValue(CloseEasingFunctionProperty, value); }
        }

        /// <summary>
        /// 动画进度
        /// 显示完成 = 1
        /// 隐藏完成 = 0
        /// </summary>
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(TK_Transition), new PropertyMetadata(default(double)));

        /// <summary>
        /// 动画进度
        /// 显示完成 = 1
        /// 隐藏完成 = 0
        /// </summary>
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        /// <summary>
        /// 是否淡入淡出
        /// </summary>
        public static readonly DependencyProperty IsFadeProperty =
            DependencyProperty.Register("IsFade", typeof(bool), typeof(TK_Transition), new PropertyMetadata(false));

        /// <summary>
        /// 是否淡入淡出
        /// </summary>
        public bool IsFade
        {
            get { return (bool)GetValue(IsFadeProperty); }
            set { SetValue(IsFadeProperty, value); }
        }

        /// <summary>
        /// 折叠后的大小
        /// </summary>
        public static readonly DependencyProperty CollapsedSizeProperty =
            DependencyProperty.Register("CollapsedSize", typeof(double), typeof(TK_Transition), new PropertyMetadata(default(double), OnIsShowChanged));

        /// <summary>
        /// 折叠后的大小
        /// </summary>
        public double CollapsedSize
        {
            get { return (double)GetValue(CollapsedSizeProperty); }
            set { SetValue(CollapsedSizeProperty, value); }
        }

        #endregion 依赖属性

        /// <summary>
        /// 关闭转换器
        /// </summary>
        /// <returns>关闭是否完成</returns>
        public async Task<bool> Close()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            this.Closed += (a, b) => taskCompletionSource.TrySetResult(true);
            return await taskCompletionSource.Task;
        }

        private static void OnIsShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var transition = d as TK_Transition;
            ChangeTransitionVisual(transition);
        }

        private static void ChangeTransitionVisual(TK_Transition transition)
        {
            if (transition.IsLoaded && transition.Type != TransitionType.None)
            {
                if (transition.IsShow)
                {
                    ShowAnimation(transition);
                }
                else
                {
                    CloseAnimation(transition);
                }
            }
            else if (transition.IsInitialized || transition.Type == TransitionType.None)
            {
                transition.BeginAnimation(TK_Transition.ProgressProperty, null);

                if (transition.IsShow)
                {
                    transition.Progress = 1;
                }
                else
                {
                    transition.Progress = 0;
                }
            }
        }

        /// <summary>
        /// 开始显示动画
        /// </summary>
        /// <param name="transition">转换</param>
        public static void ShowAnimation(TK_Transition transition)
        {
            if (transition.Type == TransitionType.None)
            {
                return;
            }

            Storyboard storyboard = new Storyboard();
            storyboard.Completed += (sender, e) =>
            {
                var args = new RoutedEventArgs();
                args.RoutedEvent = TK_Transition.ShowedEvent;
                transition.RaiseEvent(args);
                transition.ShowedCommand?.Execute(null);
            };

            DoubleAnimation progressAnimation;
            if (transition.Type >= TransitionType.CollapseUp && transition.Type <= TransitionType.CollapseRight)
            {
                progressAnimation = GetProgressAnimation(transition, 1, transition.ShowEasingFunction);
            }
            else
            {
                progressAnimation = GetProgressAnimation(transition, 1, transition.ShowEasingFunction, from: 0);
            }

            switch (transition.Type)
            {
                case TransitionType.Fade:
                case TransitionType.CollapseUp:
                case TransitionType.CollapseDown:
                case TransitionType.CollapseLeft:
                case TransitionType.CollapseRight:
                default:
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.FadeLeft:
                    DoubleAnimation translateAnimation = GetTranslateXAnimation(transition, transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.FadeRight:
                    translateAnimation = GetTranslateXAnimation(transition, -transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.FadeUp:
                    translateAnimation = GetTranslateYAnimation(transition, transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.FadeDown:
                    translateAnimation = GetTranslateYAnimation(transition, -transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.Zoom:
                    DoubleAnimation scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    DoubleAnimation scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomX:
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomY:
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomLeft:
                    transition.RenderTransformOrigin = new Point(0, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomRight:
                    transition.RenderTransformOrigin = new Point(1, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomUp:
                    transition.RenderTransformOrigin = new Point(0.5, 0);
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomDown:
                    transition.RenderTransformOrigin = new Point(0.5, 1);
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;
            }

            storyboard.Begin();
        }

        /// <summary>
        /// 开始关闭动画
        /// </summary>
        /// <param name="transition">转换</param>
        public static void CloseAnimation(TK_Transition transition)
        {
            if (transition.Type == TransitionType.None)
            {
                return;
            }

            Storyboard storyboard = new Storyboard();
            storyboard.Completed += (sender, e) =>
            {
                var args = new RoutedEventArgs();
                args.RoutedEvent = TK_Transition.ClosedEvent;
                transition.RaiseEvent(args);
                transition.ClosedCommand?.Execute(null);
            };

            DoubleAnimation progressAnimation;
            if (transition.Type >= TransitionType.CollapseUp && transition.Type <= TransitionType.CollapseRight)
            {
                var progress = GetCollapsedProgress(transition);
                progressAnimation = GetProgressAnimation(transition, progress, transition.CloseEasingFunction);
            }
            else
            {
                progressAnimation = GetProgressAnimation(transition, 0, transition.CloseEasingFunction, from: 1);
            }

            switch (transition.Type)
            {
                case TransitionType.Fade:
                case TransitionType.CollapseUp:
                case TransitionType.CollapseDown:
                case TransitionType.CollapseLeft:
                case TransitionType.CollapseRight:
                default:
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.FadeLeft:
                    DoubleAnimation translateAnimation = GetTranslateXAnimation(transition, 0, transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.FadeRight:
                    translateAnimation = GetTranslateXAnimation(transition, 0, -transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.FadeUp:
                    translateAnimation = GetTranslateYAnimation(transition, 0, transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.FadeDown:
                    translateAnimation = GetTranslateYAnimation(transition, 0, -transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.Zoom:
                    DoubleAnimation scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    DoubleAnimation scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomX:
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomY:
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomLeft:
                    transition.RenderTransformOrigin = new Point(0, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomRight:
                    transition.RenderTransformOrigin = new Point(1, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomUp:
                    transition.RenderTransformOrigin = new Point(0.5, 0);
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;

                case TransitionType.ZoomDown:
                    transition.RenderTransformOrigin = new Point(0.5, 1);
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(progressAnimation);
                    break;
            }

            storyboard.Begin();
        }

        private static DoubleAnimation GetProgressAnimation(TK_Transition transition, double to, IEasingFunction easing, double? from = null)
        {
            DoubleAnimation progressAnimation = new DoubleAnimation()
            {
                From = transition.Progress == to ? (to > 0 ? 0 : 1) : from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            if (from != null)
            {
                progressAnimation.From = from;
            }

            Storyboard.SetTargetProperty(progressAnimation, new PropertyPath(ProgressProperty));
            Storyboard.SetTarget(progressAnimation, transition);

            return progressAnimation;
        }

        private static DoubleAnimation GetTranslateXAnimation(TK_Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetTranslateYAnimation(TK_Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetScaleXAnimation(TK_Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetScaleYAnimation(TK_Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static double GetCollapsedProgress(TK_Transition transition)
        {
            switch (transition.Type)
            {
                case TransitionType.CollapseUp:
                case TransitionType.CollapseDown:
                    return transition.ActualHeight == 0 ? 0 : transition.CollapsedSize / transition.ActualHeight;

                case TransitionType.CollapseLeft:
                case TransitionType.CollapseRight:
                    return transition.ActualWidth == 0 ? 0 : transition.CollapsedSize / transition.ActualWidth;

                default:
                    return 0;
            }
        }
    }

    /// <summary>
    /// 转换类型
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 淡入
        /// </summary>
        Fade,

        /// <summary>
        /// 向左淡入
        /// </summary>
        FadeLeft,

        /// <summary>
        /// 向右淡入
        /// </summary>
        FadeRight,

        /// <summary>
        /// 向上淡入
        /// </summary>
        FadeUp,

        /// <summary>
        /// 向下淡入
        /// </summary>
        FadeDown,

        /// <summary>
        /// 缩放
        /// </summary>
        Zoom,

        /// <summary>
        /// X 轴缩放
        /// </summary>
        ZoomX,

        /// <summary>
        /// Y 轴缩放
        /// </summary>
        ZoomY,

        /// <summary>
        /// 向左缩放
        /// </summary>
        ZoomLeft,

        /// <summary>
        /// 向右缩放
        /// </summary>
        ZoomRight,

        /// <summary>
        /// 向上缩放
        /// </summary>
        ZoomUp,

        /// <summary>
        /// 向下缩放
        /// </summary>
        ZoomDown,

        /// <summary>
        /// 向上折叠
        /// </summary>
        CollapseUp,

        /// <summary>
        /// 向下折叠
        /// </summary>
        CollapseDown,

        /// <summary>
        /// 向左折叠
        /// </summary>
        CollapseLeft,

        /// <summary>
        /// 向右折叠
        /// </summary>
        CollapseRight,
    }
}
