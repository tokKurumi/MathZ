namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Continuous Uniform distribution.
/// </summary>
/// <param name="Lower">Lower bound. Range: lower ≤ upper.</param>
/// <param name="Upper">Upper bound. Range: lower ≤ upper.</param>
public record ContinuousUniformParams(double Lower, double Upper);