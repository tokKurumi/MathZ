namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a LogNormal distribution.
/// </summary>
/// <param name="Mu">The log-scale (μ) of the logarithm of the distribution.</param>
/// <param name="Sigma">The shape (σ) of the logarithm of the distribution. Range: σ ≥ 0.</param>
public record LogNormalParams(double Mu, double Sigma);