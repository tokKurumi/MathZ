namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating an Exponential distribution.
/// </summary>
/// <param name="Rate">The rate (λ) parameter of the distribution. Range: λ ≥ 0.</param>
public record ExponentialParams(double Rate);