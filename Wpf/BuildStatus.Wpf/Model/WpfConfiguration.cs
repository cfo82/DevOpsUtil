namespace DevOpsUtil.BuildStatus.Wpf.Model
{
    using System;
    using System.IO;
    using DevOpsUtil.BuildStatus.Core.Common;

    public class WpfConfiguration : Configuration
    {
        private readonly string _localFolderPath;

        public WpfConfiguration()
        {
            _localFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BuildStatus");
        }

        public override string LocalFolderPath => _localFolderPath;
    }
}
