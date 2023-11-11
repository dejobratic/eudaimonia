namespace Eudaimonia.Domain.Kernel;

public interface IIdGenerator<out T>
{
    T NewId();
}