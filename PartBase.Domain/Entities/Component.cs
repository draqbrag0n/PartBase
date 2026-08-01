    namespace PartBase.Domain.Entities;

    public class Component
    {
        public Guid Id { get; private set; }

        public string PartNumber { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public Guid ManufacturerId { get; private set; }

        public Guid CategoryId { get; private set; }

        public string Package { get; private set; } = string.Empty;

        public string DatasheetUrl { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; private set; }

        private Component() { }

        public Component(
            string partNumber,
            string description,
            Guid manufacturerId,
            Guid categoryId,
            string package,
            string datasheetUrl)
        {
            Id = Guid.NewGuid();
            PartNumber = partNumber;
            Description = description;
            ManufacturerId = manufacturerId;
            CategoryId = categoryId;
            Package = package;
            DatasheetUrl = datasheetUrl;
            CreatedAt = DateTime.UtcNow;
        }
    }