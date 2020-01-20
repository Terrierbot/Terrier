﻿using Discord.Commands;
using McMaster.NETCore.Plugins;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Terrier.Services
{
    public class PluginManager
    {
        public IReadOnlyList<TerrierPlugin> Plugins => _plugins;
        public IReadOnlyList<ITerrierService> Services => _services;

        private readonly List<TerrierPlugin> _plugins;
        private readonly List<ITerrierService> _services;

        public PluginManager()
        {
            _plugins = new List<TerrierPlugin>();
            _services = new List<ITerrierService>();
        }

        internal void LoadPlugins(IServiceCollection services, CommandService commandService)
        {
            var loaders = new List<PluginLoader>();

            if (!Directory.Exists(TerrierConstants.PluginsDirectory))
                Directory.CreateDirectory(TerrierConstants.PluginsDirectory);

            foreach (var dir in Directory.GetDirectories(TerrierConstants.PluginsDirectory))
            {
                var dirName = Path.GetFileName(dir);
                var pluginDll = Path.Combine(dir, dirName + ".dll");
                if (File.Exists(pluginDll))
                {
                    var loader = PluginLoader.CreateFromAssemblyFile(pluginDll, TerrierConstants.SharedTypes);
                    loaders.Add(loader);
                }
            }

            var loadedPlugins = new List<TerrierPlugin>();
            foreach (var loader in loaders)
            {
                var assembly = loader.LoadDefaultAssembly();
                foreach (var pluginType in assembly.GetTypes()
                    .Where(t => typeof(TerrierPlugin).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    var plugin = (TerrierPlugin)Activator.CreateInstance(pluginType);
                    _plugins.Add(plugin);
                    plugin.OnEnable();
                    plugin.ConfigureServices(services);

                    commandService.AddModulesAsync(assembly, services.BuildServiceProvider()).GetAwaiter().GetResult();

                    loadedPlugins.Add(plugin);
                    Console.WriteLine($"Enabled `{plugin.Name}`.");
                }
            }
        }
    }
}
