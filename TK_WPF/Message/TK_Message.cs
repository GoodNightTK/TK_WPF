using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using TK_WPF.Message;
using System.Threading;
using System.Runtime.CompilerServices;

namespace TK_WPF
{
    public class TK_Message
    {
        public static Dictionary<string, TK_Container> Containers = new Dictionary<string, TK_Container>();
        #region 全局
        private static void Show(MessageType type, string element, int millisecondTimeOut = 3000)
        {

            TK_MessageWindow messageWindow = TK_MessageWindow.GetInstance();
            messageWindow.Dispatcher.VerifyAccess();
            TK_MessageCard messageCard = new TK_MessageCard()
            {
                MessageType = type,
                Message = element
            };
            messageWindow.AddMessageCard(messageCard, millisecondTimeOut);
            messageWindow.Show();
        }
        public static void Info(string message, int millisecondTimeOut = 3000)
        {
            Show(MessageType.Info, message, millisecondTimeOut);
        }

        public static void Success(string message, int millisecondTimeOut = 3000)
        {
            Show(MessageType.Success, message, millisecondTimeOut);
        }

        public static void Warning(string message, int millisecondTimeOut = 3000)
        {
            Show(MessageType.Warning, message, millisecondTimeOut);
        }

        public static void Error(string message, int millisecondTimeOut = 3000)
        {
            Show(MessageType.Error, message, millisecondTimeOut);
        }
        #endregion

        #region 指定容器
        private static void Show(string containerIdentifier, MessageType type, string element, int millisecondTimeOut = 3000)
        {
            TK_Container messagePanel;
            if (!Containers.TryGetValue(containerIdentifier,out messagePanel))
            {
                return;
            }
            messagePanel.Dispatcher.VerifyAccess();

            TK_MessageCard messageCard = new TK_MessageCard()
            {
                Message = element,
                MessageType = type
            };
            messageCard.Close += (s, e) =>
            {
                messagePanel.RemoveMessageCard(messageCard);
            };
            messagePanel.AddMessageCard(messageCard);
            messageCard.ShowMessage = true;
            if (millisecondTimeOut > 0)
            {
                Task.Run(async () =>
               {
                   await Task.Delay(millisecondTimeOut);
                   Application.Current.Dispatcher.Invoke(new Action(() =>
                   {
                       if (messageCard.ShowMessage)
                       {
                           messageCard.ShowMessage = false;
                       }
                   }));
               });
            }
        }

        public static void Info(string containerIdentifier, string message, int millisecondTimeOut = 3000)
        {
            Show(containerIdentifier, MessageType.Info, message, millisecondTimeOut);
        }

        public static void Success(string containerIdentifier, string message, int millisecondTimeOut = 3000)
        {
            Show(containerIdentifier, MessageType.Success, message, millisecondTimeOut);
        }

        public static void Warning(string containerIdentifier, string message, int millisecondTimeOut = 3000)
        {
            Show(containerIdentifier, MessageType.Warning, message, millisecondTimeOut);
        }

        public static void Error(string containerIdentifier, string message, int millisecondTimeOut = 3000)
        {
            Show(containerIdentifier, MessageType.Error, message, millisecondTimeOut);
        }

        internal static void UpdateContainer(TK_Container container, string? identify)
        {
            if (!Containers.ContainsKey(identify))
            {
                Containers.Add(identify, container);
            }
        }
        public static void RemoveContainer(string identify)
        {
            if (Containers.ContainsKey(identify))
            {
                Containers.Remove(identify);
            }
        }
        #endregion
    }
}
