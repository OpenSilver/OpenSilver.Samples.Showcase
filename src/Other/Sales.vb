Imports System.Collections.ObjectModel

Namespace OpenSilver.Samples.Showcase.Other
    Friend Class Sales
        Public Shared Property Tables As ObservableCollection(Of SalesData) = GetListOfTables()

        Private Shared Function GetListOfTables() As ObservableCollection(Of SalesData)
            Return New ObservableCollection(Of SalesData)() From {
                New SalesData("Jan", 9),
                New SalesData("Feb", 2),
                New SalesData("Mar", 7),
                New SalesData("Apr", 18)
            }
        End Function

        Public Shared Property Chairs As ObservableCollection(Of SalesData) = GetListOfChairs()

        Private Shared Function GetListOfChairs() As ObservableCollection(Of SalesData)
            Return New ObservableCollection(Of SalesData)() From {
                New SalesData("Jan", 3),
                New SalesData("Feb", 6),
                New SalesData("Mar", 4),
                New SalesData("Apr", 15)
            }
        End Function

        Public Shared Property ProductionCosts As ObservableCollection(Of ProductionCostData) = GetProductionCosts()

        Private Shared Function GetProductionCosts() As ObservableCollection(Of ProductionCostData)
            Return New ObservableCollection(Of ProductionCostData)() From {
                New ProductionCostData("Raw materials", 80),
                New ProductionCostData("Labor", 50),
                New ProductionCostData("Overhead", 30),
                New ProductionCostData("Marketing and Advertising", 20),
                New ProductionCostData("R&D", 10),
                New ProductionCostData("Quality Control", 6),
                New ProductionCostData("Miscellaneous", 4)
            }
        End Function
    End Class

    Public Class SalesData
        Public Sub New(ByVal mon As String, ByVal salesValue As Integer)
            Sales = salesValue
            Month = mon
        End Sub

        Public Property Sales As Integer
        Public Property Month As String
    End Class

    Public Class ProductionCostData
        Public Sub New(ByVal nameValue As String, ByVal costValue As Integer)
            Cost = costValue
            Name = nameValue
        End Sub

        Public Property Cost As Integer
        Public Property Name As String
    End Class
End Namespace
