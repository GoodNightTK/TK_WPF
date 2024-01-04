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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestDemo
{
    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        private async void TK_Button_Click(object sender, RoutedEventArgs e)
        {
            //await TK_WPF.TK_Dialog.Show("MainWindow", new UserControl1());
            string mm = await TK_WPF.TK_Loading.Card<string, string>("MainWindow","请稍等。。。",  this.load, "测试");

            MessageBox.Show(mm);
        }

        private async Task<string> load(string arg)
        {
            Thread.Sleep(5000);
            Console.WriteLine(arg);
            return arg;
        }

        private async void TK_Button_Click_1(object sender, RoutedEventArgs e)
        {
            await TK_WPF.TK_Dialog.Show("MainWindow",new UserControl1());
           await TK_WPF.TK_Loading.Wave("MainWindow","加载中......", async (o) =>
           {
               await Task.Delay(5000);
               return string.Empty;
               
           }, "");
        }
    }
}
