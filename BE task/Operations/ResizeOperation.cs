using BE_task.Entities;
using BE_task.Interfaces;

namespace BE_task.Operations
{
    public class ResizeOperation : IImageOperation
    {
        public string Name => "Resize";

        private readonly int _size;
        private readonly ILogger _logger;

        public ResizeOperation(int size, ILogger logger)
        {
            _size = size;
            _logger = logger;
        }

        public void Apply(Image image)
        {
            _logger.LogInformation("Resizing {imageName} to {size} pixels.", image.Name, _size);
        }
    }
}
