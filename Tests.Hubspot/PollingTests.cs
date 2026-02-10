using Apps.Hubspot.Crm.Polling.Inputs;
using Apps.Hubspot.Crm.Polling.Lists;
using Apps.Hubspot.Crm.Polling.Memory;
using Blackbird.Applications.Sdk.Common.Polling;
using HubspotTests.Base;
using Newtonsoft.Json;

namespace Tests.Hubspot;

[TestClass]
public class PollingTests : TestBase
{
    [TestMethod]
    public async Task OnDealStatusChanged_ReturnsUpdatedDeals()
    {
		// Arrange
		var polling = new Deals(InvocationContext);
		var memory = new DateTime(2026, 02, 10, 11, 0, 0, DateTimeKind.Utc);
        var pollingMemory = new PollingEventRequest<DateTimeMemory> { Memory = new DateTimeMemory(memory) };
        var input = new OnStatusChangedRequest 
        { 
            Status = "presentationscheduled",
            DealId = "55827471492"
        };

        // Act
        var result = await polling.OnDealStatusChanged(pollingMemory, input);

        // Assert
        Console.WriteLine(JsonConvert.SerializeObject(result.Result, Formatting.Indented));
        Assert.IsNotNull(result.Result);
    }
}
