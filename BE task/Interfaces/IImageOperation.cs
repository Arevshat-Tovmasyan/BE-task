using BE_task.Entities;

namespace BE_task.Interfaces
{
    public interface IImageOperation
    {
        string Name { get; }
        void Apply(Image image);
    }
}
