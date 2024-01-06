namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Laplace distribution.
/// </summary>
/// <param name="Location">The location (μ) of the distribution.</param>
/// <param name="Scale">The scale (b) of the distribution. Range: b > 0.</param>
public record LaplaceParams(double Location, double Scale);