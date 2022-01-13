using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SalahService.Configuration;
using RestSharp;
using SalahService.Model;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using System.Threading;

namespace SalahService.Services
{
    /// <summary>
    /// A wrapper around AlAdhan API.
    /// </summary>
    public class AlAdhanService : IAlAdhanService, IHealthCheck
    {
        private readonly ILogger<AlAdhanService> _log;
        private readonly IOptions<AlAdhanOptions> _options;

        public AlAdhanService(ILogger<AlAdhanService> log, IOptions<AlAdhanOptions> options)
        {
            _log = log;
            _options = options;
        }

        public Data GetTimings(string latitude, string longitude)
        {
            var client = GetRestClient();
            var encodedPath = Uri.EscapeUriString($"timings?latitude={latitude}&longitude={longitude}&method=2&iso8601=true");
            var request = new RestRequest(encodedPath, Method.GET);
            
            var response = client.ExecuteGetAsync<RootObject>(request, CancellationToken.None);
            //response.Wait();
            var fullUri = (client.BuildUri(request));

            return response.Result.Data.Data;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }

        private IRestClient GetRestClient()
        {
            var resourceUrl = new Uri(_options.Value.baseUrl);

            var restClient = new RestClient(resourceUrl);
            restClient.AddDefaultHeader("accept", "api-version=6.1-preview.4");
            restClient.AddDefaultHeader("accept", "application/json");
            return restClient;
        }
    }
}
