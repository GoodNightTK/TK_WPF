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
using System.Windows.Shapes;
using TK_WPF.Message;

namespace TK_WPF
{
    /// <summary>
    /// TK_NotificationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TK_NotificationWindow : Window
    {
        private static TK_NotificationWindow messageWindow = null;
        public TK_NotificationWindow()
        {
            InitializeComponent();
        }

        public static TK_NotificationWindow GetInstance()
        {
            if (messageWindow == null)
            {
                messageWindow = new TK_NotificationWindow();
            }
            else if (!messageWindow.IsLoaded)
            {
                messageWindow = new TK_NotificationWindow();
            }
            return messageWindow;
        }

        public void AddMessageCard(TK_NotificationCard messageCard, int millisecondTimeOut)
        {

            messageStackPanel.Children.Insert(0, messageCard);
            messageCard.Close += (s, e) =>
            {
                messageStackPanel.Children.Remove(messageCard);
                if (messageStackPanel.Children.Count == 0)
                {
                    this.Close();
                }
            };
            messageCard.ShowNotification = true;
            // 进入动画完成
            if (millisecondTimeOut > 0)
            {
                Task.Run(async () =>
               {
                   await Task.Delay(millisecondTimeOut);
                   Dispatcher.Invoke(() =>
                   {
                       if (messageCard.ShowNotification)
                       {
                           messageCard.ShowNotification = false;
                       }
                   });
               });
            };
        }
    }
}
