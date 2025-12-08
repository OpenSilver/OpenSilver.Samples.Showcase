[![LogoShowcaseLight_small](https://github.com/user-attachments/assets/2e71fcf1-8df9-435f-8686-635e12af03f2)](https://OpenSilverShowcase.com)

# OpenSilver Showcase

This app contains over 200 small samples that demonstrate OpenSilver’s features. Built with C# and XAML (with VB.NET and F# snippets included), it’s also a great way to learn XAML and explore .NET UI concepts. It runs on all modern browsers, and a mobile app is also available for iOS and Android.

## Run live in your web browser:
 👉 [OpenSilverShowcase.com](https://OpenSilverShowcase.com)

## Or download the mobile app:
- **Android** *([Link to Google Play](https://play.google.com/store/apps/details?id=net.opensilver.showcase))*

  [![qr_code_android_app_small](https://github.com/user-attachments/assets/028e8cf1-3f56-4a6a-a762-40aad4c99e66)](https://play.google.com/store/apps/details?id=net.opensilver.showcase)

- **iOS** *([Link to Apple App Store](https://apps.apple.com/app/opensilver-showcase/id6746472943))* 

  [![qr_code_ios_app_small](https://github.com/user-attachments/assets/3172019c-0d7f-4d4a-832c-1ad82e3595c4)](https://apps.apple.com/app/opensilver-showcase/id6746472943)



## Screenshots:

![Screenshot1](https://github.com/user-attachments/assets/06983e67-7fbf-4d7d-b407-7bf3d7b434d5)

![iphone1_small](https://github.com/user-attachments/assets/fb14670e-6264-496e-b6fa-d2106a87208b)



## Source code organization:

The main branches are:
- **develop**: this branch is where day to day development occurs (please send your **pull requests** here)
- **master**: this branch corresponds to the version of the packages that are on Nuget.org

The different solution files are for different purposes and include different projects:
- **OpenSilver.Showcase.sln** contains all projects, including F# and VB versions of the showcase.
- **OpenSilver.Showcase - Minimal(Fast).sln** contains the bare minimum C# projects to run the showcase. The projects of Blazor components are not included and the samples for these components will instead be shown in an iFrame. This is recommended for users who wish to be able to build and run the showcase faster.
- **OpenSilver.Showcase - With Blazor Demos.sln** contains all the C# projects, except the projects for Blazor component that do require a license.
- **OpenSilver.Showcase - C# Full.sln** contains all the C# projects, including those that use third party Blazor components that require a license.

To load the Blazor samples locally, select `Debug-FullBlazor` or `Release-FullBlazor` configuration. Otherwise, the Blazor samples will be shown in an iFrame pointing to the hosted version.

Here are the projects included for each .sln file (names shortened for readability):
| Solution file         | OpenSilver.Showcase | Browser      | Simulator | MauiHybrid | Shared | Blazorise | MudBlazor | Radzen | DevExpress | Syncfusion |
| :---                  |     :---:           |  :---:       |   :---:    |   :---:   | :---:  |  :---:    |  :---:    | :---:  |   :---:    |   :---:    |
| **Showcase.sln**      |    C#✅ F#✅ VB✅    | C#✅ F#✅ VB✅ |   ✅       |     ✅     |   ✅   |    ✅      |    ✅     |   ✅    |     ✅     |     ✅      |
| **Minimal(Fast)**     |    C#✅ F#❌ VB❌    | C#✅ F#❌ VB❌ |   ✅       |     ❌     |   ✅   |    ❌      |    ❌     |   ❌    |     ❌     |     ❌      |
| **With Blazor Demos** |    C#✅ F#❌ VB❌    | C#✅ F#❌ VB❌ |   ✅       |     ❌     |   ✅   |    ✅      |    ✅     |   ✅    |     ❌     |     ❌      |
| **C# Full**           |    C#✅ F#❌ VB❌    | C#✅ F#❌ VB❌ |   ✅       |     ✅     |   ✅   |    ✅      |    ✅     |   ✅    |     ✅     |     ✅      |
