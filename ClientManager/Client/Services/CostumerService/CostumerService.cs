using ClientManager.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ClientManager.Client.Services.CostumerService
{
    public class CostumerService : ICostumerService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public CostumerService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Costumer> Costumers { get; set; } = new List<Costumer>();

        public async Task GetCostumers()
        {
            var results = await _http.GetFromJsonAsync<List<Costumer>>("api/costumers");
            if (results !=null)
            {
                Costumers = results;
            }
        }


        public async Task<Costumer> GetSingleCostumer(int id)
        {
            var results = await _http.GetFromJsonAsync<Costumer>($"api/costumers/{id}");
            if (results != null)
            {
                return results;
            }
            throw new Exception("Costumer not found");
        }

        public async Task CreateCostumer(Costumer costumer)
        {
            var result = await _http.PostAsJsonAsync("api/costumers", costumer);
            await SetCostumers(result);
        }

        private async Task SetCostumers(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Costumer>>();
            if (response != null)
            {
                Costumers = response;
                _navigationManager.NavigateTo("costumers");
            }
            else
            {
                throw new Exception("Costumer not set");
            }            
        }

        public async Task UpdateCostumer(Costumer costumer)
        {
            var result = await _http.PutAsJsonAsync($"api/costumers/{costumer.Id}", costumer);
            await SetCostumers(result);
        }

        public async Task DeleteCostumer(int id)
        {
            var result = await _http.DeleteAsync($"api/costumers/{id}");
            await SetCostumers(result);
        }
    }
}
