namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Geometric distribution.
/// </summary>
/// <param name="P">The probability (p) of generating one. Range: 0 ≤ p ≤ 1.</param>
public record GeometricParams(double P);