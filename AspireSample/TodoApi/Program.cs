using Marten;
using Microsoft.AspNetCore.Mvc;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("Marten")!);

    // Specify that we want to use STJ as our serializer
    options.UseSystemTextJsonForSerialization();

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
});
// Add service defaults & Aspire components.
builder.AddServiceDefaults();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/user",
    async (CreateUserRequest create, [FromServices] IDocumentStore store) =>
{
    // Open a session for querying, loading, and updating documents
    await using var session = store.LightweightSession();

    var user = new User {
        FirstName = create.FirstName,
        LastName = create.LastName,
        Internal = create.Internal
    };
    session.Store(user);

    await session.SaveChangesAsync();
});

app.MapGet("/users",
    async (bool internalOnly, [FromServices] IDocumentStore store, CancellationToken ct) =>
{
    // Open a session for querying documents only
    await using var session = store.QuerySession();

    return await session.Query<User>()
        .Where(x=> x.Internal == internalOnly)
        .ToListAsync(ct);
});

// OR Inject the session directly to skip the management of the session lifetime
app.MapGet("/user/{id:guid}",
    async (Guid id, [FromServices] IQuerySession session, CancellationToken ct) =>
{
    return await session.LoadAsync<User>(id, ct);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record CreateUserRequest(string FirstName, string LastName, bool Internal);