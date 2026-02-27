using Apps.Hubspot.Crm.DataSourceHandlers;
using Apps.Hubspot.Crm.DataSourceHandlers.PropertiesHandlers;
using HubspotTests.Base;

namespace Tests.Hubspot
{
    [TestClass]
    public class DataHandlerTests : TestBase
    {
        [TestMethod]
        public async Task CompanyLifecycleStageDataHandler_IsSuccess()
        {
            var handler = new CompanyLifecycleStageDataHandler(InvocationContext);

            var result = await handler.GetDataAsync(new(), CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DealStageDataHandler_IsSuccess()
        {
            var handler = new DealStageDataHandler(InvocationContext);

            var result = await handler.GetDataAsync(new(), CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DealPropertiesDataHandler_IsSuccess()
        {
            var handler = new DealPropertiesDataHandler(InvocationContext);

            var result = await handler.GetDataAsync(new(), CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Assert.IsNotNull(result);
        } 

        [TestMethod]
        public async Task CompanyPropertiesDataHandler_IsSuccess()
        {
            var handler = new CompanyPropertiesDataHandler(InvocationContext);

            var result = await handler.GetDataAsync(new(), CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DealBooleanPropertiesDataHandler_IsSuccess()
        {
            var handler = new DealBooleanPropertiesDataHandler(InvocationContext);

            var result = await handler.GetDataAsync(new Blackbird.Applications.Sdk.Common.Dynamic.DataSourceContext { SearchString = "" }, CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DealDataHandler_IsSuccess()
        {
            var handler = new DealDataHandler(InvocationContext);

            var result = await handler.GetDataAsync(new Blackbird.Applications.Sdk.Common.Dynamic.DataSourceContext { SearchString= "" }, CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Assert.IsNotNull(result);
        }
    }
}
