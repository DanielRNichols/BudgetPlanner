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
                var queryStr = CreateQueryString(includeRelated);
                return await _httpClient.GetFromJsonAsync<T>($"{_resourceUrl}{id}{queryStr}");
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
                var queryStr = CreateQueryString(includeRelated);
                return await _httpClient.GetFromJsonAsync<IList<T>>($"{_resourceUrl}{queryStr}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Create(T entity)
        {
            if (entity == null)
                return false;

            //_httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("bearer", await GetBearerToken());

            var response = await _httpClient.PostAsJsonAsync<T>(_resourceUrl, entity);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                return true;

            return false;

        }

        public async Task<bool> Update(int id, T entity)
        {
            if (entity == null)
                return false;

            //_httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("bearer", await GetBearerToken());

            var response = await _httpClient.PutAsJsonAsync<T>(_resourceUrl + id, entity);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync(_resourceUrl + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        private string CreateQueryString(bool includeRelated)
        {
            return includeRelated ? "?includeRelated=true" : "";
        }

    }
}
