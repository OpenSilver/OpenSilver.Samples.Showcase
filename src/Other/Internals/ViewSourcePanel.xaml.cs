using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

public partial class ViewSourcePanel : Grid
{
    private IEnumerable<ViewSourceButtonInfo> _sources;

    public ViewSourcePanel()
    {
        InitializeComponent();
    }

    public void ViewSource(IEnumerable<ViewSourceButtonInfo> sources)
    {
        _sources = sources;
        cSharpButton.IsChecked = true;
    }

    private void UpdateTabs(IEnumerable<ViewSourceButtonInfo> sources)
    {
        var selectedIndex = TabControl.SelectedIndex;
        TabControl.Items.Clear();

        foreach (var viewSourceButtonInfo in sources)
        {
            TabControl.Items.Add(new TabItem()
            {
                Header = viewSourceButtonInfo.GetHeader(),
                Content = new ControlToDisplayCodeHostedOnGitHub(viewSourceButtonInfo.GetAbsoluteUrl()),
                DataContext = viewSourceButtonInfo
            });
        }

        if (selectedIndex >= 0 && TabControl.Items.Count > selectedIndex)
        {
            TabControl.SelectedIndex = selectedIndex;
        }
    }

    private IEnumerable<ViewSourceButtonInfo> GetCSharpSources() => GetSources(".vb", ".fs");
    private IEnumerable<ViewSourceButtonInfo> GetVBNETSources() => GetSources(".cs", ".fs");
    private IEnumerable<ViewSourceButtonInfo> GetFSharpSources() => GetSources(".cs", ".vb");

    private IEnumerable<ViewSourceButtonInfo> GetSources(params string[] extensionsToIgnore)
        => _sources.Where(x => string.IsNullOrEmpty(x.FileName) || !extensionsToIgnore.Contains(Path.GetExtension(x.FileName)?.ToLower()));

    private void OnCSharpRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        UpdateTabs(GetCSharpSources());
    }

    private void OnVBNETRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        UpdateTabs(GetVBNETSources());
    }

    private void OnFSharpRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        UpdateTabs(GetFSharpSources());
    }
}
