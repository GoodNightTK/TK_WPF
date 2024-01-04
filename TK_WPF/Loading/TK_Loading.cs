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
        private static async Task<T2> Loading<T1, T2>(string identifier, string message, LoadType type, Func<T1, T2> action, T1 value)
        {
            TaskCompletionSource<T2> source = new TaskCompletionSource<T2>();
            if (!TK_Message.Containers.ContainsKey(identifier))
            {
                return default;
            }

            TK_Container container;
            if (TK_Message.Containers.TryGetValue(identifier, out container) && !container.IsBusy)
            {
                T2 result = default;
                container.IsBusy = true;
                container.Dispatcher.VerifyAccess();
                TK_LoadingBase card = new TK_LoadingBase()
                {
                    LoadType = type,
                    IsEnabled = true,
                    Content = message,
                    Height=container.centerContent.MaxHeight*1/6
                };
                card.Close += (s, e) =>
                {
                    container.IsBusy = false;
                    source.TrySetResult(result);
                };
                container.AddCenter(card);
                card.ShowLoading = true;
                if (action != null)
                {
                    result = await Task.Run(async () =>
                     {
                         var res = action(value);
                         await Task.Delay(1000);
                         return res;
                     });
                    await Task.Delay(100);
                }
                card.ShowLoading = false;
                return await source.Task;
            }
            else
            {
                return default;
            }
        }

        private static async Task<T2> Loading<T1, T2>(string identifier, string message, LoadType type, Func<T1, Task<T2>> action, T1 value)
        {
            TaskCompletionSource<T2> source = new TaskCompletionSource<T2>();
            if (!TK_Message.Containers.ContainsKey(identifier))
            {
                return default;
            }
            TK_Container container;
            if (TK_Message.Containers.TryGetValue(identifier, out container) && !container.IsBusy)
            {
                T2 result = default;
                container.IsBusy = true;
                container.Dispatcher.VerifyAccess();
                TK_LoadingBase card = new TK_LoadingBase()
                {
                    LoadType = type,
                    IsEnabled = true,
                    Content = message,
                    Height = container.centerContent.MaxHeight * 1 / 6
                };
                card.Close += (s, e) =>
                {
                    container.IsBusy = false;
                    source.TrySetResult(result);
                };
                container.AddCenter(card);

                card.ShowLoading = true;
                if (action != null)
                {
                    result = await Task.Run(async () =>
                    {
                        var res = await action(value);
                        await Task.Delay(1000);
                        return res;
                    });
                    await Task.Delay(100);
                }
                card.ShowLoading = false;
                return await source.Task;
            }
            else
            {
                return default;
            }
        }

        public async static Task<T2> Card<T1, T2>(string identifier,string message, Func<T1, T2> action, T1 value)
        {
            return await Loading(identifier,message, LoadType.Card, action, value);
        }

        public async static Task<T2> Card<T1, T2>(string identifier, string message, Func<T1, Task<T2>> action, T1 value)
        {
            return await Loading(identifier, message, LoadType.Card, action, value);
        }

        public async static Task<T2> Rotary<T1, T2>(string identifier, string message, Func<T1, T2> action, T1 value)
        {
            return await Loading(identifier, message, LoadType.Rotary, action, value);
        }

        public async static Task<T2> Rotary<T1, T2>(string identifier, string message, Func<T1, Task<T2>> action, T1 value)
        {
            return await Loading(identifier, message, LoadType.Rotary, action, value);
        }

        public async static Task<T2> Marquee<T1, T2>(string identifier, string message, Func<T1, T2> action, T1 value)
        {
            return await Loading(identifier, message, LoadType.Marquee, action, value);
        }

        public async static Task<T2> Marquee<T1, T2>(string identifier, string message, Func<T1, Task<T2>> action, T1 value)
        {
            return await Loading(identifier, message, LoadType.Marquee, action, value);
        }

        public async static Task<T2> Wave<T1, T2>(string identifier, string message, Func<T1, T2> action, T1 value)
        {
            return await Loading(identifier, message, LoadType.Wave, action, value);
        }

        public async static Task<T2> Wave<T1, T2>(string identifier, string message, Func<T1, Task<T2>> action, T1 value)
        {
            return await Loading(identifier, message, LoadType.Wave, action, value);
        }
    }
}
