using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Hubspot.Crm.Actions;
using Apps.Hubspot.Crm.Models.Companies.Response;
using HubspotTests.Base;

namespace Tests.Hubspot
{
    [TestClass]
    public class ProjectTests : TestBase
    {
        [TestMethod]
        public async Task CreateProject_IsSuccess()
        {
            var action = new CompanyActions(InvocationContext);

            var input = new CompanyProperties { Domain = "velodeville.com ", Name ="Test" };
            var response = await action.CreateCompany(input);

            Assert.IsNotNull(response.ToString());
        }
    }
}
