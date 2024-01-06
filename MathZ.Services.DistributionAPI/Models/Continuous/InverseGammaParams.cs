namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating an Inverse Gamma distribution.
/// </summary>
/// <param name="Shape">The shape (α) of the distribution. Range: α > 0.</param>
/// <param name="Scale">The scale (β) of the distribution. Range: β > 0.</param>
public record InverseGammaParams(double Shape, double Scale);