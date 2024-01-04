﻿using System;
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
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Loading"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TK_WPF.Loading;assembly=TK_WPF.Loading"
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
    ///     <MyNamespace:TK_LoadingBase/>
    ///
    /// </summary>
    public class TK_LoadingBase : ContentControl
    {
        public static readonly RoutedEvent CloseEvent;
        static TK_LoadingBase()
        {
            CloseEvent = EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TK_LoadingBase));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TK_LoadingBase), new FrameworkPropertyMetadata(typeof(TK_LoadingBase)));
        }

        #region 事件
        // 关闭消息事件
        public event RoutedEventHandler Close
        {
            add { base.AddHandler(TK_LoadingBase.CloseEvent, value); }
            remove { base.RemoveHandler(TK_LoadingBase.CloseEvent, value); }
        }
        #endregion

        #region 属性

        /// <summary>
        /// 等待类型
        /// </summary>
        public LoadType LoadType
        {
            get { return (LoadType)GetValue(LoadTypeProperty); }
            set { SetValue(LoadTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadTypeProperty =
            DependencyProperty.Register("LoadType", typeof(LoadType), typeof(TK_LoadingBase), new PropertyMetadata());



        /// <summary>
        /// 展示等待框
        /// </summary>
        public bool ShowLoading
        {
            get { return (bool)GetValue(ShowLoadingProperty); }
            set { SetValue(ShowLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLoadingProperty =
            DependencyProperty.Register("ShowLoading", typeof(bool), typeof(TK_LoadingBase), new PropertyMetadata());


        /// <summary>
        /// 关闭等待框
        /// </summary>
        public bool Closed
        {
            get { return (bool)GetValue(ClosedProperty); }
            set { SetValue(ClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Closed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClosedProperty =
            DependencyProperty.Register("Closed", typeof(bool), typeof(TK_LoadingBase), new PropertyMetadata(false, new PropertyChangedCallback(OnClosedChange)));

        private static void OnClosedChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)(e.NewValue))
            {
                if (d is TK_LoadingBase card)
                {
                    RoutedEventArgs args = new RoutedEventArgs(CloseEvent);
                    card.RaiseEvent(args);
                }
            }
        }
        #endregion
    }
}
