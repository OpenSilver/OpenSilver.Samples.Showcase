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

        public string Commit { get; set; } = "afa6e6df079c709a1f91b4cf5861e451621f04a7";

        public string Repository { get; set; } = "OpenSilver.Samples.Showcase";

        public string Owner { get; set; } = "OpenSilver";

        public string TabHeader { get; set; }

        public string Fragment { get; set; }

        public string Notes { get; set; }

        public string GetHeader() => !string.IsNullOrEmpty(TabHeader) ? TabHeader : FileName;

        public string GetAbsoluteUrl() => $"https://github.com/{Owner}/{Repository}/blob/{Commit}/{RelativePath}/{FileName}{Fragment}";
    }
}
