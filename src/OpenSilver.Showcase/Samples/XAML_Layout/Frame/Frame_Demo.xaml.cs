using OpenSilver.Showcase.Search;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenSilver.Showcase;

[SearchKeywords("navigation", "navigationservice", "page", "content", "view", "container", "query", "urimapping", "urimapper", "url", "back", "forward")]
public partial class Frame_Demo : UserControl
{
    Page _page;

    public Frame_Demo()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _page = GetParentPage(this);

        UpdateButtonsState();
    }

    private void UpdateButtonsState()
    {
        ButtonBackwards.IsEnabled = _page.NavigationService.CanGoBack;
        ButtonForward.IsEnabled = _page.NavigationService.CanGoForward;

        // Note: if you wish to update the state of the buttons
        // every time that the user navigates to a page, 
        // you can register then Frame.Navigated event and
        // call the code above in the handler.
    }

    private void ButtonBackwards_Click(object sender, RoutedEventArgs e)
    {
        if (_page.NavigationService.CanGoBack)
        {
            _page.NavigationService.GoBack();
        }
    }

    private void ButtonForward_Click(object sender, RoutedEventArgs e)
    {
        if (_page.NavigationService.CanGoForward)
        {
            _page.NavigationService.GoForward();
        }
    }

    private static Page GetParentPage(DependencyObject child)
    {
        DependencyObject parent = child;
        while (parent is not null and not Page)
        {
            parent = VisualTreeHelper.GetParent(parent);
        }
        return parent as Page;
    }

}
