namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Cauchy distribution.
/// </summary>
/// <param name="Location">The location (x0) of the distribution.</param>
/// <param name="Scale">The scale (γ) of the distribution. Range: γ > 0.</param>
public record CauchyParams(double Location, double Scale);