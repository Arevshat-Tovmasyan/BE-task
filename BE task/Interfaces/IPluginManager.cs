namespace BE_task.Interfaces
{
    public interface IPluginManager
    {
        void LoadPlugin(IImageOperation plugin);
        void UnloadPlugin(string pluginName);
        IImageOperation GetPlugin(string pluginName);
        IReadOnlyCollection<string> GetAvailablePlugins();
    }
}
