using System;
using System.Windows.Markup;

namespace OpenSilver.Showcase;

[ContentProperty(nameof(Key))]
public class ResxExtension : MarkupExtension
{
    public string Key { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return SampleResourceFile.ResourceManager.GetString(Key);
    }
}

