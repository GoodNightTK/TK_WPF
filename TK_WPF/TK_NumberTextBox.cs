using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// 数值类型
    /// </summary>
    public enum NumericType
    {
        /// <summary>
        /// int type
        /// </summary>
        [Description("int type")]
        Int = 0,

        /// <summary>
        /// double type
        /// </summary>
        [Description("double type")]
        Double,
    }
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
    ///     <MyNamespace:TK_NumberTextBox/>
    ///
    /// </summary>
    [TemplatePart(Name = TextBoxPartName, Type = typeof(TextBox))]
    [TemplatePart(Name = ButtonIncreasePartName, Type = typeof(TK_RepeatButton))]
    [TemplatePart(Name = ButtonDecreasePartName, Type = typeof(TK_RepeatButton))]
    public class TK_NumberTextBox : Control
    {


        static TK_NumberTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_NumberTextBox), new FrameworkPropertyMetadata(typeof(TK_NumberTextBox)));
        }
        /// <summary>
        /// 文本框名称
        /// </summary>
        public const string TextBoxPartName = "PART_TextBox";

        /// <summary>
        /// 增加按钮名称
        /// </summary>
        public const string ButtonIncreasePartName = "PART_IncreaseButton";

        /// <summary>
        /// 减少按钮名称
        /// </summary>
        public const string ButtonDecreasePartName = "PART_DecreaseButton";

        /// <summary>
        /// 默认 int 类型正则匹配
        /// </summary>
        public const string DefaultIntPattern = "[0-9-]";

        /// <summary>
        /// 默认 double 类型正则匹配
        /// </summary>
        public const string DefaultDoublePattern = "[0-9-+Ee\\.]";

        private TextBox textBox = new TextBox();

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild(TextBoxPartName) is TextBox textBox)
            {
                var textBinding = new Binding(nameof(Text));
                textBinding.Source = this;
                textBinding.Mode = BindingMode.TwoWay;
                textBox.SetBinding(TextBox.TextProperty, textBinding);
                textBox.TextChanged += TextBox_TextChanged;
                textBox.PreviewTextInput += TextBox_PreviewTextInput;
                textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
                textBox.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, null, new CanExecuteRoutedEventHandler(TextBox_CanExecutePaste)));
                this.textBox = textBox;
            }
            if (GetTemplateChild(ButtonIncreasePartName) is TK_RepeatButton upButton)
            {
                upButton.Click += IncreaseButton_Click;
            }

            if (GetTemplateChild(ButtonDecreasePartName) is TK_RepeatButton downButton)
            {
                downButton.Click += DecreaseButton_Click;
            }
        }

        #region 事件

        /// <summary>
        /// 值改变事件
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double?>), typeof(TK_NumberTextBox));

        /// <summary>
        /// 值改变事件处理
        /// </summary>
        public event RoutedPropertyChangedEventHandler<double?> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        #endregion 事件

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
            DependencyProperty.Register("Corner", typeof(CornerRadius), typeof(TK_NumberTextBox), new PropertyMetadata());

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
            DependencyProperty.Register("FirstContent", typeof(object), typeof(TK_NumberTextBox), new PropertyMetadata());

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
            DependencyProperty.Register("LastContent", typeof(object), typeof(TK_NumberTextBox), new PropertyMetadata());



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
            DependencyProperty.Register("MouseOverBorBrush", typeof(Brush), typeof(TK_NumberTextBox), new PropertyMetadata());

        /// <summary>
        /// 显示文本
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
           "Text", typeof(string), typeof(TK_NumberTextBox), new PropertyMetadata(null, OnTextChanged));

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// 值
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
           "Value", typeof(double?), typeof(TK_NumberTextBox), new FrameworkPropertyMetadata(default(double?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        /// <summary>
        /// 值
        /// </summary>
        public double? Value
        {
            get { return (double?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
           "MinValue", typeof(double), typeof(TK_NumberTextBox), new PropertyMetadata(double.MinValue));

        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
           "MaxValue", typeof(double), typeof(TK_NumberTextBox), new PropertyMetadata(double.MaxValue));

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        /// <summary>
        /// 增减间隔
        /// </summary>
        public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register(
          "Interval", typeof(double), typeof(TK_NumberTextBox), new PropertyMetadata(default(double)));

        /// <summary>
        /// 增减间隔
        /// </summary>
        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }
        ///// <summary>
        ///// 数值输入正则匹配
        ///// </summary>
        //public static readonly DependencyProperty NumericPatternProperty = DependencyProperty.Register(
        // "NumericPattern", typeof(string), typeof(TK_NumberTextBox), new PropertyMetadata(default(string)));

        ///// <summary>
        ///// 数值输入正则匹配
        ///// </summary>
        //public string NumericPattern
        //{
        //    get { return (string)GetValue(NumericPatternProperty); }
        //    set { SetValue(NumericPatternProperty, value); }
        //}
        #endregion 属性

        #region 实现

        private static double GetCalculatedValue(TK_NumberTextBox numberBox, double value)
        {
            if (value > numberBox.MaxValue)
            {
                return numberBox.MaxValue;
            }
            else if (value < numberBox.MinValue)
            {
                return numberBox.MinValue;
            }
            else
            {
                return value;
            }
        }

        private void GetIntervalAndMin(out double interval, out double min,out int bit)
        {
            interval = Interval ;
            min = MinValue == double.MinValue ? 0 : MinValue;
            string[] bits = interval.ToString().Split('.');
            if(bits.Length==2)
            {
                bit = bits[1].Length;
            }
            else
            {
                bit = 0;
            }
        }
        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            GetIntervalAndMin(out double interval, out double min,out int bit);
            Value =Math.Round(GetCalculatedValue(this, Value == null ? min + interval : Value.GetValueOrDefault() + interval),bit);
            //textBox.Focus();
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            GetIntervalAndMin(out double interval, out double min,out int bit);
            Value =Math.Round(GetCalculatedValue(this, Value == null ? min : Value.GetValueOrDefault()- interval),bit);
            //textBox.Focus();
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var numberBox = d as TK_NumberTextBox;

            if (double.TryParse(numberBox.Text, out double value))
            {
                var newValue = GetCalculatedValue(numberBox, value);
                if (numberBox.Value != newValue)
                {
                    if (numberBox.IsLoaded)
                    {
                        numberBox.Value = newValue;
                    }
                    else
                    {
                        numberBox.Loaded += NumberBox_Loaded;
                    }
                }
            }
            //else if (string.IsNullOrEmpty(numberBox.Text))
            //{
            //    if (InputBoxHelper.GetIsClearable(numberBox))
            //    {
            //        numberBox.Value = null;
            //    }
            //    else
            //    {
            //        numberBox.Text = numberBox.Value.Value.ToString(numberBox.TextFormat);
            //    }
            //}

            if (!numberBox.Value.HasValue)
            {
                numberBox.Text = null;
            }
            else
            {
                numberBox.Text = numberBox.Value.Value.ToString();
            }
        }

        private static void NumberBox_Loaded(object sender, RoutedEventArgs e)
        {
            var numberBox = sender as TK_NumberTextBox;
            if (double.TryParse(numberBox.Text, out double value))
            {
                var newValue = GetCalculatedValue(numberBox, value);
                if (numberBox.Value != newValue)
                {
                    numberBox.Value = newValue;
                }
            }

            numberBox.Loaded -= NumberBox_Loaded;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var numberBox = d as TK_NumberTextBox;
            numberBox.Text = numberBox.Value?.ToString();
            numberBox.textBox?.Select(numberBox.textBox.Text.Length, 1);

            var args = new RoutedPropertyChangedEventArgs<double?>((double?)e.OldValue, (double?)e.NewValue);
            args.RoutedEvent = TK_NumberTextBox.ValueChangedEvent;
            numberBox.RaiseEvent(args);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 限制与数值无关输入
            string pattern = DefaultDoublePattern;
            var regex = new Regex(pattern);
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // 限制空格输入
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TextBox_CanExecutePaste(object sender, CanExecuteRoutedEventArgs e)
        {
            // 限制粘贴
            e.CanExecute = false;
            e.Handled = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (double.TryParse(textBox.Text, out double value))
            {
                if (value < MinValue || value > MaxValue)
                {
                    var newValue = GetCalculatedValue(this, value);
                    if (Value != newValue)
                    {
                        Value = newValue;
                    }
                    else
                    {
                        textBox.Text = newValue.ToString();
                        textBox.Select(textBox.Text.Length, 0);
                    }
                }
            }
        }

        #endregion 实现
    }
}
