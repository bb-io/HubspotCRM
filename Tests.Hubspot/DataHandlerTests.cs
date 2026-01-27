using Apps.Hubspot.Crm.DataSourceHandlers;
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
    }
}
