namespace DevOpsUtil.BuildStatus.Core.Common;

using DevOpsUtil.BuildStatus.Core.Interfaces;

public class NullTrafficLightService : ITrafficLightService
{
    private TrafficLightColor _color;

    public TrafficLightColor Color
    {
        get
        {
            return _color;
        }

        set
        {
            _color = value;
            System.Diagnostics.Debug.WriteLine($"Set Traffic Light to {_color}");
        }
    }
}
