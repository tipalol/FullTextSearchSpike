using FullTextSearchSpike;
using FullTextSearchSpike.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDbContext(app);

app.Run();

    
static void MigrateDbContext(IHost app)
{
    using var scope = app.Services.CreateScope();
    var databaseFacade = scope.ServiceProvider.GetRequiredService<TextDbContext>().Database;

    var migrations = databaseFacade.GetPendingMigrations().ToArray();
    if (migrations.Length > 0)
    {
        Console.WriteLine(
            $"Migrate database: {Environment.NewLine}{string.Join(Environment.NewLine, migrations)}");
    }

    databaseFacade.Migrate();
}

public static class ServicesExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<SeedHelper>();
        services.AddMediatR((opt) => 
            { opt.RegisterServicesFromAssembly(typeof(Program).Assembly); });

        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var dbSettings = new DatabaseSettings();
        configuration.Bind(nameof(DatabaseSettings), dbSettings);

        services.AddDbContext<TextDbContext>((provider, options) =>
        {
            options.UseNpgsql(GetConnectionString(dbSettings),
                assembly => assembly.MigrationsAssembly(typeof(TextDbContext).Assembly.FullName));
            options.UseSnakeCaseNamingConvention();
        }, ServiceLifetime.Scoped, ServiceLifetime.Singleton);

        return services;
    }

    static string GetConnectionString(DatabaseSettings dbSettings)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = "localhost",
            Port = 5432,
            Database = "textSearch",
            Username = "postgres",
            Password = "postgres",
            MaxPoolSize = 400
        };

        return connectionStringBuilder.ConnectionString;
    }
}