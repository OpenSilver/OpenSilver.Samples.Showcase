using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("performance", "virtualization", "lazy loading", "memory optimization", "UI")]
    public partial class Virtualization_Demo : UserControl
    {
        public List<Tuple<string, string>> colors = GetColorList();
        public Virtualization_Demo()
        {
            this.InitializeComponent();
            this.DataContext = colors;
        }

        static List<Tuple<string, string>> GetColorList()
        {
            List<Tuple<string, string>> colorList = new List<Tuple<string, string>>();

            // Get all predefined colors
            foreach (PropertyInfo property in typeof(Colors).GetProperties())
            {
                if (property.PropertyType == typeof(Color))
                {
                    Color color = (Color)property.GetValue(null, null);
                    string colorName = property.Name;
                    string colorCode = "#" + color.ToString().Substring(3); // Convert to hex format

                    colorList.Add(Tuple.Create(colorName, colorCode));
                }
            }

            return colorList;
        }


    }
}
