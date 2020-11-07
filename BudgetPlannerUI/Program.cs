using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BudgetPlannerUI.Services;
using BudgetPlannerUI.Interfaces;
using Blazored.Toast;
using Blazored.LocalStorage;

namespace BudgetPlannerUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                }
            );

            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddTransient<IBudgetGroupsDataService, BudgetGroupsDataServices>();
            builder.Services.AddTransient<IBudgetCategoriesDataService, BudgetCategoriesDataServices>();
            builder.Services.AddTransient<IBudgetItemsDataService, BudgetItemsDataServices>();


            await builder.Build().RunAsync();
        }
    }
}
