using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Document = Microsoft.Azure.Documents.Document;

namespace Rogue.Development.Channels.Stocks;

public static class Stocks
{
    [FunctionName("negotiate")]
    public static SignalRConnectionInfo Negotiate(
        [HttpTrigger(AuthorizationLevel.Anonymous)]
        HttpRequest req,
        [SignalRConnectionInfo(HubName = "stocks")]
        SignalRConnectionInfo connectionInfo)
    {
        return connectionInfo;
    }

    [FunctionName("stocksChanged")]
    public static void StocksChanged([CosmosDBTrigger(
            databaseName: "Rogue",
            collectionName: "stocks",
            ConnectionStringSetting = "AzureCosmosDBConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true,
            FeedPollDelay = 500)]
        IReadOnlyList<Document> stockUpdates, ILogger logger,
        [SignalR(HubName = "stocks")] IAsyncCollector<SignalRMessage> signalRMessages)
    {
        var updates = stockUpdates.Select(stock => new StockUpdateDto
        {
            StockId = Guid.NewGuid(),
            Value = 1
        });

        signalRMessages.AddAsync(
            new SignalRMessage
            {
                Target = "stockUpdate",
                Arguments = new object[] { updates }
            });
    }
}