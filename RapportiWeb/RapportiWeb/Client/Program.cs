using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RapportiWeb.Client;
using RapportiWeb.Client.Services.Auth;
using RapportiWeb.Client.Services.Clienti;
using RapportiWeb.Client.Services.Pdf;
using RapportiWeb.Client.Services.Rapporti;
using RapportiWeb.Client.Services.Richieste;
using RapportiWeb.Shared;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using RapportiWeb.Client.Services.Users;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddScoped<IClientiService, ClientiService>();
builder.Services.AddScoped<IRichiesteService, RichiesteService>();
builder.Services.AddScoped<IRapportiService, RapportiService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsersServices, UserServices>();


builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateprovider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:443/") });


await builder.Build().RunAsync();
