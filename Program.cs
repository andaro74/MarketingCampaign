using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json.Serialization;

namespace MarketingCampaign
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapOpenApi("/openapi/{documentName}.yaml");
            }

            Sweepstakes[] sampleSweepstakes =
            [
                new(1, "VID-PIX-DELUXE", "Pixel Quest Deluxe", "Win a digital copy of Pixel Quest Deluxe.", DateOnly.FromDateTime(DateTime.Now.AddDays(14))),
                new(2, "VID-NEON-ULT", "Neon Drift Ultimate", "Enter for a chance to receive Neon Drift Ultimate.", DateOnly.FromDateTime(DateTime.Now.AddDays(21))),
                new(3, "VID-SKY-LEGENDS", "Skyforge Legends", "Win Skyforge Legends and bonus DLC.", DateOnly.FromDateTime(DateTime.Now.AddDays(28))),
                new(4, "VID-STELLAR-ARENA", "Stellar Tactics Arena", "Claim a digital code for Stellar Tactics Arena.", DateOnly.FromDateTime(DateTime.Now.AddDays(35)))
            ];

            var sweepstakesApi = app.MapGroup("/sweepstakes");
            sweepstakesApi.MapGet("/", () => sampleSweepstakes)
                .WithName("GetSweepstakes");

            sweepstakesApi.MapGet("/{sku}", Results<Ok<Sweepstakes>, NotFound> (string sku) =>
                sampleSweepstakes.FirstOrDefault(sweepstake =>
                    string.Equals(sweepstake.Sku, sku, StringComparison.OrdinalIgnoreCase)) is { } sweepstake
                    ? TypedResults.Ok(sweepstake)
                    : TypedResults.NotFound())
                .WithName("GetSweepstakesBySku");

            app.Run();
        }
    }

    public record Sweepstakes(int Id, string Sku, string Title, string Description, DateOnly EndsOn);

    [JsonSerializable(typeof(Sweepstakes[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
