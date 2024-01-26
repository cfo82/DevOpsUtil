namespace DevOpsUtil.Core.Services;

using System;
using System.Globalization;
using System.IO;
using DevOpsUtil.Core.Contracts;

public class LogService : ILogService
{
    private readonly IErrorHandler _errorHandler;
    private readonly string _localFolderPath;

    public LogService(IErrorHandler errorHandler)
    {
        _errorHandler = errorHandler;
        _localFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BuildStatus");

        _errorHandler.ErrorChanged += ErrorHandlerOnErrorChanged;

        WriteEntry("Starting Log Service");
    }

    public void WriteEntry(string message)
    {
        var date = DateTime.Now;
        var dateString = date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
        var timeString = date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern);

        var lines = new[] { $"{dateString} {timeString}: {message}" };

        if (!Directory.Exists(_localFolderPath))
        {
            Directory.CreateDirectory(_localFolderPath);
        }

        lock (this)
        {
            File.AppendAllLines(Path.Combine(_localFolderPath, "log.txt"), lines);
        }
    }

    private void ErrorHandlerOnErrorChanged(object? sender, EventArgs eventArgs)
    {
        if (_errorHandler.Error != null)
        {
            WriteEntry(_errorHandler.Error.ToString());
        }
    }
}