﻿using KakeysBakery.Data;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Testcontainers.PostgreSql;


namespace KakeysBakeryTests;

public class BakeryFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    //private string testConnection = "host=localhost;port=5432; database=ourTestingbase;password=Strong_password_123!;username=username";


    public BakeryFactory()
    {
        var whereAmI = Environment.CurrentDirectory;

        var backupFile = Directory.GetFiles("../../..", "*.sql", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f))
            .OrderByDescending(fi => fi.LastWriteTime)
            .First();

        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithPassword("Strong_password_123!")
            .WithUsername("username")
            .WithDatabase("ourTestingbase")
            //.WithPortBinding(5432)
            .WithBindMount(backupFile.FullName, "/docker-entrypoint-initdb.d/init.sql")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<KakeysSharedLib.Data.PostgresContext>));
            //services.AddDbContextFactory<PostgresContext>(options => options.UseNpgsql(testConnection));
            services.AddDbContextFactory<KakeysSharedLib.Data.PostgresContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}