namespace OpenSilver.Showcase
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

        public string Commit { get; set; } = "04289e41e247599d036aa0bf25a6169c3504363c";

        public string Repository { get; set; } = "OpenSilver.Samples.Showcase";

        public string Owner { get; set; } = "OpenSilver";

        public string TabHeader { get; set; }

        public string Fragment { get; set; }

        public string Notes { get; set; }

        public string GetHeader() => !string.IsNullOrEmpty(TabHeader) ? TabHeader : FileName;

        public string GetAbsoluteUrl() => $"https://github.com/{Owner}/{Repository}/blob/{Commit}/{RelativePath}/{FileName}{Fragment}";
    }
}
