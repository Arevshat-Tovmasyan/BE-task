using BE_task.Interfaces;

namespace BE_task.Entities
{
    public class Image
    {
        public string Name { get; set; }
        private readonly List<IImageOperation> _operations = new();

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
