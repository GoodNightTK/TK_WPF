using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TK_WPF;

namespace TestDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : TK_MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            menu.ItemsSource = new ObservableCollection<TK_WindowMenu>()
            {
                new TK_WindowMenu("测试",IconCode.CheckBox_UnCheck,"",new List<TK_WindowMenu>()
                {
                    new TK_WindowMenu("测试2",new UserControl2(),IconCode.Card_Question,"")
                })
            };
        }
    }
}
