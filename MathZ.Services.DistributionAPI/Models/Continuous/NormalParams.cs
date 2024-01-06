namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Normal distribution.
/// </summary>
/// <param name="Mean">The mean (μ) of the normal distribution.</param>
/// <param name="Stddev">The standard deviation (σ) of the normal distribution. Range: σ ≥ 0.</param>
public record NormalParams(double Mean, double Stddev);