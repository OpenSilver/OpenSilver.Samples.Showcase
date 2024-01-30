namespace OpenSilver.Samples.Showcase

open System.Windows

type App = class
    inherit AppXaml
    
    new () as this = {} then
        this.InitializeComponent()

        let mainPage = new MainPage()
        Window.Current.Content <- mainPage;
end
