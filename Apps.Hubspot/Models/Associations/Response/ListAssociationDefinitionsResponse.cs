using Blackbird.Applications.Sdk.Common;

namespace Apps.Hubspot.Crm.Models.Associations.Response;

public record ListAssociationDefinitionsResponse([property: Display("Association types")]
    List<AssociationType> AssociationTypes);