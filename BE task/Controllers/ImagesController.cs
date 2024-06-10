using BE_task.DTOs;
using BE_task.Entities;
using BE_task.Interfaces;
using BE_task.Operations;
using Microsoft.AspNetCore.Mvc;


namespace BE_task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IPluginManager _pluginManager;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IPluginManager pluginManager, ILogger<ImagesController> logger)
        {
            _pluginManager = pluginManager;
            _logger = logger;
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
        /// Create custom index
        /// </summary>
        [HttpPut("Resize")]
        public ActionResult<ResizePluginDTO> UploadPlugin(int size)
        {
            var op = new ResizeOperation(size, _logger);

            _pluginManager.LoadPlugin(op);

            var imageName = "Image_1";
            var image = new Image(imageName);

            var configManager = new Services.ConfigurationManager();
            configManager.ConfigureImage(imageName, new List<string> { op.Name });

            Services.ConfigurationManager.ApplyConfiguration(image, _pluginManager, configManager.GetImageConfiguration(imageName));

            image.ApplyOperations();

            return Ok($"Resizing {imageName} to {size} pixels.");
        }

    }
}
