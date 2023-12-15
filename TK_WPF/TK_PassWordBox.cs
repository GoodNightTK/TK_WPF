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
    ///     <MyNamespace:TK_PassWordBox/>
    ///
    /// </summary>
    public class TK_PassWordBox:TK_TextBox
    {
        static TK_PassWordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_PassWordBox), new FrameworkPropertyMetadata(typeof(TK_PassWordBox)));
        }

        #region 属性
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get { return (string)GetValue(PassWordProperty); }
            set { SetValue(PassWordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PassWord.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.Register("PassWord", typeof(string), typeof(TK_PassWordBox), new PropertyMetadata(string.Empty));


        /// <summary>
        /// 密码注释符
        /// </summary>
        public char PassWordChar
        {
            get { return (char)GetValue(PassWordCharProperty); }
            set { SetValue(PassWordCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PassWordChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordCharProperty =
            DependencyProperty.Register("PassWordChar", typeof(char), typeof(TK_PassWordBox), new PropertyMetadata('●'));

        #endregion


        //重写文本框内容改变事件
        protected override void OnTextChanged(TextChangedEventArgs e)
        {

            base.OnTextChanged(e);
            if(PassWord==null)
            {
                PassWord=string.Empty;
            }
            //已键入的文本长度
            int textLength = Text.Length;
            //已保存的密码长度
            int psdLength = PassWord.Length;
            //起始修改位置
            int startIndex = -1;
            for (int i = 0; i < textLength; i++)
            {

                if (Text[i] != PassWordChar)
                {

                    startIndex = i;
                    break;
                }
            }
            //未作任何修改
            if (startIndex == -1 && textLength == psdLength) return;
            //结束修改位置
            int stopIndex = -1;
            for (int i = textLength - 1; i >= 0; i--)
            {

                if (Text[i] != PassWordChar)
                {

                    stopIndex = i;
                    break;
                }
            }
            //添加或修改了一个或连续的多个值
            if (startIndex != -1)
            {

                //累计修改长度
                int alterLength = stopIndex - startIndex + 1;
                //长度没变化，单纯的修改了一个或连续的多个值
                if (textLength == psdLength)
                {

                    PassWord = PassWord.Substring(0, startIndex) + Text.Substring(startIndex, alterLength) + PassWord.Substring(stopIndex + 1);
                }
                //新增了内容
                else
                {

                    //新增且修改了原来内容
                    if (alterLength > textLength - psdLength)
                    {

                        //计算未修改密码个数 textLength - alterLength
                        //计算已修改密码个数 = 原密码长度 - 未修改密码个数 psdLength - (textLength - alterLength)
                        //原密码该保留的后半部分的索引 = 已修改密码个数 + 起始修改位置
                        PassWord = PassWord.Substring(0, startIndex) + Text.Substring(startIndex, alterLength) + PassWord.Substring(psdLength - (textLength - alterLength) + startIndex);
                    }
                    //单纯的新增了一个或多个连续的值
                    else
                    {

                        PassWord = PassWord.Substring(0, startIndex) + Text.Substring(startIndex, alterLength) + PassWord.Substring(startIndex);
                    }

                }
            }
            //删除了一个或连续的多个值
            else
            {

                //已删除的数据长度
                int length = psdLength - textLength;
                if (SelectionStart < textLength)
                {

                    //改变了中间的数据
                    PassWord = PassWord.Substring(0, SelectionStart) + PassWord.Substring(SelectionStart + length);
                }
                else
                {

                    //删除了结尾的数据
                    PassWord = PassWord.Substring(0, SelectionStart);
                }
            }
            //记住光标位置(设置完Text后会丢失，所以现在要记住)
            int selectionStart = SelectionStart;
            //设置显示密码
            Text = new string(PassWordChar, textLength);
            //设置光标位置
            SelectionStart = selectionStart;
        }
    }
}
