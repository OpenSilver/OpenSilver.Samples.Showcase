using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase
{
    public class ViewSourceButtonInfo
    {
        public ViewSourceButtonInfo() { }

        public ViewSourceButtonInfo(string relativePath, string fileName)
        {
            RelativePath = relativePath;
            FileName = fileName;
        }

        public string FileName { get; set; }

        public string RelativePath { get; set; }

        public string Branch { get; set; } = "develop";

        public string Repository { get; set; } = "OpenSilver.Samples.Showcase";

        public string Owner { get; set; } = "OpenSilver";

        public string TabHeader { get; set; }

        public string GetHeader() => !string.IsNullOrEmpty(TabHeader) ? TabHeader : FileName;

        public string GetAbsoluteUrl() => $"https://github.com/{Owner}/{Repository}/blob/{Branch}/{RelativePath}/{FileName}";
    }
}
