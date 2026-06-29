using Apps.Hubspot.Crm.Polling.Inputs;
using Apps.Hubspot.Crm.Polling.Lists;
using Apps.Hubspot.Crm.Polling.Memory;
using Blackbird.Applications.Sdk.Common.Polling;
using HubspotTests.Base;

namespace Tests.Hubspot;

[TestClass]
public class CompaniesPollingTests : TestBase
{
    [TestMethod]
    public async Task OnCompanyPropertyChanged_IsSuccess()
    {
        // Arrange
        var polling = new Companies(InvocationContext);
        var pollingRequest = new PollingEventRequest<DateTimeMemory>
        {
            Memory = new DateTimeMemory { LastPollingTime = new DateTime(2026, 06, 29, 15, 18, 0, DateTimeKind.Utc) }
        };
        var input = new OnCompanyPropertyChangedRequest { Property = "test_property" };

        // Act
        var result = await polling.OnCompanyPropertyChanged(pollingRequest, input);

        // Assert
        PrintResult(result);
        Assert.IsNotNull(result);
    }
}