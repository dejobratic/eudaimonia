using Eudaimonia.Application.Utils.Commands;

namespace Eudaimonia.Application.Features.Publishers.DeletePublisher;

public class DeletePublisherCommand : ICommand
{
    public Guid Id { get; set; }
}