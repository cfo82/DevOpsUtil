namespace DevOpsUtil.Core.Contracts;

public interface ILogService
{
    void WriteEntry(string message);
}