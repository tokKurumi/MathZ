namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Skewed Generalized Error distribution.
/// </summary>
/// <param name="Location">The location (μ) of the distribution.</param>
/// <param name="Scale">The scale (σ) of the distribution. Range: σ > 0.</param>
/// <param name="Skew">The skew, 1 > λ > -1.</param>
/// <param name="P">Parameter that controls kurtosis. Range: p > 0.</param>
public record SkewedGeneralizedErrorParams(double Location, double Scale, double Skew, double P);