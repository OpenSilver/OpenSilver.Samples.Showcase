Namespace OpenSilver.Samples.Showcase.Search
    Public Class ControlSearch
        Private Shared ReadOnly controls As List(Of SearchableItem) = SamplesInfoLoader.GetAllControls()

        Public Shared ReadOnly SearchKeywords As String() = controls _
            .SelectMany(Function(x) x.Keywords.Append(x.Name)) _
            .Select(Function(x) x.ToLower().Replace("_demo", "")) _
            .Distinct() _
            .OrderBy(Function(x) x) _
            .ToArray()

        Public Shared Function Search(query As String) As List(Of SearchableItem)
            query = query.ToLower() ' Case insensitive

            Return controls.
                Where(Function(control) control.Name.ToLower().Contains(query) OrElse
                      control.Keywords.Any(Function(k) k.ToLower().Contains(query))
                ).ToList()
        End Function
    End Class
End Namespace
