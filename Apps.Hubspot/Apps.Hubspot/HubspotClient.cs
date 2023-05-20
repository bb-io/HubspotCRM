using Apps.Hubspot.Crm.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Hubspot.Crm
{
    public class HubspotClient : RestClient
    {
        public HubspotClient() :
            base(new RestClientOptions() { BaseUrl = new Uri("https://api.hubapi.com") })
        { }

        public T GetObject<T>(HubspotRequest request) => ExecuteWithError<BaseObjectWithProperties<T>>(request).Properties;
        public IEnumerable<BaseObject> GetMultipleObjects(HubspotRequest request) => ExecuteWithError<MultipleObjects<BaseObject>>(request).Results;
        public BaseObject? PostObject(HubspotRequest request) => ExecuteWithError<BaseObject>(request);
        public T PatchObject<T>(HubspotRequest request) => ExecuteWithError<BaseObjectWithProperties<T>>(request).Properties;

        public T? ExecuteWithError<T>(HubspotRequest request)
        {
            var res = this.Execute(request);
            if (!res.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<Error>(res.Content);
                throw new Exception(error?.ToString());
            }
            return JsonConvert.DeserializeObject<T>(res.Content);
        }
    }
}
