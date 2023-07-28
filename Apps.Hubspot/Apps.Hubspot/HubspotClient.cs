using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Outputs;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Hubspot.Crm
{
    public class HubspotClient : RestClient
    {
        public HubspotClient() :
            base(new RestClientOptions() { BaseUrl = new Uri("https://api.hubapi.com") })
        { }

        public BaseObjectWithProperties<T> GetFullObject<T>(HubspotRequest request) => ExecuteWithError<BaseObjectWithProperties<T>>(request);
        public T GetObject<T>(HubspotRequest request) => GetFullObject<T>(request).Properties;
        public IEnumerable<BaseObject> GetMultipleObjects(HubspotRequest request) => ExecuteWithError<MultipleObjects<BaseObject>>(request).Results;
        public BaseObject? PostObject(HubspotRequest request) => ExecuteWithError<BaseObject>(request);
        public T PatchObject<T>(HubspotRequest request) => ExecuteWithError<BaseObjectWithProperties<T>>(request).Properties;
        public CustomProperty GetProperty(HubspotRequest request, string name)
        {
            request.AddQueryParameter("properties", name.ToApiPropertyName());
            var res = ExecuteWithError<ObjectWithCustomProperties>(request);
            var property = res?.Properties?[name.ToApiPropertyName()];
            return new CustomProperty { Property = property };
        }

        public T SetProperty<T>(HubspotRequest request, string name, string value)
        {
            request.AddObject(new Dictionary<string, string>() { { name.ToApiPropertyName(), value } });
            var res = ExecuteWithError<BaseObjectWithProperties<T>>(request);
            return res.Properties;
        }

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
