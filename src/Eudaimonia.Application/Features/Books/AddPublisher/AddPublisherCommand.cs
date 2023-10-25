﻿using Eudaimonia.Application.Utils.Commands;

namespace Eudaimonia.Application.Features.Books.AddPublisher;

public class AddPublisherCommand : ICommand
{
    public string? FullName { get; set; }
    public string? Bio { get; set; }
}