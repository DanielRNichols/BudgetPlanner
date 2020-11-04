using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Services
{
    public class BudgetPlannerDataService<T> : IBudgetPlannerDataService<T> where T : class
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly string _resourceUrl = "";

        public BudgetPlannerDataService(HttpClient httpClient, string resourceUrl)
        {
            _httpClient = httpClient;
            _resourceUrl = resourceUrl;
        }

        public async Task<T> Get(int id, bool includeRelated = false)
        {
            if (id < 1)
                return null;

            try
            {
                return await _httpClient.GetFromJsonAsync<T>($"{_resourceUrl}{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> Get(bool includeRelated = false)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IList<T>>(_resourceUrl);
            }
            catch
            {
                return null;
            }
        }
    }
}
