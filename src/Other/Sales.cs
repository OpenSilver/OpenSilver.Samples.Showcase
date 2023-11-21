using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;

namespace OpenSilver.Samples.Showcase.Other
{
    internal class Sales
    {
        public static ObservableCollection<SalesData> Tables { get; set; } = GetListOfTables();
        private static ObservableCollection<SalesData> GetListOfTables()
        {
            return new ObservableCollection<SalesData>()
            {
                new SalesData("Jan", 9),
                new SalesData("Feb", 2),
                new SalesData("Mar", 7),
                new SalesData("Apr", 18)
            };
        }

        public static ObservableCollection<SalesData> Chairs { get; set; } = GetListOfChairs();
        private static ObservableCollection<SalesData> GetListOfChairs()
        {
            return new ObservableCollection<SalesData>()
            {
                new SalesData("Jan", 3),
                new SalesData("Feb", 6),
                new SalesData("Mar", 4),
                new SalesData("Apr", 15)
            };
        }

        public static ObservableCollection<ProductionCostData> ProductionCosts { get; set; } = GetProductionCosts();
        private static ObservableCollection<ProductionCostData> GetProductionCosts()
        {
            return new ObservableCollection<ProductionCostData>()
            {
                new ProductionCostData("Raw materials", 80),
                new ProductionCostData("Labor", 50),
                new ProductionCostData("Overhead", 30),
                new ProductionCostData("Marketing and Advertising", 20),
                new ProductionCostData("R&D", 10),
                new ProductionCostData("Quality Control", 6),
                new ProductionCostData("Miscellaneous", 4)

            };
        }
    }

    public class SalesData
    {
        public SalesData(string mon, int sales)
        {
            Sales = sales;
            Month = mon;
        }

        public int Sales { get; set; }
        public string Month { get; set; }
    }

    public class ProductionCostData
    {
        public ProductionCostData(string name, int cost)
        {
            Cost = cost;
            Name = name;
        }

        public int Cost { get; set; }
        public string Name { get; set; }
    }
}
