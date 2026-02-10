using Newtonsoft.Json;
using HubspotTests.Base;
using Apps.Hubspot.Crm.Actions;
using Apps.Hubspot.Crm.Models.Deals.Request;

namespace Tests.Hubspot;

[TestClass]
public class DealTests : TestBase
{
    [TestMethod]
    public async Task SearchDeals_ShouldReturnDeals()
    {
        // Arrange
        var action = new DealActions(InvocationContext);
        var request = new SearchDealsRequest 
        { 
            CreatedFrom = new DateTime(2026, 02, 10, 10, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var response = await action.SearchDeals(request);

        // Assert
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        Assert.IsNotNull(response);
    }
}
