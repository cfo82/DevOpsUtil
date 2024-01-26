namespace DevOpsUtil.Core.Contracts;

using System;

public interface IBrowserService
{
    void OpenUrl(Uri url);
}