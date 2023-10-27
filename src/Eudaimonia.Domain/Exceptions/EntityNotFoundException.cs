using System.Runtime.Serialization;

namespace Eudaimonia.Domain.Exceptions;

[Serializable]
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, object entityId)
        : base($"{entityName} with id {entityId} not found.")
    {
    }

    protected EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
