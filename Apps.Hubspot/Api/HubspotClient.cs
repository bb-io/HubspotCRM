using System.Net.Mime;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Pagination;
using Apps.Hubspot.Crm.Models.Properties.Request;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Hubspot.Crm.Api;

public class HubspotClient : BlackBirdRestClient
{
    public HubspotClient() :
        base(new()
        {
            BaseUrl = Urls.Api.ToUri()
        })
    {
    }

    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        string content = (await ExecuteWithErrorHandling(request)).Content;
        T val = JsonConvert.DeserializeObject<T>(content, JsonSettings);
        if (val == null)
        {
            throw new Exception($"Could not parse {content} to {typeof(T)}");
        }

        return val;
    }

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        RestResponse restResponse = await ExecuteAsync(request);
        if (!restResponse.IsSuccessStatusCode)
        {
            throw ConfigureErrorException(restResponse);
        }

        return restResponse;
    }

    public Task<BaseObjectWithProperties<T>> GetFullObject<T>(RestRequest request) =>
        ExecuteWithErrorHandling<BaseObjectWithProperties<T>>(request);

    public async Task<IEnumerable<BaseObject>> GetMultipleObjects(RestRequest request)
    {
        var response = await ExecuteWithErrorHandling<MultipleObjects<BaseObject>>(request);
        return response.Results;
    }

    public async Task<CustomPropertyEntity> GetProperty(RestRequest request, string name)
    {
        try
        {
            request.AddQueryParameter("properties", name.ToApiPropertyName());

            var res = await ExecuteWithErrorHandling<ObjectWithCustomProperties>(request);
            return new()
            {
                Value = res.Properties?[name.ToApiPropertyName()]
            };
        }
        catch (Exception ex)
        {
            throw new PluginApplicationException($"Could not get property {name}, error: {ex.Message}");
        }    
    }

    public Task<BaseObjectWithProperties<T>> SetProperty<T>(RestRequest request, string name, string value)
    {
        var payload = new PropertiesRequest()
        {
            Properties = new() { { name.ToApiPropertyName(), value } }
        };
        request.AddJsonBody(payload);
        
        return ExecuteWithErrorHandling<BaseObjectWithProperties<T>>(request);
    }

    public async Task<List<T>> Paginate<T>(RestRequest request)
    {
        var results = new List<T>();

        var baseUrl = request.Resource;
        string? after = null;

        do
        {
            if (after is not null)
                request.Resource = baseUrl.SetQueryParameter("after", after);

            var response = await ExecuteWithErrorHandling<MultipleObjects<T>>(request);
            after = response.Paging?.Next?.After;

            results.AddRange(response.Results);
        } while (!string.IsNullOrEmpty(after));

        return results;
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (response.ContentType is MediaTypeNames.Text.Html)
            throw new PluginApplicationException(response.StatusDescription);

        var error = JsonConvert.DeserializeObject<Error>(response.Content!);

        if (error != null && string.Equals(error.Category, "VALIDATION_ERROR", StringComparison.OrdinalIgnoreCase))
        {
            return new PluginApplicationException($"The specified inputs are invalid, message {error.Message}. Please check the inputs and try again.");
        }

        throw new PluginApplicationException(error?.Message);
    }
}