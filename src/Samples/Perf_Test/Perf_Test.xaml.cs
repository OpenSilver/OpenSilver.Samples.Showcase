using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace OpenSilver.Samples.Showcase;

public partial class Perf_Test : Page
{
    private readonly Stopwatch _stopwatch = new();
    private readonly Dictionary<string, long> _elements = [];

    public Perf_Test()
    {
        InitializeComponent();

        controlsCombo.ItemsSource = ControlSearch.controls.OrderBy(x => x.Name);
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        progressBar.Maximum = controlsCombo.Items.Count;
        resultsPanel.Children.Clear();

        for (int i = 0; i < controlsCombo.Items.Count; i++)
        {
            progressBar.Value = i + 1;
            await LoadDemo((controlsCombo.Items[i] as SearchableItem)?.Name);
        }

        var sum = _elements.Sum(x => x.Value);
        var result = string.Join(Environment.NewLine, _elements.OrderByDescending(x => x.Value).Select(x => $"{x.Key}: {x.Value} ms, {(x.Value / (double)sum):P2}"));
        resultsPanel.Children.Clear();
        resultsPanel.Children.Add(new TextBox { Text = $"Total: {controlsCombo.Items.Count} in {sum} ms{Environment.NewLine}{result}", Height = double.NaN });
    }

    private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await LoadDemo((controlsCombo.SelectedItem as SearchableItem)?.Name);
    }

    private async Task LoadDemo(string name)
    {
        container.Child = null;
        await Task.Delay(1);
        var sampleType = SamplesInfoLoader.GetControlTypeByName(name);
        object controlInstance = Activator.CreateInstance(sampleType);
        var tcs = new TaskCompletionSource<object>();

        if (controlInstance is FrameworkElement element)
        {
            element.Loaded += Element_Loaded;
            element.InvokeOnLayoutUpdated(() =>
            {
                var elapsedMs = _stopwatch.ElapsedMilliseconds;
                _elements[element.GetType().Name] = elapsedMs;
                var message = $"Element layout updated: {element.GetType().Name} in {elapsedMs} ms";
                Log(message);
                resultsPanel.Children.Add(new TextBlock { Text = message });
                tcs.SetResult(null);
            });

            _stopwatch.Restart();
            container.Child = element;
            Log($"Added {element.GetType().Name} in {_stopwatch.ElapsedMilliseconds} ms");
        }
        await tcs.Task;
        await Task.Delay(100);
    }

    private void Log(string message)
    {
        result.Text = message;
        Console.WriteLine(message);
        Debug.WriteLine(message);
    }

    private void Element_Loaded(object sender, RoutedEventArgs e)
    {
        Log($"Element_Loaded {sender} in {_stopwatch.ElapsedMilliseconds} ms");
    }
}
