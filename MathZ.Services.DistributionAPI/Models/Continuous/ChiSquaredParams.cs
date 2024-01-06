namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a ChiSquared distribution.
/// </summary>
/// <param name="Freedom">The degrees of freedom (k) of the distribution. Range: k > 0.</param>
public record ChiSquaredParams(double Freedom);