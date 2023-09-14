using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Apps.Hubspot.Crm.Models.Associations.Request;
using Apps.Hubspot.Crm.Models.Associations.Response;
using Apps.Hubspot.Crm.Models.Pagination;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Parsers;
using RestSharp;

namespace Apps.Hubspot.Crm.Actions;

[ActionList]
public class AssociationActions : HubspotInvocable
{
    public AssociationActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    #region Actions

    [Action("Create association label", Description = "Set association labels between two records")]
    public Task CreateAssociationLabel([ActionParameter] CreateAssociationLabelRequest input)
    {
        var sourceType = input.FromObjectType;
        var targetType = input.ToObjectType;

        var endpoint =
            $"/crm/v4/objects/{sourceType}/{input.FromObjectId}/associations/{targetType}/{input.ToObjectId}";
        var request = new HubspotRequest(endpoint, Method.Put, Creds).WithJsonBody(new[]
        {
            new AssociationLabel()
            {
                AssociationTypeId = IntParser.Parse(input.AssociationTypeId, nameof(input.AssociationTypeId))!.Value,
                AssociationCategory = input.IsUserDefined ? "USER_DEFINED" : "HUBSPOT_DEFINED"
            }
        });

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Create association", Description = "Create association type between two object types")]
    public Task CreateAssociation([ActionParameter] ManageAssociationRequest input)
    {
        var sourceType = input.FromObjectType;
        var targetType = input.ToObjectType;

        var endpoint =
            $"/crm/v4/objects/{sourceType}/{input.FromObjectId}/associations/default/{targetType}/{input.ToObjectId}";
        var request = new HubspotRequest(endpoint, Method.Put, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Create association definition", Description = "Create a user defined association definition")]
    public async Task<AssociationType> CreateAssociationDefinition(
        [ActionParameter] CreateAssociationDefinitionRequest input)
    {
        var sourceType = input.FromObjectType;
        var targetType = input.ToObjectType;

        var endpoint = $"/crm/v4/associations/{sourceType}/{targetType}/labels";
        var request = new HubspotRequest(endpoint, Method.Post, Creds).WithJsonBody(input);

        var response = await Client.ExecuteWithErrorHandling<MultipleObjects<AssociationType>>(request);
        return response.Results.First();
    }

    [Action("List associations", Description = "List all associations of an object by object type")]
    public async Task<ListAssociationsResponse> ListAssociations([ActionParameter] ListAssociationsRequest input)
    {
        var sourceType = input.FromObjectType;
        var targetType = input.ToObjectType;

        var endpoint = $"/crm/v4/objects/{sourceType}/{input.FromObjectId}/associations/{targetType}";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        var response = await Client.Paginate<AssociationResponse>(request);
        return new(response);
    }

    [Action("List association definitions", Description = "List all associations of an object by object type")]
    public async Task<ListAssociationDefinitionsResponse> ListAssociationDefinitions(
        [ActionParameter] ListAssociationsRequest input)
    {
        var sourceType = input.FromObjectType;
        var targetType = input.ToObjectType;

        var endpoint = $"/crm/v4/associations/{sourceType}/{targetType}/labels";
        var request = new HubspotRequest(endpoint, Method.Get, Creds);

        var response = await Client.Paginate<AssociationType>(request);
        return new(response);
    }

    [Action("Delete association", Description = "Deletes all associations between two records")]
    public Task DeleteAssociation([ActionParameter] ManageAssociationRequest input)
    {
        var sourceType = input.FromObjectType;
        var targetType = input.ToObjectType;

        var endpoint =
            $"/crm/v4/objects/{sourceType}/{input.FromObjectId}/associations/{targetType}/{input.ToObjectId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    [Action("Delete association definition", Description = "Deletes an association definition")]
    public Task DeleteAssociationDefinition([ActionParameter] DeleteAssociationDefinitionRequest input)
    {
        var sourceType = input.FromObjectType;
        var targetType = input.ToObjectType;

        var endpoint = $"/crm/v4/associations/{sourceType}/{targetType}/labels/{input.AssociationTypeId}";
        var request = new HubspotRequest(endpoint, Method.Delete, Creds);

        return Client.ExecuteWithErrorHandling(request);
    }

    #endregion
}