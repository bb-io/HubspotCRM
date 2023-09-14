using Apps.Hubspot.Crm.Models.Entities.Base;

namespace Apps.Hubspot.Crm.Models;

public record ListItemsResponse(IEnumerable<BaseObject> Items);