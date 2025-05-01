using OnAirLight.CommandLine.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnAirLight.CommandLine.Hubitat
{
    public class HubitatService
    {
        private HttpClient Client { get; set; }
        private string BaseMakerApiAddress { get; set; }
        private string AccessToken { get; set; }

        private const string InitCommand = "initialize";

        private JsonSerializerOptions DefaultOptions => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public HubitatService(AppConfig config)
        {
            // Create an HttpClient that doesn't validate the server certificate
            HttpClientHandler customHttpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };

            Client = new HttpClient(customHttpClientHandler);

            BaseMakerApiAddress = $"https://{config.Hubitat.NameOrIp}/apps/api/{config.Hubitat.Id}";

            AccessToken = $"access_token={config.Hubitat.Token}";

        }

        public async Task<IList<Device>> GetDevicesAsync()
        {
            var requestUri = new UriBuilder($"{BaseMakerApiAddress}/devices");
            requestUri.Query = AccessToken;

            var response = await Client.GetAsync(requestUri.Uri);

            response.EnsureSuccessStatusCode();

            var list = await JsonSerializer.DeserializeAsync<Device[]>(await response.Content.ReadAsStreamAsync(), DefaultOptions);
            return list.ToList();
        }

        public async Task SendDeviceInitAsync(Device device)
        {
            var requestUri = new UriBuilder($"{BaseMakerApiAddress}/devices/{device.Id}/{InitCommand}");
            requestUri.Query = AccessToken;

            var response = await Client.GetAsync(requestUri.Uri);

            response.EnsureSuccessStatusCode();
        }

        public async Task SendDevicePreset(Device device, string preset)
        {
            var requestUri = new UriBuilder($"{BaseMakerApiAddress}/devices/{device.Id}/{preset}");
            requestUri.Query = AccessToken;

            var response = await Client.GetAsync(requestUri.Uri);

            response.EnsureSuccessStatusCode();
        }


    }
}
