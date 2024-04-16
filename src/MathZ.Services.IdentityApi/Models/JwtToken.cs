namespace MathZ.Services.IdentityApi.Models;

using System.ComponentModel;

[DisplayName("Token")]
public record JwtToken(
    string Token);
