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

        public string Commit { get; set; } = "3190b7276b80ca26f8c90699b4d333e599538bb4";

        public string Repository { get; set; } = "OpenSilver.Samples.Showcase";

        public string Owner { get; set; } = "OpenSilver";

        public string TabHeader { get; set; }

        public string Fragment { get; set; }

        public string Notes { get; set; }

        public string GetHeader() => !string.IsNullOrEmpty(TabHeader) ? TabHeader : FileName;

        public string GetAbsoluteUrl() => $"https://github.com/{Owner}/{Repository}/blob/{Commit}/{RelativePath}/{FileName}{Fragment}";
    }
}
