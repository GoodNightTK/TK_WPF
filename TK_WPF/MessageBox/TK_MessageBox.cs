using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace TK_WPF
{
    public class TK_MessageBox
    {
        /// <summary>
        /// 指定
        /// </summary>
        /// <param name="Identifier"></param>
        /// <param name="message"></param>
        /// <param name="button"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        private static async Task<MessageBoxResult> ShowDialog(string Identifier, string message, MessageBoxButton button, MessageBoxType icon)
        {
            TaskCompletionSource<MessageBoxResult> result = new TaskCompletionSource<MessageBoxResult>();
            if (!TK_Message.Containers.ContainsKey(Identifier))
            {
                result.SetResult(MessageBoxResult.None);
                return await result.Task;
            }
            TK_Container container = TK_Message.Containers[Identifier];
            double height = container.centerContent.MaxHeight;
            double width= container.centerContent.MaxWidth;
            if(container.IsBusy)
            {
                result.TrySetResult(MessageBoxResult.None);
                return await result.Task;
            }
            container.IsBusy = true;
            container.Dispatcher.VerifyAccess();
            TK_MessageBoxCard card = new TK_MessageBoxCard()
            {
                Message = message,
                MessageBoxType = icon,
                MessageBoxButton = button,
                Height = height * 1/3,
                Width = width * 1/3
            };
            card.Close += (s, e) =>
            {
                container.IsBusy = false;
                result.TrySetResult(card.Result);
            };
            container.AddCenter(card);

            card.ShowMessageBox = true;
            return await result.Task;
        }

        public async static Task<MessageBoxResult> Info(string Identifier, string message, MessageBoxButton button = MessageBoxButton.OKCancel)
        {
            return await ShowDialog(Identifier, message, button, MessageBoxType.Info);
        }


        public static async Task<MessageBoxResult> Question(string Identifier, string message, MessageBoxButton button = MessageBoxButton.OKCancel)
        {
            return await ShowDialog(Identifier, message, button, MessageBoxType.Question);
        }


        public static async Task<MessageBoxResult> Success(string Identifier, string message, MessageBoxButton button = MessageBoxButton.OKCancel)
        {
            return await ShowDialog(Identifier, message, button, MessageBoxType.Success);
        }


        public static async Task<MessageBoxResult> Error(string Identifier, string message, MessageBoxButton button = MessageBoxButton.OKCancel)
        {
            return await ShowDialog(Identifier, message, button, MessageBoxType.Error);
        }


        public static async Task<MessageBoxResult> Warning(string Identifier, string message, MessageBoxButton button = MessageBoxButton.OKCancel)
        {
            return await ShowDialog(Identifier, message, button, MessageBoxType.Warning);
        }
    }
}
