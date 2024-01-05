namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Beta distribution.
/// </summary>
/// <param name="A">The α shape parameter of the Beta distribution. Range: α ≥ 0.</param>
/// <param name="B">The β shape parameter of the Beta distribution. Range: β ≥ 0.</param>
public record BetaParams(double A, double B);