namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a BetaScaled distribution.
/// </summary>
/// <param name="A">The α shape parameter of the BetaScaled distribution. Range: α > 0.</param>
/// <param name="B">The β shape parameter of the BetaScaled distribution. Range: β > 0.</param>
/// <param name="Location">The location (μ) of the distribution.</param>
/// <param name="Scale">The scale (σ) of the distribution. Range: σ > 0.</param>
public record BetaScaledParams(double A, double B, double Location, double Scale);