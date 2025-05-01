using Microsoft.Graph;
using OnAirLight.CommandLine.Authentication;
using OnAirLight.CommandLine.Config;
using System;
using System.Threading.Tasks;

namespace OnAirLight.CommandLine.Graph
{
    public class GraphService
    {
        private readonly GraphServiceClient _graphClient;

        public GraphService(AppConfig config)
        {
            var authProvider = new DeviceCodeAuthProvider(config.Azure.AppId, config.Azure.TenantId, config.Azure.AllScopes);
            _graphClient = new GraphServiceClient(authProvider);
        }

        public async Task<(string availability, string activity)> GetPresenceAsync(string userId)
        {
            var response = await _graphClient.Communications.GetPresencesByUserId(new[] { userId })
                .Request()
                .PostAsync();


            if (response is null || response.Count == 0)
            {
                throw new InvalidOperationException($"Presence info not found for user ID '{userId}'");
            }

            if (response.Count > 1)
            {
                throw new InvalidOperationException($"Multiple presence results for user ID '{userId}'");
            }

            return (response[0].Availability, response[0].Activity);
        }

    }
}
