Namespace OpenSilver.Samples.Showcase.Search
    Public Module ControlSearch
        Private ReadOnly controls As List(Of SearchableItem) = SamplesInfoLoader.GetAllControls()

        Public Function Search(query As String) As List(Of SearchableItem)
            query = query.ToLower() ' Case insensitive

            Return controls.
                Where(Function(control) control.Name.ToLower().Contains(query) OrElse
                      control.Keywords.Any(Function(k) k.ToLower().Contains(query))
                ).ToList()
        End Function
    End Module
End Namespace
