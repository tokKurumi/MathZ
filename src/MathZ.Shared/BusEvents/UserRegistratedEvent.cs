namespace MathZ.Shared.BusEvents;

using MathZ.Shared.Models;

public record UserRegistratedEvent(
    User User);
