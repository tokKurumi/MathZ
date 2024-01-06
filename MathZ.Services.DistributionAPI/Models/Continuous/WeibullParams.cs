namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Weibull distribution.
/// </summary>
/// <param name="Shape">The shape (k) of the distribution. Range: k > 0.</param>
/// <param name="Scale">The scale (λ) of the distribution. Range: λ > 0.</param>
public record WeibullParams(double Shape, double Scale);