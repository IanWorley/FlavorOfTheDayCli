﻿using Business;
using CliFx;
using Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Culvers_cli;

public static class Program
{

    public static async Task<int>  Main(string[] args) => 
        await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .SetExecutableName("FlavourOfTheDay.CLI")
            .UseTypeActivator(commandTypes =>
            {
                var services = new ServiceCollection();
                services.AddBusinessServices();
                foreach (var commandType in commandTypes)
                    services.AddTransient(commandType);

                return services.BuildServiceProvider();
            })
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
}


