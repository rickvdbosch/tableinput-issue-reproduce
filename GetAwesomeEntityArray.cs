using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using TableInputIssue.Entities;

namespace TableInputIssue
{
    public class GetAwesomeEntityArray(ILogger<GetAwesomeEntityArray> logger)
    {
        [Function(nameof(GetAwesomeEntityArray))]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "awesomeentityarray/{partitionKey}/{rowKey}")] HttpRequest req,
            [TableInput("AwesomeEntities", "{partitionKey}", "{rowKey}", Connection = "StorageConnectionString")] IEnumerable<AwesomeEntity> entities)
        {
            logger.LogInformation("GetAwesomeEntityArray called");
            return new OkObjectResult(entities);
        }
    }
}
