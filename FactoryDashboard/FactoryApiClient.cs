using System.Net.Http.Json;
using SmartFactory.Contracts.Machines;

namespace FactoryDashboard
{
    public class FactoryApiClient
    {
        private readonly HttpClient _httpClient;
        public FactoryApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<MachineResponse>> GetMachinesAsync()
        {
            var response = await _httpClient.GetAsync("/api/machines");
            response.EnsureSuccessStatusCode();

            var machines = await response.Content.ReadFromJsonAsync<List<MachineResponse>>();
            return machines ?? [];
        }
    }
}