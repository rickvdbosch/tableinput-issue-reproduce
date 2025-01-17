# `TableInput` issue reproduction repo

The only reason for this repo to exist is to reproduce the TableInput issue.  

## Context

There's an Azure Storage account pblcstrg001 that has a table AwesomeEntities that holds 6 entities:

|PartitionKey|RowKey|WhatMakesThisSpecial|
|-|-|-|
|here-be|001|dragons|
|here-be|002|monsters|
|here-be|003|TableInput issues...|
|you-shall|001|pass|
|you-shall|002|steal|
|you-shall|003|covet|

We want to build an Azure Function that gets a specific `AwesomeEntity` from that table using `TableInput`.

## The Functions

This repo holds an Azure Functions project with 3 (actually 4) Functions:

1. `GetAwesomeEntityArray`  
This Function shows that a `TableInput` binding with a PartitionKey and a RowKey returns an IEnumerable of `AwesomeEntity` instances holding all the entities in the specified partition. One would expect one `AwesomeEntity`, but trying this results in an error as can be seen when looking at the second Azure Function.

2. `GetAwesomeEntityFails`  
This Function shows that a `TableInput` binding with a PartitionKey and a RowKey errors out when trying to bind it to the `AwesomeEntity` custom entity type. This Function is actually split up in two Functions:  
a. `GetAwesomeEntityFails01` has bare signature  
b. `GetAwesomeEntityFails02` includes explicit parameters for `partitionKey` and `rowKey` as proposed as a trick by svrooij in [this comment](https://github.com/Azure/azure-functions-dotnet-worker/issues/2320#issuecomment-2065243585) in the GitHub issue.

3. `GetTableEntityWorks`
This Function shows that a `TableInput` binding with a PartitionKey and a RowKey works when binding it to a `TableEntity`.

## Security

Yes, this repo contains a secret. No, this normally is _not_ recommended. However, this secret is a read-only SaS token for tables only. To ensure reproducing the issue works completely, I chose to add this. The Storage Account the SaS connection string points to is being monitored, and exists in its own shielded environment 😎  

### This is fine ...  

![This is fine.](https://media.npr.org/assets/img/2023/01/14/this-is-fine_custom-b7c50c845a78f5d7716475a92016d52655ba3115.jpg)
