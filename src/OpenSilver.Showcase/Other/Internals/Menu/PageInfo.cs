using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OpenSilver.Showcase
{
    public class PageInfo : IMenuElement
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public Brush IconBrush { get; set; }
        public bool IsVisibleInMenu { get; set; } = true;
    }
}
