using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SignalRFunctions
{
    public static class SignalRFunctions
    {
        public static string storeid;
        public static string TimeInterval;
        public static int ShelfId;
        public static int ProductId;
        public static string ProductName;
        public static string ShelfContainProductNames;
        public static int ProductSellingRank;
        public static int SoldProductQuantityLastHour;
        public static int SoldProductQuantityLastDay;
        public static int SoldProductQuantityLast3Months;
        public static int RemainProductQuantity;
        public static float ProductPrice;
        public static float ProductCost;
        public static float ProductDiscount;
        public static float ProductProfitPerItem;
        public static float ProductProfitPercentagePerItem;
        public static string ProductImageURL;
        public static int ShelfRank;
        public static int CustomerQuantityLastHour;
        public static int CustomerQuantityLastDay;
        public static int CustomerQuantityLast3Months;
        public static string ShelfProductNames;
        public static int ShelfItemQuantity;
        public static float ShelfRevenueLastHour;
        public static float ShelfRevenueLastDay;
        public static float ShelfRevenueLast3Months;
        public static int ShelfSoldItemQuantityLastHour;
        public static int ShelfSoldItemQuantityLastDay;
        public static int ShelfSoldItemQuantityLast3Months;
        public static float ShelfCostPerItem;
        public static float ShelfPricePerItem;
        public static float ConversionRateLastHour;
        public static float ConversionRateLastDay;
        public static float ConversionRateLast3Months;
        public static float ShelfProfitLastHour;
        public static float ShelfProfitLastDay;
        public static float ShelfProfitLast3Months;

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "dttelemetry")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("broadcast")]
        public static Task SendMessage(
            [EventGridTrigger] EventGridEvent eventGridEvent,
            [SignalR(HubName = "dttelemetry")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            JObject eventGridData = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
            if (eventGridEvent.EventType.Contains("telemetry"))
            {
                var data = eventGridData.SelectToken("data");

                var telemetryMessage = new Dictionary<object, object>();
                foreach (JProperty property in data.Children())
                {
                    log.LogInformation(property.Name + " - " + property.Value);
                    telemetryMessage.Add(property.Name, property.Value);
                }
                return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "TelemetryMessage",
                    Arguments = new[] { telemetryMessage }
                });
            }
            else
            {
                try
                {
                    storeid = eventGridEvent.Subject;
                    
                    var data = eventGridData.SelectToken("data");
                    var patch = data.SelectToken("patch");
                   
                    var property = new Dictionary<object, object>
                    {
                        {"storeid", storeid }
                    };
                    return signalRMessages.AddAsync(
                        new SignalRMessage
                        {
                            Target = "PropertyMessage",
                            Arguments = new[] { property }
                        });
                }
                catch (Exception e)
                {
                    log.LogInformation(e.Message);
                    return null;
                }
            }

        }
    }
}