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

    [TestMethod]
    public async Task SetDealBooleanProperty_ShouldWork()
    {
        // Arrange
        var action = new DealActions(InvocationContext);
        var request = new DealRequest
        {
            DealId = "55827471492"
        };

        var property = "hs_is_closed";
        bool value = false;

        // Act
        var response = await action.SetDealBooleanProperty(request, property, value);

        // Assert
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        Assert.IsNotNull(response);
    }
}
