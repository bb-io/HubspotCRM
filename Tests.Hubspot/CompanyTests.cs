using Apps.Hubspot.Crm.Actions;
using HubspotTests.Base;

namespace Tests.Hubspot
{
    [TestClass]
    public class CompanyTests : TestBase
    {
        [TestMethod]
        public async Task SearchCompanies_ShouldReturnCompanies()
        {
            var action = new CompanyActions(InvocationContext);
            var request = new Apps.Hubspot.Crm.Models.Companies.Request.SearchCompaniesRequest
            {
                Status = new List<string> { "lead" },
            };

            var response = await action.SearchCompanies(request);
            Console.WriteLine($"{response.Companies.Count()}");
            foreach (var item in response.Companies)
            {
                Console.WriteLine($"Company: {item.Id}  -  {item.Name}  -  Created {item.CreatedAt}  -  Updated {item.UpdatedAt}  -  {item.Lifecyclestage}");
            }

            Assert.IsNotNull(response);
        }
    }
}
