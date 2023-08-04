
namespace JPMorgan.MessageProcessing.Domain.Core
{
    public abstract class Entity
    {
        protected Guid Id { get; private set; } = Guid.NewGuid();
    }
}
