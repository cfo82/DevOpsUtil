namespace DevOpsUtil.BuildStatus.Core.AzureDevOps;

using System;
using System.Linq;
using DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

public class Release
{
    private readonly ReleaseData _releaseData;

    public Release(ReleaseData releaseData)
    {
        _releaseData = releaseData;
    }

    public int Id => _releaseData.Id;

    public int Age => (int)(DateTime.Now - _releaseData.CreatedOn).TotalDays;

    public bool WasSuccessful =>
        _releaseData.Environments.All(x =>
            x.Status.Equals("succeeded", StringComparison.InvariantCultureIgnoreCase) ||
            x.Status.Equals("notStarted", StringComparison.InvariantCultureIgnoreCase));

    public bool HasFailed =>
        // basically failed is everything that is not WasSuccessful and not IsRunning which would
        // mean expected states would be 'rejected' or 'partiallySucceeded' in case it needs to be coded
        // explicitly once.
        !WasSuccessful && !IsRunning;

    public bool IsRunning =>
        _releaseData.Environments.Any(x =>
            x.Status.Equals("inProgress", StringComparison.InvariantCultureIgnoreCase));

    public string Result => string.Join(", ", _releaseData.Environments.Select(x => x.Status));
}
