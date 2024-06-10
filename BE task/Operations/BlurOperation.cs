using BE_task.Entities;
using BE_task.Interfaces;

namespace BE_task.Operations
{
    public class BlurOperation : IImageOperation
    {
        public string Name => "Blur";

        private readonly int _blurAmount;
        private readonly ILogger _logger;

        public BlurOperation(int blurAmount, ILogger logger)
        {
            _blurAmount = blurAmount;
            _logger = logger;
        }

        public void Apply(Image image)
        {
            _logger.LogInformation("Applying blur of {blurAmount} pixels to {imageName}.", _blurAmount, image.Name);
        }
    }
}
