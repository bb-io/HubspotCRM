﻿using System.Net.Mime;
using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Extensions;
using Apps.Hubspot.Crm.Models;
using Apps.Hubspot.Crm.Models.Entities;
using Apps.Hubspot.Crm.Models.Entities.Base;
using Apps.Hubspot.Crm.Models.Pagination;
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

    public Task<BaseObjectWithProperties<T>> GetFullObject<T>(RestRequest request) =>
        ExecuteWithErrorHandling<BaseObjectWithProperties<T>>(request);

    public async Task<IEnumerable<BaseObject>> GetMultipleObjects(RestRequest request)
    {
        var response = await ExecuteWithErrorHandling<MultipleObjects<BaseObject>>(request);
        return response.Results;
    }

    public async Task<CustomPropertyEntity> GetProperty(RestRequest request, string name)
    {
        request.AddQueryParameter("properties", name.ToApiPropertyName());
        
        var res = await ExecuteWithErrorHandling<ObjectWithCustomProperties>(request);
        return new()
        {
            Property = res.Properties?[name.ToApiPropertyName()]
        };
    }

    public Task<BaseObjectWithProperties<T>> SetProperty<T>(RestRequest request, string name, string value)
    {
        request.AddObject(new Dictionary<string, string>() { { name.ToApiPropertyName(), value } });
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
            return new(response.StatusDescription);

        var error = JsonConvert.DeserializeObject<Error>(response.Content!);
        return new(error?.ToString());
    }
}