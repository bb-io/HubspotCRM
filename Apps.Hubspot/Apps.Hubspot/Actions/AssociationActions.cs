using Apps.Hubspot.Crm.Constants;
using Apps.Hubspot.Crm.Models.Associations.Request;
using Apps.Hubspot.Crm.Models.Associations.Response;
using Apps.Hubspot.Crm.Models.Pagination;
using Apps.Hubspot.Crm.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Hubspot.Crm.Actions;

[ActionList]
public class AssociationActions
{
    #region Fields

    private HubspotClient Client { get; }

    #endregion

    #region Constructors

    public AssociationActions()
    {
        Client = new();
    }

    #endregion

    #region Actions

    [Action("Create association label", Description = "Set association labels between two records")]
    public void CreateAssociationLabel(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateAssociationLabelRequest input)
    {
        var sourceType = input.FromObjectType.ToLower();
        var targetType = input.ToObjectType.ToLower();

        if (!AreObjectTypesValid(sourceType, targetType, out var error))
            throw new(error);

        var endpoint =
            $"/crm/v4/objects/{sourceType}/{input.FromObjectId}/associations/{targetType}/{input.ToObjectId}";
        var request = new HubspotRequest(endpoint, Method.Put, authenticationCredentialsProviders);
        request.AddJsonBody(new[]
        {
            new AssociationLabel()
            {
                AssociationTypeId = IntParser.Parse(input.AssociationTypeId, nameof(input.AssociationTypeId))!.Value,
                AssociationCategory = input.IsUserDefined ? "USER_DEFINED" : "HUBSPOT_DEFINED"
            }
        });

        Client.ExecuteWithError(request);
    }

    [Action("Create association", Description = "Create association type between two object types")]
    public void CreateAssociation(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] ManageAssociationRequest input)
    {
        var sourceType = input.FromObjectType.ToLower();
        var targetType = input.ToObjectType.ToLower();

        if (!AreObjectTypesValid(sourceType, targetType, out var error))
            throw new(error);

        var endpoint =
            $"/crm/v4/objects/{sourceType}/{input.FromObjectId}/associations/default/{targetType}/{input.ToObjectId}";
        var request = new HubspotRequest(endpoint, Method.Put, authenticationCredentialsProviders);

        Client.ExecuteWithError(request);
    }
    
    [Action("Create association definition", Description = "Create a user defined association definition")]
    public AssociationType CreateAssociationDefinition(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] CreateAssociationDefinitionRequest input)
    {
        var sourceType = input.FromObjectType.ToLower();
        var targetType = input.ToObjectType.ToLower();

        if (!AreObjectTypesValid(sourceType, targetType, out var error))
            throw new(error);

        var endpoint =
            $"/crm/v4/associations/{sourceType}/{targetType}/labels";
        var request = new HubspotRequest(endpoint, Method.Post, authenticationCredentialsProviders);
        request.AddJsonBody(input);

        var response = Client.ExecuteWithError<MultipleObjects<AssociationType>>(request);
        return response.Results.First();
    }

    [Action("List associations", Description = "List all associations of an object by object type")]
    public ListAssociationsResponse ListAssociations(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] ListAssociationsRequest input)
    {
        var sourceType = input.FromObjectType.ToLower();
        var targetType = input.ToObjectType.ToLower();

        if (!AreObjectTypesValid(sourceType, targetType, out var error))
            throw new(error);

        var endpoint = $"/crm/v4/objects/{sourceType}/{input.FromObjectId}/associations/{targetType}";
        var request = new HubspotRequest(endpoint, Method.Get, authenticationCredentialsProviders);

        var response = Client.Paginate<AssociationResponse>(request);
        return new(response);
    }

    [Action("List association definitions", Description = "List all associations of an object by object type")]
    public ListAssociationDefinitionsResponse ListAssociationDefinitions(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] ListAssociationsRequest input)
    {
        var sourceType = input.FromObjectType.ToLower();
        var targetType = input.ToObjectType.ToLower();

        if (!AreObjectTypesValid(sourceType, targetType, out var error))
            throw new(error);

        var endpoint = $"/crm/v4/associations/{sourceType}/{targetType}/labels";
        var request = new HubspotRequest(endpoint, Method.Get, authenticationCredentialsProviders);

        var response = Client.Paginate<AssociationType>(request);
        return new(response);
    }

    [Action("Delete association", Description = "Deletes all associations between two records")]
    public void DeleteAssociation(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] ManageAssociationRequest input)
    {
        var sourceType = input.FromObjectType.ToLower();
        var targetType = input.ToObjectType.ToLower();

        if (!AreObjectTypesValid(sourceType, targetType, out var error))
            throw new(error);

        var endpoint =
            $"/crm/v4/objects/{sourceType}/{input.FromObjectId}/associations/{targetType}/{input.ToObjectId}";
        var request = new HubspotRequest(endpoint, Method.Delete, authenticationCredentialsProviders);

        Client.ExecuteWithError(request);
    }

    [Action("Delete association definition", Description = "Deletes an association definition")]
    public void DeleteAssociationDefinition(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        [ActionParameter] DeleteAssociationDefinitionRequest input)
    {
        var sourceType = input.FromObjectType.ToLower();
        var targetType = input.ToObjectType.ToLower();

        if (!AreObjectTypesValid(sourceType, targetType, out var error))
            throw new(error);

        var endpoint = $"/crm/v4/associations/{sourceType}/{targetType}/labels/{input.AssociationTypeId}";
        var request = new HubspotRequest(endpoint, Method.Delete, authenticationCredentialsProviders);

        Client.ExecuteWithError(request);
    }

    #endregion

    #region Utils

    private bool AreObjectTypesValid(string fromObjectType, string toObjectType, out string? error)
    {
        var errorMessage = new List<string>();
        error = null;

        if (!Types.ObjectTypes.Contains(fromObjectType))
            errorMessage.Add("Source object type is invalid");

        if (!Types.ObjectTypes.Contains(toObjectType))
            errorMessage.Add("Target object type is invalid");

        if (errorMessage.Any())
        {
            errorMessage.Add($"Valid values are: {string.Join(", ", Types.ObjectTypes)}");
            error = string.Join("; ", errorMessage);

            return false;
        }

        return true;
    }

    #endregion
}