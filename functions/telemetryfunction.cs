using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Collections.Generic;

namespace My.Function
{
    // This class processes telemetry events from IoT Hub, reads temperature of a device
    // and sets the "Temperature" property of the device with the value of the telemetry.
    public class telemetryfunction
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static string adtServiceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");

        [FunctionName("telemetryfunction")]
        public async void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            try
            {
                // After this is deployed, you need to turn the Managed Identity Status to "On",
                // Grab Object Id of the function and assigned "Azure Digital Twins Owner (Preview)" role
                // to this function identity in order for this function to be authorized on ADT APIs.
                //Authenticate with Digital Twins
                var credentials = new DefaultAzureCredential();
                log.LogInformation(credentials.ToString());
                DigitalTwinsClient client = new DigitalTwinsClient(
                    new Uri(adtServiceUrl), credentials, new DigitalTwinsClientOptions
                    { Transport = new HttpClientTransport(httpClient) });
                log.LogInformation($"ADT service client connection created.");

                if (eventGridEvent != null && eventGridEvent.Data != null)
                {

                    JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
                    string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];
                    var ID = deviceMessage["body"]["storeid"];
                    var TimeInterval = deviceMessage["body"]["TimeInterval"];
                    var ShelfId = deviceMessage["body"]["ShelfId"];
                    var ProductId = deviceMessage["body"]["ProductId"];
                    var ProductName = deviceMessage["body"]["ProductName"];
                    var ShelfContainProductNames = deviceMessage["body"]["ShelfContainProductNames"];
                    var ProductSellingRank = deviceMessage["body"]["ProductSellingRank"];
                    var SoldProductQuantityLastHour = deviceMessage["body"]["SoldProductQuantityLastHour"];
                    var SoldProductQuantityLastDay = deviceMessage["body"]["SoldProductQuantityLastDay"];
                    var SoldProductQuantityLast3Months = deviceMessage["body"]["SoldProductQuantityLast3Months"];
                    var RemainProductQuantity = deviceMessage["body"]["RemainProductQuantity"];
                    var ProductPrice = deviceMessage["body"]["ProductPrice"];
                    var ProductCost = deviceMessage["body"]["ProductCost"];
                    var ProductDiscount = deviceMessage["body"]["ProductDiscount"];
                    var ProductProfitPerItem = deviceMessage["body"]["ProductProfitPerItem"];
                    var ProductProfitPercentagePerItem = deviceMessage["body"]["ProductProfitPercentagePerItem"];
                    var ProductImageURL = deviceMessage["body"]["ProductImageURL"];
                    var ShelfRank = deviceMessage["body"]["ShelfRank"];
                    var CustomerQuantityLastHour = deviceMessage["body"]["CustomerQuantityLastHour"];
                    var CustomerQuantityLastDay = deviceMessage["body"]["CustomerQuantityLastDay"];
                    var CustomerQuantityLast3Months = deviceMessage["body"]["CustomerQuantityLast3Months"];
                    var ShelfProductNames = deviceMessage["body"]["ShelfProductNames"];
                    var ShelfItemQuantity = deviceMessage["body"]["ShelfItemQuantity"];
                    var ShelfRevenueLastHour = deviceMessage["body"]["ShelfRevenueLastHour"];
                    var ShelfRevenueLastDay = deviceMessage["body"]["ShelfRevenueLastDay"];
                    var ShelfRevenueLast3Months = deviceMessage["body"]["ShelfRevenueLast3Months"];
                    var ShelfSoldItemQuantityLastHour = deviceMessage["body"]["ShelfSoldItemQuantityLastHour"];
                    var ShelfSoldItemQuantityLastDay = deviceMessage["body"]["ShelfSoldItemQuantityLastDay"];
                    var ShelfSoldItemQuantityLast3Months = deviceMessage["body"]["ShelfSoldItemQuantityLast3Months"];
                    var ShelfCostPerItem = deviceMessage["body"]["ShelfCostPerItem"];
                    var ShelfPricePerItem = deviceMessage["body"]["ShelfPricePerItem"];
                    var ConversionRateLastHour = deviceMessage["body"]["ConversionRateLastHour"];
                    var ConversionRateLastDay = deviceMessage["body"]["ConversionRateLastDay"];
                    var ConversionRateLast3Months = deviceMessage["body"]["ConversionRateLast3Months"];
                    var ShelfProfitLastHour = deviceMessage["body"]["ShelfProfitLastHour"];
                    var ShelfProfitLastDay = deviceMessage["body"]["ShelfProfitLastDay"];
                    var ShelfProfitLast3Months = deviceMessage["body"]["ShelfProfitLast3Months"];

                    log.LogInformation($"Device:{deviceId} Device Id is: {ID}");
                    log.LogInformation($"Device:{deviceId} Time interval is: {TimeInterval}");
                    log.LogInformation($"Device:{deviceId} ShelfId is: {ShelfId}");
                    log.LogInformation($"Device:{deviceId} ProductId is: {ProductId}");
                    log.LogInformation($"Device: {deviceId} ProductName is: {ProductName}");
                    log.LogInformation($"Device: {deviceId} ShelfContainProductNames is: {ShelfContainProductNames}");
                    log.LogInformation($"Device: {deviceId} ProductSellingRank is: {ProductSellingRank}");
                    log.LogInformation($"Device: {deviceId} SoldProductQuantityLastHour: {SoldProductQuantityLastHour}");
                    log.LogInformation($"Device: {deviceId} SoldProductQuantityLastDay: {SoldProductQuantityLastDay}");
                    log.LogInformation($"Device: {deviceId} SoldProductQuantityLast3Months: {SoldProductQuantityLast3Months}");
                    log.LogInformation($"Device: {deviceId} RemainProductQuantity is: {RemainProductQuantity}");
                    log.LogInformation($"Device: {deviceId} ProductPrice is: {ProductPrice}");
                    log.LogInformation($"Device: {deviceId} ProductCost is: {ProductCost}");
                    log.LogInformation($"Device: {deviceId} ProductDiscount is: {ProductDiscount}");
                    log.LogInformation($"Device: {deviceId} ProductProfitPerItem is: {ProductProfitPerItem}");
                    log.LogInformation($"Device: {deviceId} ProductProfitPercentagePerItem is: {ProductProfitPercentagePerItem}");
                    log.LogInformation($"Device: {deviceId} ProductImageURL is: {ProductImageURL}");
                    log.LogInformation($"Device: {deviceId} ShelfRank is: {ShelfRank}");
                    log.LogInformation($"Device: {deviceId} CustomerQuantityLastHour is: {CustomerQuantityLastHour}");
                    log.LogInformation($"Device: {deviceId} CustomerQuantityLastDay is: {CustomerQuantityLastDay}");
                    log.LogInformation($"Device: {deviceId} CustomerQuantityLast3Months is: {CustomerQuantityLast3Months}");
                    log.LogInformation($"Device: {deviceId} ShelfProductNames is: {ShelfProductNames}");
                    log.LogInformation($"Device: {deviceId} ShelfItemQuantity is: {ShelfItemQuantity}");
                    log.LogInformation($"Device: {deviceId} ShelfRevenueLastHour is: {ShelfRevenueLastHour}");
                    log.LogInformation($"Device: {deviceId} ShelfRevenueLastDay is: {ShelfRevenueLastDay}");
                    log.LogInformation($"Device: {deviceId} ShelfRevenueLast3Months is: {ShelfRevenueLast3Months}");
                    log.LogInformation($"Device: {deviceId} ShelfSoldItemQuantityLastHour is: {ShelfSoldItemQuantityLastHour}");
                    log.LogInformation($"Device: {deviceId} ShelfSoldItemQuantityLastDay is: {ShelfSoldItemQuantityLastDay}");
                    log.LogInformation($"Device: {deviceId} ShelfSoldItemQuantityLast3Months is: {ShelfSoldItemQuantityLast3Months}");
                    log.LogInformation($"Device: {deviceId} ShelfCostPerItem is: {ShelfCostPerItem}");
                    log.LogInformation($"Device: {deviceId} ShelfPricePerItem is: {ShelfPricePerItem}");
                    log.LogInformation($"Device: {deviceId} ConversionRateLastHour is: {ConversionRateLastHour}");
                    log.LogInformation($"Device: {deviceId} ConversionRateLastDay is: {ConversionRateLastDay}");
                    log.LogInformation($"Device: {deviceId} ConversionRateLast3Months is: {ConversionRateLast3Months}");
                    log.LogInformation($"Device: {deviceId} ShelfProfitLastHour is: {ShelfProfitLastHour}");
                    log.LogInformation($"Device: {deviceId} ShelfProfitLastDay is: {ShelfProfitLastDay}");
                    log.LogInformation($"Device: {deviceId} ShelfProfitLast3Months is: {ShelfProfitLast3Months}");
                    var updateProperty = new JsonPatchDocument();
                    var turbineTelemetry = new Dictionary<string, Object>()
                    {
                        ["storeid"] = ID,
                        ["TimeInterval"] = TimeInterval,
                        ["ShelfId"] = ShelfId,
                        ["ProductId"] = ProductId,
                        ["ProductName"] = ProductName,
                        ["ShelfContainProductNames"] = ShelfContainProductNames,
                        ["ProductSellingRank"] = ProductSellingRank,
                        ["SoldProductQuantityLastHour"] = SoldProductQuantityLastHour,
                        ["SoldProductQuantityLastDay"] = SoldProductQuantityLastDay,
                        ["SoldProductQuantityLast3Months"] = SoldProductQuantityLast3Months,
                        ["RemainProductQuantity"] = RemainProductQuantity,
                        ["ProductPrice"] = ProductPrice,
                        ["ProductCost"] = ProductCost,
                        ["ProductDiscount"] = ProductDiscount,
                        ["ProductProfitPerItem"] = ProductProfitPerItem,
                        ["ProductProfitPercentagePerItem"] = ProductProfitPercentagePerItem,
                        ["ProductImageURL"] = ProductImageURL,
                        ["ShelfRank"] = ShelfRank,
                        ["CustomerQuantityLastHour"] = CustomerQuantityLastHour,
                        ["CustomerQuantityLastDay"] = CustomerQuantityLastDay,
                        ["CustomerQuantityLast3Months"] = CustomerQuantityLast3Months,
                        ["ShelfProductNames"] = ShelfProductNames,
                        ["ShelfItemQuantity"] = ShelfItemQuantity,
                        ["ShelfRevenueLastHour"] = ShelfRevenueLastHour,
                        ["ShelfRevenueLastDay"] = ShelfRevenueLastDay,
                        ["ShelfRevenueLast3Months"] = ShelfRevenueLast3Months,
                        ["ShelfSoldItemQuantityLastHour"] = ShelfSoldItemQuantityLastHour,
                        ["ShelfSoldItemQuantityLastDay"] = ShelfSoldItemQuantityLastDay,
                        ["ShelfSoldItemQuantityLast3Months"] = ShelfSoldItemQuantityLast3Months,
                        ["ShelfCostPerItem"] = ShelfCostPerItem,
                        ["ShelfPricePerItem"] = ShelfPricePerItem,
                        ["ConversionRateLastHour"] = ConversionRateLastHour,
                        ["ConversionRateLastDay"] = ConversionRateLastDay,
                        ["ConversionRateLast3Months"] = ConversionRateLast3Months,
                        ["ShelfProfitLastHour"] = ShelfProfitLastHour,
                        ["ShelfProfitLastDay"] = ShelfProfitLastDay,
                        ["ShelfProfitLast3Months"] = ShelfProfitLast3Months
                    };
                    updateProperty.AppendAdd("/storeid", ID.Value<string>());

                    log.LogInformation(updateProperty.ToString());
                    try
                    {
                        await client.PublishTelemetryAsync(deviceId, Guid.NewGuid().ToString(), JsonConvert.SerializeObject(turbineTelemetry));
                    }
                    catch (Exception e)
                    {
                        log.LogInformation(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }
        }
    }
}