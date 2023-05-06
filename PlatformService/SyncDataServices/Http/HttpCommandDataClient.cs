using PlatformService.Dtos;
using System.Text.Json;
using System.Text;
using PlatformService.SyncDataServices.Interfaces;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task SendPlatformToCommandAsync(PlatformReadDto platform)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(platform), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("--> Sync POST to Command Service is OK!");
            else
                Console.WriteLine($"Sync POST to Command Service is NOT OK! {response} ");
        }
    }
}