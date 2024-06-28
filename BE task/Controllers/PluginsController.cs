using BE_task.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE_task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PluginsController : ControllerBase
    {
        private readonly IPluginManager _pluginManager;

        public PluginsController(IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        /// <summary>
        /// Get all plugins
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IReadOnlyList<string>> Get()
        {
            var plugins = _pluginManager.GetAvailablePlugins();

            return Ok(plugins);
        }

        /// <summary>
        /// Unload plugin
        /// </summary>
        /// <param name="pluginName">pluginName</param>
        [HttpDelete("{pluginName}")]
        public ActionResult UnloadPlugin(string pluginName)
        {
            _pluginManager.UnloadPlugin(pluginName);

            return Ok();
        }

        /// <summary>
        /// Upload plugin
        /// </summary>
        /// <param name="pluginName">pluginName</param>
        [HttpPost("{pluginName}")]
        public ActionResult UploadPlugin(string pluginName)
        {
            _pluginManager.UploadPlugin(pluginName);

            return Ok();
        }
    }
}
