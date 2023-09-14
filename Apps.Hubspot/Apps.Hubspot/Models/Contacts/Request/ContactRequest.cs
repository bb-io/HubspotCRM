﻿using Apps.Hubspot.Crm.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Hubspot.Crm.Models.Contacts.Request;

public class ContactRequest
{
    [Display("Contact")]
    [DataSource(typeof(ContactHandler))]
    public string ContactId { get; set; }
}