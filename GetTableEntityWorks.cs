using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using TableInputIssue.Entities;
using TableInputIssue.Helpers;

namespace TableInputIssue
{
    public class GetTableEntityWorks(ILogger<GetTableEntityWorks> logger, IStartHelper startHelper)
    {
        [Function(nameof(GetTableEntityWorks))]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tableentity/{partitionKey}/{rowKey}")] HttpRequest req,
            [TableInput("AwesomeEntities", "{partitionKey}", "{rowKey}")] TableEntity entity, string partitionKey)
        {
            logger.LogInformation("GetTableEntityWorks called");
            string start = startHelper.GetStart(partitionKey);
            return new OkObjectResult($"{start} {entity[nameof(AwesomeEntity.WhatMakesThisSpecial)]}!");
        }
    }
}
