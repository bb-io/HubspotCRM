using HubspotTests.Base;

namespace Tests.Hubspot
{
    [TestClass]
    public class DealTests : TestBase
    {
        [TestMethod]
        public async Task SearchDeals_ShouldReturnDeals()
        {
            var action = new Apps.Hubspot.Crm.Actions.DealActions(InvocationContext);
            var request = new Apps.Hubspot.Crm.Models.Deals.Request.SearchDealsRequest
            {
            };
            var response = await action.SearchDeals(request);
            Console.WriteLine($"{response.Deals.Count()}");
            foreach (var item in response.Deals)
            {
                Console.WriteLine($"Deal: {item.Id}  -  {item.Dealname}  -  Stage: {item.Pipeline}");
            }
            Assert.IsNotNull(response);
        }
    }
}
