namespace DevOpsUtil.Core.Services;

using System;
using DevOpsUtil.Core.Contracts;
using Microsoft.Extensions.Logging;

public class ErrorHandler : IErrorHandler
{
    private readonly ILogger<ErrorHandler> _logger;
    private Exception? _error;

    public ErrorHandler(ILogger<ErrorHandler> logger)
    {
        _logger = logger;
    }

    public event EventHandler? ErrorChanged;

    public Exception? Error
    {
        get
        {
            return _error;
        }

        set
        {
            if (_error != value)
            {
                _error = value;

                if (_error != null)
                {
                    _logger.LogError(_error, "Error occured");
                }

                ErrorChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}