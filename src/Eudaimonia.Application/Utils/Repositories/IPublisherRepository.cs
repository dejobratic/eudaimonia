using Eudaimonia.Domain;

namespace Eudaimonia.Application.Utils.Repositories;

public interface IPublisherRepository : ICommandRepository<Publisher, PublisherId>
{
}