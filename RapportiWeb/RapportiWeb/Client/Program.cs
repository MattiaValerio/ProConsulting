using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RapportiWeb.Client;
using RapportiWeb.Client.Services.Clienti;
using RapportiWeb.Client.Services.Rapporti;
using RapportiWeb.Client.Services.Richieste;
using RapportiWeb.Shared;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();


builder.Services.AddScoped<IClientiService, ClientiService>();
builder.Services.AddScoped<IRichiesteService, RichiesteService>();
builder.Services.AddScoped<IRapportiService, RapportiService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
