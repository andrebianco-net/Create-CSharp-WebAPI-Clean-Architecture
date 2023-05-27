using ProductRegistrationService.Domain.Validation;

namespace ProductRegistrationService.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            ValidateDomain(id);
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);

            ModifiedAt = DateTime.Now;
        }

        public ICollection<Product> Products { get; set; }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), 
                "Invalid name. Name is required.");

            DomainExceptionValidation.When(name.Length < 3, 
                "Invalid name, too short, minimum 3 characters.");

            Name = name;
        }
    }
}