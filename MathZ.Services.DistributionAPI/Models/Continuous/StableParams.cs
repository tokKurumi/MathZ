namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Stable distribution.
/// </summary>
/// <param name="Alpha">The stability (α) of the distribution. Range: 2 ≥ α > 0.</param>
/// <param name="Beta">The skewness (β) of the distribution. Range: 1 ≥ β ≥ -1.</param>
/// <param name="Scale">The scale (c) of the distribution. Range: c > 0.</param>
/// <param name="Location">The location (μ) of the distribution.</param>
public record StableParams(double Alpha, double Beta, double Scale, double Location);