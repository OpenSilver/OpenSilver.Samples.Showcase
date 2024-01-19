namespace OpenSilver.Samples.Showcase

open MaterialDesign_Styles_Kit
open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Media
#else
open Windows.UI
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Media
#endif

type App = class
    inherit AppXaml
    
    new () as this = {} then
        this.InitializeComponent()

        let mainPage = new MainPage()
        Window.Current.Content <- mainPage;
end
