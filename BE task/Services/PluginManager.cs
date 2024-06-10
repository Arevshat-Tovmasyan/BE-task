using BE_task.Interfaces;

namespace BE_task.Services
{
    public class PluginManager : IPluginManager
    {
        private static readonly Dictionary<string, IImageOperation> _plugins = new();

        public void LoadPlugin(IImageOperation plugin)
        {
            _plugins[plugin.Name] = plugin;
        }

        public void UnloadPlugin(string pluginName)
        {
            _plugins.Remove(pluginName);
        }

        public IImageOperation GetPlugin(string pluginName)
        {
            if (_plugins.TryGetValue(pluginName, out IImageOperation value))
            {
                return value;
            }

            throw new KeyNotFoundException($"Plugin {pluginName} not found.");
        }

        public IReadOnlyCollection<string> GetAvailablePlugins()
        {
            return _plugins.Keys;
        }
    }
}
