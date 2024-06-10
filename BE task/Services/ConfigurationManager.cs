using BE_task.Entities;
using BE_task.Interfaces;

namespace BE_task.Services
{
    public class ConfigurationManager
    {
        private Dictionary<string, List<string>> imageConfigurations = new();

        public void ConfigureImage(string imageName, List<string> pluginNames)
        {
            imageConfigurations[imageName] = pluginNames;
        }

        public List<string> GetImageConfiguration(string imageName)
        {
            if (imageConfigurations.TryGetValue(imageName, out List<string> value))
            {
                return value;
            }

            return new List<string>();
        }

        public static void ApplyConfiguration(Image image, IPluginManager pluginManager, List<string> pluginNames)
        {
            foreach (var pluginName in pluginNames)
            {
                image.AddOperation(pluginManager.GetPlugin(pluginName));
            }
        }
    }
}
