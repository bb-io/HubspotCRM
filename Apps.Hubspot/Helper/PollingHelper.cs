using Apps.Hubspot.Crm.Polling.Memory;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Apps.Hubspot.Crm.Helper;

public static class PollingHelper
{
    public static PollingEventResponse<DateTimeMemory, T> DontFlyBird<T>(DateTime currentDateTime)
    {
        return new PollingEventResponse<DateTimeMemory, T>
        {
            FlyBird = false,
            Memory = new DateTimeMemory { LastPollingTime = currentDateTime },
            Result = default
        };
    }
}