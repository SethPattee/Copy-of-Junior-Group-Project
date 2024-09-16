using AutoShopAppLibrary.Components;
using AutoShopAppLibrary.Data;

using Docker.DotNet.Models;

using DotNet.Testcontainers.Builders;

using FluentAssertions.Common;
using FluentAssertions.Equivalency;

using iText.Kernel.XMP.Options;

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
//using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
//using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

using Testcontainers.PostgreSql;
//using FluentAssertions.Specialized;
//using iText.Kernel.Colors.Gradients;
//using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
//using Org.BouncyCastle.Crypto;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Internal;
//using Microsoft.Extensions.Configuration;
//using System.Configuration;
//using System.Data.Entity.Core.Common;
//using System.Runtime.InteropServices;
//using System.ComponentModel;
//using System.Data.Common;
//using System.Diagnostics;
//using Microsoft.AspNetCore.Connections;

namespace AutoShopTDDSuite;

public class WebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer dbContainer;
    public WebAppFactory()
    {
        var currentLocation = Environment.CurrentDirectory;
        var backupFile = Directory.GetFiles("../../../", "*.sql", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f))
            .OrderByDescending(fi => fi.LastWriteTime)
            .FirstOrDefault();
        dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithPassword("password")
            .WithBindMount(backupFile.FullName, "/docker-entrypoint-initdb.d/init.sql")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<PostgresContext>));
            services.AddDbContextFactory<PostgresContext>(Options => Options.UseNpgsql(dbContainer.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
    {
        await dbContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await dbContainer.StopAsync();
    }
}