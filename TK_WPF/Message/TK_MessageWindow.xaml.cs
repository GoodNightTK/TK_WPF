using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TK_WPF.Message
{
    /// <summary>
    /// TK_MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TK_MessageWindow : Window
    {
        private static TK_MessageWindow messageWindow = null;

        private TK_MessageWindow()
        {
            InitializeComponent();
        }

        public static TK_MessageWindow GetInstance()
        {
            if (messageWindow == null)
            {
                messageWindow = new TK_MessageWindow();
            }
            else if (!messageWindow.IsLoaded)
            {
                messageWindow = new TK_MessageWindow();
            }
            return messageWindow;
        }

        public void AddMessageCard(TK_MessageCard messageCard, int millisecondTimeOut)
        {

            messageCard.Close += (s, e) =>
            {
                messageStackPanel.Children.Remove(messageCard);
                if (messageStackPanel.Children.Count == 0)
                {
                    this.Close();
                }
            };
            messageStackPanel.Children.Add(messageCard);
            messageCard.ShowMessage = true;
            // 进入动画完成
            if (millisecondTimeOut > 0)
            {
                     Task.Run( async() =>
                    {
                        await Task.Delay(millisecondTimeOut);
                        Dispatcher.Invoke( () =>
                        {
                            if (messageCard.ShowMessage)
                            {
                                messageCard.ShowMessage = false;
                            }
                        });
                    });
            }
        }
    }
}
