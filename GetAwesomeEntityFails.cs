using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using TableInputIssue.Entities;
using TableInputIssue.Helpers;

namespace TableInputIssue
{
    public class GetAwesomeEntityFails(ILogger<GetAwesomeEntityFails> logger)
    {
        [Function("GetAwesomeEntityFails01")]
        public IActionResult Run01(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "awesomeentity01/{partitionKey}/{rowKey}")] HttpRequest req,
            [TableInput("AwesomeEntities", "{partitionKey}", "{rowKey}")] AwesomeEntity entity, string partitionKey)
        {
            logger.LogInformation("GetAwesomeEntityFails01 called");
            string start = StartHelper.GetStart(partitionKey);
            return new OkObjectResult($"{start} {entity.WhatMakesThisSpecial}!");
        }

        // This Function explicitly specifies the partition key and row key as parameters above the TableInput, based on the potential
        // trick mentioned by svrooij in https://github.com/Azure/azure-functions-dotnet-worker/issues/2320#issuecomment-2065243585
        [Function("GetAwesomeEntityFails02")]
        public IActionResult Run02(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "awesomeentity02/{partitionKey}/{rowKey}")] HttpRequest req,
            string partitionKey, string rowKey,
            [TableInput("AwesomeEntities", "{partitionKey}", "{rowKey}")] AwesomeEntity entity)
        {
            logger.LogInformation("GetAwesomeEntityFails02 called");
            string start = StartHelper.GetStart(partitionKey);
            return new OkObjectResult($"{start} {entity.WhatMakesThisSpecial}! (this was retrieved from rowKey {rowKey})");
        }
    }
}
