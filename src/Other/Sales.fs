namespace OpenSilver.Samples.Showcase.Other

open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.Linq
open System.Text
open System.Threading.Tasks
open System.Windows.Controls.DataVisualization.Charting

type SalesData(mon: string, sales: int) =
    member val Sales = sales with get, set
    member val Month = mon with get, set

type ProductionCostData(name: string, cost: int) =
    member val Cost = cost with get, set
    member val Name = name with get, set

type internal Sales() =
    static member Tables = Sales.GetListOfTables()
    static member private GetListOfTables() =
        ObservableCollection<SalesData>(
            [
                new SalesData("Jan", 9)
                new SalesData("Feb", 2)
                new SalesData("Mar", 7)
                new SalesData("Apr", 18)
            ])

    static member Chairs = Sales.GetListOfChairs()
    static member private GetListOfChairs() =
        ObservableCollection<SalesData>(
            [
                new SalesData("Jan", 3)
                new SalesData("Feb", 6)
                new SalesData("Mar", 4)
                new SalesData("Apr", 15)
            ])

    static member ProductionCosts = Sales.GetProductionCosts()
    static member private GetProductionCosts() =
        ObservableCollection<ProductionCostData>(
            [
                new ProductionCostData("Raw materials", 80)
                new ProductionCostData("Labor", 50)
                new ProductionCostData("Overhead", 30)
                new ProductionCostData("Marketing and Advertising", 20)
                new ProductionCostData("R&D", 10)
                new ProductionCostData("Quality Control", 6)
                new ProductionCostData("Miscellaneous", 4)
            ])
