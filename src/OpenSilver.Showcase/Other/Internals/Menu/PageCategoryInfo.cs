using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OpenSilver.Showcase
{
    public class PageCategoryInfo : IMenuElement
    {
        public string Name { get; set; }

        public Brush Foreground { get; set; }

        public ObservableCollection<PageInfo> Pages { get; set; } = new ObservableCollection<PageInfo>();
    }
}
