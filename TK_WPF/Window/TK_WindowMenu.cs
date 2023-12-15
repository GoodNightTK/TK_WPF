using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TK_WPF
{
    public class TK_WindowMenu
    {
        public IconCode IconCode { get; set; }
        public string Header { get; set; }
        public object Content { get; set; }
        public List<TK_WindowMenu> Children { get; set; } = new List<TK_WindowMenu>();
        public string IconBase { get; set; }

        public TK_WindowMenu(string header, IconCode iconCode=IconCode.Node,string iconBase="", List<TK_WindowMenu> children=null)
        {
            IconCode = iconCode;
            Header = header;
            Children = children;
            IconBase = iconBase;
        }
        public TK_WindowMenu(string header, object content, IconCode iconCode = IconCode.Node,string iconBase="")
        {
            IconCode = iconCode;
            Header = header;
            Content = content;
            IconBase = iconBase;
        }
    }
}
