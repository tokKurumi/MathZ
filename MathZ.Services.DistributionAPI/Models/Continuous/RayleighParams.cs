namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Rayleigh distribution.
/// </summary>
/// <param name="Scale">The scale (σ) of the distribution. Range: σ > 0.</param>
public record RayleighParams(double Scale);