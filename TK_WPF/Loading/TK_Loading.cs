using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TK_WPF
{
    public class TK_Loading
    {
        private static async Task<bool> Loading(string identifier, LoadType type, Func<Task> action)
        {
            TaskCompletionSource<bool> result = new TaskCompletionSource<bool>();
            if (!TK_Message.Containers.ContainsKey(identifier))
            {
                result.SetResult(false);
                return await result.Task;
            }
            TK_Container container;
            if (TK_Message.Containers.TryGetValue(identifier, out container)&&!container.IsBusy)
            {
                container.IsBusy = true;
                container.Dispatcher.VerifyAccess();
                TK_LoadingCard card = new TK_LoadingCard()
                {
                    LoadType = type,
                    IsEnabled = true,
                    Width = 150
                };
                container.AddCenter(card);

                card.ShowLoading = true;
                if (action != null)
                {
                    await Task.Run(async () =>
                    {
                        await action();
                        await Task.Delay(1000);
                        return true;
                    });
                    await Task.Delay(100);
                    result.TrySetResult(true);
                }
                else
                {
                    result.TrySetResult(false);
                }
                await result.Task;
                container.IsBusy = false;
                card.ShowLoading = false;
                return true;
            }
            else
            {
                result.SetResult(false);
                return await result.Task;
            }
        }

        public async static Task<bool> Card(string identifier, Func<Task> action)
        {
            return await Loading(identifier, LoadType.Card, action);
        }

        public async static Task<bool> Rotary(string identifier, Func<Task> action)
        {
            return await Loading(identifier, LoadType.Rotary, action);
        }

        public async static Task<bool> Marquee(string identifier, Func<Task> action)
        {
            return await Loading(identifier, LoadType.Marquee, action);
        }

        public async static Task<bool> Wave(string identifier, Func<Task> action)
        {
            return await Loading(identifier, LoadType.Wave, action);
        }
    }
}
