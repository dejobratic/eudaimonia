using System.Runtime.Serialization;

namespace Eudaimonia.Presentation.Exceptions
{
    [Serializable]
    public class UnsupportedConfigurationException : Exception
    {
        public UnsupportedConfigurationException(string name, string? value)
            : base($"Configuration value '{value ?? "null"}' for {name} is not supported.")
        {
        }

        protected UnsupportedConfigurationException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}