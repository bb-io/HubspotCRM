# Blackbird.io Hubspot CRM

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Hubspot CRM is a popular and widely-used customer relationship management platform designed to help businesses manage their sales, marketing, and customer service efforts in a more efficient and organized manner. This Hubspot CRM application primarily centers around company, deal, ticket, contact and associations management.

## Connecting
1. Navigate to apps and search for **Hubspot (CRM)**. If you cannot find Hubspot (CRM) then click _Add App_ in the top right corner, select Hubspot (CRM) and add the app to your Blackbird environment.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My organization'.
4. Click _Authorize connection_.
5. Follow the instructions that HubSpot CRM gives you, authorizing Blackbird.io to act on your behalf.
6. When you return to Blackbird, confirm that the connection has appeared and the status is _Connected_.

## Actions

### Associations

-   **List associations** returns a list of specified object associations.
-   **List association definitions** returns a list of specified object association definitions.
-   **Create association label** sets association labels between two records.
-   **Create/delete association**
-   **Create/delete association definition**

### Companies

-  **Get all companies** returns a list of all companies.
-  **Get company address** returns full company address.
-  **Get company by custom property** returns a company by specified property.
-  **Get/create/delete company**
-  **Get/set company property**

### Contacts

-  **Get all contacts** returns a list of all contacts.
-  **Get/create/delete contact**
-  **Get/set contact property**

### Deals 

-  **Get all deals** returns a list of all deals.
-  **Get/create/delete deal**
-  **Get/set deal property**

### Quotes

-  **Get all quotes** returns a list of all quotes.
-  **Get/create/delete quote**
-  **Get/set quote property**

### Tickets

-  **Get all tickets** returns a list of all tickets.
-  **Get/create/delete ticket**
-  **Get/set ticket property**

## Events

-   **On company created/deleted/merged**
-   **On company association/property changed**
-   **On contact created/deleted/merged/restored**
-   **On contact association/property changed**
-   **On contact deleted for privacy**
-   **On conversation created/deleted**
-   **On conversation deleted for privacy**
-   **On conversation new message**
-   **On conversation property changed**
-   **On deal created/deleted/merged/restored**
-   **On deal association/property changed**
-   **On line item created/deleted/merged/restored**
-   **On line item association/property changed**
-   **On product created/deleted/merged/restored**
-   **On product property changed**
-   **On ticket created/deleted/merged/restored**
-   **On ticket association/property changed**

## Missing features

In the future we can add actions for:

-   Taxes
-   Goals
-   Line items

## Feedback

Feedback to our implementation of Hubspot CRM is always very welcome. Reach out to us using the [established channels](https://www.blackbird.io/), or create an issue.

<!-- end docs -->