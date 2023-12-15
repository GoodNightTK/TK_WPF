using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using TK_WPF.Message;

namespace TK_WPF
{
    public class TK_Notification
    {
        #region 全局
        private static void Show(NotificationType type, string element,string titel, int millisecondTimeOut = 3000)
        {

            TK_NotificationWindow messageWindow = TK_NotificationWindow.GetInstance();
            messageWindow.Dispatcher.VerifyAccess();
            TK_NotificationCard messageCard = new TK_NotificationCard()
            {
                NotificationType = type,
                Message = element,
                Titel= titel
            };
            messageWindow.AddMessageCard(messageCard, millisecondTimeOut);
            messageWindow.Show();
        }
        public static void Info(string message, int millisecondTimeOut = 3000)
        {
            Show(NotificationType.Info, message, "Info", millisecondTimeOut);
        }

        public static void Success(string message, int millisecondTimeOut = 3000)
        {
            Show(NotificationType.Success, message,"Success", millisecondTimeOut);
        }

        public static void Warning(string message, int millisecondTimeOut = 3000)
        {
            Show(NotificationType.Warning, message,"Warning", millisecondTimeOut);
        }

        public static void Error(string message, int millisecondTimeOut = 3000)
        {
            Show(NotificationType.Error, message, "Error",  millisecondTimeOut);
        }
        #endregion

        #region 指定容器
        private static void Show(string containerIdentifier, NotificationType type, string element,string titel, int millisecondTimeOut = 3000)
        {
            TK_Container messagePanel;
            if (!TK_Message.Containers.TryGetValue(containerIdentifier,out messagePanel))
            {
                return;
            }
            messagePanel.Dispatcher.VerifyAccess();

            CancellationTokenSource token = new CancellationTokenSource();
            TK_NotificationCard messageCard = new TK_NotificationCard()
            {
                Message = element,
                NotificationType = type,
                Titel= titel
            };
            messageCard.Close += (s, e) =>
            {
                messagePanel.RemoveNotificationCard(messageCard);
            };
            messagePanel.AddNotificationCard(messageCard);
            messageCard.ShowNotification = true;
        
            if (millisecondTimeOut > 0)
            {
                     Task.Run(async () =>
                    {
                        await Task.Delay(millisecondTimeOut);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (messageCard.ShowNotification)
                            {
                                messageCard.ShowNotification = false;
                            }
                        });
                    });
                };
        }


        public static void Info(string containerIdentifier, string message, int millisecondTimeOut = 3000)
        {
            Show(containerIdentifier, NotificationType.Info, message,"Info", millisecondTimeOut);
        }

        public static void Success(string containerIdentifier, string message, int millisecondTimeOut = 3000)
        {
            Show(containerIdentifier, NotificationType.Success, message,"Success", millisecondTimeOut);
        }

        public static void Warning(string containerIdentifier, string message, int millisecondTimeOut = 3000)
        {
            Show(containerIdentifier, NotificationType.Warning, message,"Warning", millisecondTimeOut);
        }

        public static void Error(string containerIdentifier, string message, int millisecondTimeOut = 3000)
        {
            Show(containerIdentifier, NotificationType.Error, message,"Error", millisecondTimeOut);
        }
        #endregion
    }
}
