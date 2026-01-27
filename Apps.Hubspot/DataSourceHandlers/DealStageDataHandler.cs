using Apps.Hubspot.Crm.Api;
using Apps.Hubspot.Crm.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Hubspot.Crm.DataSourceHandlers
{
    public class DealStageDataHandler(InvocationContext invocationContext)
    : HubspotInvocable(invocationContext), IAsyncDataSourceHandler
    {
        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new HubspotRequest("/crm/v3/pipelines/deals", Method.Get, Creds);
            var response = await Client.ExecuteWithErrorHandling<PipelinesResponse>(request);

            var search = (context.SearchString ?? string.Empty).Trim();

            var result = new Dictionary<string, string>();

            foreach (var pipeline in response.Results ?? new())
            {
                var pipelineLabel = string.IsNullOrWhiteSpace(pipeline.Label) ? pipeline.Id : pipeline.Label;

                foreach (var stage in pipeline.Stages ?? new())
                {
                    if (stage.Archived == true)
                        continue;

                    if (string.IsNullOrWhiteSpace(stage.Id))
                        continue;

                    var stageLabel = string.IsNullOrWhiteSpace(stage.Label) ? stage.Id : stage.Label;
                    var label = $"{pipelineLabel} — {stageLabel}";

                    if (!string.IsNullOrWhiteSpace(search) &&
                        !label.Contains(search, StringComparison.OrdinalIgnoreCase) &&
                        !(stage.Id?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false))
                    {
                        continue;
                    }

                    if (!result.ContainsKey(stage.Id!))
                        result.Add(stage.Id!, label);
                }
            }

            return result;
        }
    }
    public class PipelinesResponse
    {
        [JsonProperty("results")]
        public List<Pipeline>? Results { get; set; }
    }

    public class Pipeline
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("label")]
        public string? Label { get; set; }

        [JsonProperty("stages")]
        public List<Stage>? Stages { get; set; }
    }

    public class Stage
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("label")]
        public string? Label { get; set; }

        [JsonProperty("archived")]
        public bool? Archived { get; set; }
    }
}