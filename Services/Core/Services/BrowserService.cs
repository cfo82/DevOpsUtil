namespace DevOpsUtil.Core.Services;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DevOpsUtil.Core.Contracts;

public class BrowserService : IBrowserService
{
    public void OpenUrl(Uri url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // https://stackoverflow.com/a/2796367/241446
            using var proc = new Process { StartInfo = { UseShellExecute = true, FileName = url.ToString() } };
            proc.Start();

            return;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("x-www-browser", url.ToString());
            return;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url.ToString());
        }
    }
}