using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Pagination;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Hubspot.Crm
{
    public class HubspotClient : RestClient
    {
        public HubspotClient() :
            base(new RestClientOptions() { BaseUrl = new Uri("https://api.hubapi.com") })
        {
        }

        public BaseObjectWithProperties<T> GetFullObject<T>(HubspotRequest request) =>
            ExecuteWithError<BaseObjectWithProperties<T>>(request);

        public T GetObject<T>(HubspotRequest request) => GetFullObject<T>(request).Properties;

        public IEnumerable<BaseObject> GetMultipleObjects(HubspotRequest request) =>
            ExecuteWithError<MultipleObjects<BaseObject>>(request).Results;

        public BaseObject? PostObject(HubspotRequest request) => ExecuteWithError<BaseObject>(request);

        public T PatchObject<T>(HubspotRequest request) =>
            ExecuteWithError<BaseObjectWithProperties<T>>(request).Properties;

        public CustomPropertyEntity GetProperty(HubspotRequest request, string name)
        {
            request.AddQueryParameter("properties", name.ToApiPropertyName());
            var res = ExecuteWithError<ObjectWithCustomProperties>(request);
            var property = res?.Properties?[name.ToApiPropertyName()];
            return new CustomPropertyEntity { Property = property };
        }

        public T SetProperty<T>(HubspotRequest request, string name, string value)
        {
            request.AddObject(new Dictionary<string, string>() { { name.ToApiPropertyName(), value } });
            var res = ExecuteWithError<BaseObjectWithProperties<T>>(request);
            return res.Properties;
        }

        public T? ExecuteWithError<T>(RestRequest request)
        {
            var res = ExecuteWithError(request);
            return JsonConvert.DeserializeObject<T>(res.Content);
        }

        public RestResponse ExecuteWithError(RestRequest request)
        {
            var res = this.Execute(request);

            if (!res.IsSuccessStatusCode)
            {
                if (res.ContentType is "text/html")
                    throw new(res.StatusDescription);
                
                var error = JsonConvert.DeserializeObject<Error>(res.Content);
                throw new Exception(error?.ToString());
            }

            return res;
        }

        public List<T> Paginate<T>(RestRequest request)
        {
            var results = new List<T>();

            var baseUrl = request.Resource;
            string? after = null;

            do
            {
                if (after is not null)
                    request.Resource = QueryHelpers.AddQueryString(baseUrl, "after", after);

                var response = ExecuteWithError<MultipleObjects<T>>(request);
                after = response!.Paging?.Next?.After;

                results.AddRange(response.Results);
            } while (!string.IsNullOrEmpty(after));

            return results;
        }
    }
}