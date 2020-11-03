using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Services
{
    public class BudgetPlannerDataService : IBudgetPlannerDataService
    {
        private readonly HttpClient _httpClient;
        public BudgetPlannerDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BudgetItemType> GetBudgetItemType(int id, bool includeRelated = false)
        {
            return await JsonSerializer.DeserializeAsync<BudgetItemType>
                (await _httpClient.GetStreamAsync($"api/budgetitemtypes/{id}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<BudgetItemType>> GetBudgetItemTypes(bool includeRelated = false)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<BudgetItemType>>
                (await _httpClient.GetStreamAsync($"api/budgetitemtypes"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
