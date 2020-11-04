using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Services
{
    public class BudgetPlannerDataService<T> : IBudgetPlannerDataService<T> where T : class
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        protected string ResourceName { get; set; } = "";

        public BudgetPlannerDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44371/api/");
        }

        public async Task<T> Get(int id, bool includeRelated = false)
        {
            if (id < 1)
                return null;

            var response = await _httpClient.GetAsync($"{ResourceName}/{id}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);

            //return await JsonSerializer.DeserializeAsync<BudgetItemType>
            //    (await _httpClient.GetStreamAsync($"api/budgetitemtypes/{id}"),
            //    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<T>> Get(bool includeRelated = false)
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<T>>
                (await _httpClient.GetStreamAsync($"" +
                $"{ResourceName}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
