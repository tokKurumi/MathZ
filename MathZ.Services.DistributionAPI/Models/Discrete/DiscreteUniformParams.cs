namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Discrete Uniform distribution.
/// </summary>
/// <param name="Lower">Lower bound, inclusive. Range: lower ≤ upper.</param>
/// <param name="Upper">Upper bound, inclusive. Range: lower ≤ upper.</param>
public record DiscreteUniformParams(int Lower, int Upper);