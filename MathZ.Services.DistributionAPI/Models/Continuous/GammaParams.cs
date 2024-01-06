namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Gamma distribution.
/// </summary>
/// <param name="Shape">The shape (k, α) of the Gamma distribution. Range: α ≥ 0.</param>
/// <param name="Rate">The rate or inverse scale (β) of the Gamma distribution. Range: β ≥ 0.</param>
public record GammaParams(double Shape, double Rate);