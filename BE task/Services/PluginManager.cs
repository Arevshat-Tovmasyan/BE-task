using BE_task.Exceptions;
using BE_task.Interfaces;

namespace BE_task.Services
{
    public class PluginManager : IPluginManager
    {
        private static readonly Dictionary<string, IImageOperation> _plugins = new();
        private static readonly HashSet<string> _availablePlugins = new();

        public void LoadPlugin(IImageOperation plugin)
        {
            _plugins[plugin.Name] = plugin;

            _availablePlugins.Add(plugin.Name);
        }

        public void UnloadPlugin(string pluginName)
        {
            if (!_availablePlugins.Remove(pluginName))
            {
                throw new MyException($"Plugin with name ({pluginName}) does not exist.", ErrorCode.NotFound);
            }

            _plugins.Remove(pluginName);
        }
        
        public void UploadPlugin(string pluginName)
        {
            if (_availablePlugins.Contains(pluginName))
            {
                throw new MyException($"Plugin with name ({pluginName}) already exists.", ErrorCode.Conflict);
            }

            _availablePlugins.Add(pluginName);
        }

        public IImageOperation GetPlugin(string pluginName)
        {
            if (_plugins.TryGetValue(pluginName, out IImageOperation value))
            {
                return value;
            }

            throw new MyException($"Plugin with name ({pluginName}) does not exist.", ErrorCode.NotFound);
        }

        public HashSet<string> GetAvailablePlugins()
        {
            return _availablePlugins;
;
        }
    }
}
