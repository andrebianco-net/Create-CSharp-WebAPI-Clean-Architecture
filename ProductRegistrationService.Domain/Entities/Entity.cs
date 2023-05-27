using ProductRegistrationService.Domain.Validation;

namespace ProductRegistrationService.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        public void ValidateDomain(int id)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");

            Id = id;
        }

    }
}