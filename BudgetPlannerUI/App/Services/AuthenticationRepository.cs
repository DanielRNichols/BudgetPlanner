using Blazored.LocalStorage;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using BudgetPlannerUI.Pages.Users;
using BudgetPlannerUI.Providers;
using BudgetPlannerUI.Static;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Services
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        //private readonly IHttpClientFactory _client;
        protected readonly HttpClient _httpClient;
        private readonly BudgetPlannerUI.Interfaces.ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationRepository(HttpClient httpClient,
            BudgetPlannerUI.Interfaces.ILocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> Login(UserLoginModel user)
        {

            var requstContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(Static.Endpoints.UsersLogin, requstContent);

            if (!response.IsSuccessStatusCode)
                return false;

            // get the token from the response
            var content = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(content);

            // store the token in the localstorage
            await _localStorage.SetItemAsync(LocalStorageKeys.AuthToken, token.Token);

            // change authentication state of app
            var apiAuthStateProvider = _authStateProvider as ApiAuthenticationStateProvider;
            if (apiAuthStateProvider == null)
                return false;

            await apiAuthStateProvider.SetStateToLoggedIn();

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(LocalStorageKeys.AuthToken);
            // change authentication state of app
            ApiAuthenticationStateProvider apiAuthStateProvider = _authStateProvider as ApiAuthenticationStateProvider;
            if (apiAuthStateProvider != null)
                apiAuthStateProvider.SetStateToLoggedOut();
        }

        public async Task<bool> Register(UserRegistrationModel user)
        {
            var requstContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(Endpoints.UsersRegistration, requstContent);

            return response.IsSuccessStatusCode;

        }
    }
}
