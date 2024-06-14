using BE_task.DTOs;
using BE_task.Entities;
using BE_task.Exceptions;
using BE_task.Interfaces;
using BE_task.Operations;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
        /// Resize image
        /// </summary>
        /// <param name="size">size</param>
        [HttpPut("Resize")]
        public ActionResult<UpdateImageDTO> ResizeImage(int size)
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

        /// <summary>
        /// Blur image
        /// </summary>
        /// <param name="blurAmount">blurAmount</param>
        [HttpPut("Blur")]
        public ActionResult<UpdateImageDTO> BlurImage(int blurAmount)
        {
            var op = new BlurOperation(blurAmount, _logger);

            _pluginManager.LoadPlugin(op);

            var imageName = "Image_1";
            var image = new Image(imageName);

            var configManager = new Services.ConfigurationManager();
            configManager.ConfigureImage(imageName, new List<string> { op.Name });

            Services.ConfigurationManager.ApplyConfiguration(image, _pluginManager, configManager.GetImageConfiguration(imageName));

            image.ApplyOperations();

            return Ok($"Applying blur of {blurAmount} pixels to {imageName}.");
        }


        /// <summary>
        /// Update images
        /// </summary>
        [HttpPut("Bulk")]
        public ActionResult<List<string>> BulkUpdate([FromBody] List<UpdateImageDTO> body)
        {
            var existingPlugins = _pluginManager.GetAvailablePlugins();

            var givenPlugins = body.Select(x => x.PluginName).ToList();

            var unexistingPlugins = givenPlugins.Where(p => !existingPlugins.Contains(p));

            if (unexistingPlugins.Any())
            {
                throw new MyException($"Plugin(s) with name(s) ({string.Join(',', unexistingPlugins)}) does not exist", ErrorCode.Validation);
            }

            var result = new List<string>();

            var blurList = body.Where(x => string.Equals(x.PluginName, "blur", StringComparison.OrdinalIgnoreCase))
                .Select(x => $"Applying blur of {x.Value} pixels to {x.ImageName}");

            
            var resizeList = body.Where(x => string.Equals(x.PluginName, "resize", StringComparison.OrdinalIgnoreCase))
                .Select(x => $"Resizing {x.ImageName} to {x.Value} pixels.");

            result.AddRange(blurList);
            result.AddRange(resizeList);

            return Ok(result);
        }
    }
}
