using BE_task.Domain.Entities;

namespace BE_task.Domain.Interfaces
{
    public interface IImageOperation
    {
        string Name { get; }
        void Apply(Image image);
    }
}
