using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SmartFactory.Contracts.Telemetry;

namespace TelemetryWorker
{
    public class FactoryApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<FactoryApiClient> _logger;

        public FactoryApiClient(HttpClient client, ILogger<FactoryApiClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task PushLiveUpdate(MachineTelemetryLiveUpdate liveUpdate)
        {
            var response = await _client.PostAsJsonAsync("/internal/telemetry", liveUpdate);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("failed to post live update to factory api {}", response.StatusCode);
            }
        }
    }
}