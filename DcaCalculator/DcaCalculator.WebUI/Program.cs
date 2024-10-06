using DcaCalculator.WebUI.Data;
using DcaCalculator.Application.Extensions;
using DcaCalculator.Persistence.Extensions;
using DcaCalculator.Application.Interfaces;
using DcaCalculator.Persistence.HttpClients;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApplicationLayer();
        builder.Services.AddPersistenceLayer(builder.Configuration);
        builder.Services.AddHttpClient<ICoinmarketCapClient, CoinmarketCapClient>(client =>
        {
            client.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/");
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "d1983d61-5732-4cea-8b39-89819eda8526");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }


        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}