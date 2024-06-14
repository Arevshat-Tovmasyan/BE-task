namespace BE_task.Interfaces
{
    public interface IPluginManager
    {
        void LoadPlugin(IImageOperation plugin);
        void UploadPlugin(string pluginName);
        void UnloadPlugin(string pluginName);
        IImageOperation GetPlugin(string pluginName);
        HashSet<string> GetAvailablePlugins();
    }
}
