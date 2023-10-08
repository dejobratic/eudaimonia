using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eudaimonia.Application.Dtos;

public class AuthorDto : UserDto
{
    private readonly List<Guid> _authoredBookIds = new();
    public IReadOnlyList<Guid> AuthoredBookIds => _authoredBookIds;
}
