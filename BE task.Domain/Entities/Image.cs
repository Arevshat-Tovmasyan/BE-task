using BE_task.Domain.Interfaces;

namespace BE_task.Domain.Entities
{
    public class Image
    {
        public string Name { get; set; }
        private readonly List<IImageOperation> _operations = new List<IImageOperation>();

        public Image(string name)
        {
            Name = name;
        }

        public void AddOperation(IImageOperation operation)
        {
            _operations.Add(operation);
        }

        public void ApplyOperations()
        {
            foreach (var operation in _operations)
            {
                operation.Apply(this);
            }
        }
    }
}
