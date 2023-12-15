using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK_WPF;

namespace TestDemo
{
    internal class ViewModel : IDialogViewModel
    {
        public string Title { get; }

        public event Action<object> RequestClose;

        public void OnDialogOpened(object parameters)
        {
           // throw new NotImplementedException();
        }
    }
}
