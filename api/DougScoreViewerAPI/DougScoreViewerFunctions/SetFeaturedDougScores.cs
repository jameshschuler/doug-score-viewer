using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DougScoreViewer.DougScoreViewerFunctions;

public static class SetFeaturedDougScores
{
    [FunctionName("SetFeaturedDougScores")]
    public static async Task RunAsync([TimerTrigger("0 5 0 * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

        try
        {
            var client = new HttpClient();
            var requestUri = Environment.GetEnvironmentVariable("SetFeaturedDougScoresEndpoint");
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri));

            if (response.IsSuccessStatusCode)
            {
                log.LogInformation("Success!");
            }
            else
            {
                log.LogError("API responded with status code: " + response.StatusCode.ToString());
            }
        }
        catch (Exception ex)
        {
            log.LogError("Error Occurred!" + ex.Message);
        }
    }
}