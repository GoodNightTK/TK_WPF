using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TK_WPF
{
    public class TK_Dialog
    {
        /// <summary>
        /// 显示指定对话框
        /// </summary>
        /// <param name="identifier">DialogContainer 标识</param>
        /// <param name="content">内容</param>
        /// <param name="parameters">参数</param>
        /// <param name="title">标题</param>
        /// <param name="openHandler">打开前处理程序</param>
        /// <param name="closeHandle">关闭后处理程序</param>
        /// <param name="showCloseButton">是否显示默认关闭按钮</param>
        /// <returns>结果</returns>
        public static async Task<object> Show(string identifier, object content, double cornerRadius = 5, string title = null, object parameters = null, Action<TK_DialogCard> openHandler = null, Action<TK_DialogCard, object> closeHandle = null, bool showCloseButton = true)
        {
            if (TK_Message.Containers.TryGetValue(identifier, out TK_Container dialog) && !dialog.IsBusy)
            {
                dialog.Dispatcher.VerifyAccess();
                var taskCompletionSource = new TaskCompletionSource<object>();
                TK_DialogCard card = new TK_DialogCard();
                card.CornerRadius = new CornerRadius(cornerRadius);
                if (content is FrameworkElement element && element.DataContext is IDialogViewModel dialogContext)
                {
                    card.Title = string.IsNullOrEmpty(dialogContext.Title) ? title : dialogContext.Title;
                    dialogContext.RequestClose += (param) =>
                    {
                        dialog.RemoveCenter(card);
                        dialog.IsBusy = false;
                        closeHandle?.Invoke(card, param);
                        taskCompletionSource.TrySetResult(param);
                    };
                    card.CloseHandler = (param) =>
                    {
                        dialog.RemoveCenter(card);
                        dialog.IsBusy = false;
                        closeHandle?.Invoke(card, param);
                        taskCompletionSource.TrySetResult(param);
                    };
                }
                else
                {
                    taskCompletionSource.TrySetResult(null);
                    return await taskCompletionSource.Task;
                }

                card.DialogContent = content;
                card.ShowCloseButton = showCloseButton;
                dialog.AddCenter(card);
                openHandler?.Invoke(card);
                dialog.IsBusy = true;
                dialogContext.OnDialogOpened(parameters);

                return await taskCompletionSource.Task;
            }

            return default;
        }
    }
}
