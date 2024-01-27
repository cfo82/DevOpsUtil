namespace DevOpsUtil.BuildStatus.Core.AzureDevOps;

using System;
using DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

/*public class Build
{
    private readonly BuildDefinition _definition;
    private readonly BuildData _data;

    public Build(BuildDefinition definition, BuildData data)
    {
        _definition = definition;
        _data = data;
    }

    public int Id => _data.Id;

    public int Age
    {
        get
        {
            if (_data.Status.Equals("notStarted", StringComparison.InvariantCultureIgnoreCase))
            {
                return 0;
            }

            if (_data.StartTime.Year == 1 && _data.StartTime.Month == 1 && _data.StartTime.Day == 1)
            {
                return 0;
            }

            return (int)(DateTime.Now - _data.StartTime).TotalDays;
        }
    }

    public bool HasFailed =>
        !WasSuccessful &&
        !IsRunning &&
        !_definition.IsDisabled &&
        !_definition.IsIgnored;

    public bool IsRunning =>
        !_definition.IsDisabled &&
        !_definition.IsIgnored &&
        Result.Equals("running", StringComparison.InvariantCultureIgnoreCase);

    public bool WasSuccessful =>
        !_definition.IsDisabled &&
        !_definition.IsIgnored &&
        Result.Equals("succeeded", StringComparison.InvariantCultureIgnoreCase);

    public string Result
    {
        get
        {
            if (_definition.IsDisabled)
            {
                return "disabled";
            }

            if (_definition.IsDraft)
            {
                return "draft";
            }

            if (_definition.IsIgnored)
            {
                return "ignored";
            }

            if (string.IsNullOrEmpty(_data.Result))
            {
                return "running";
            }

            return _data.Result;
        }
    }
}*/
