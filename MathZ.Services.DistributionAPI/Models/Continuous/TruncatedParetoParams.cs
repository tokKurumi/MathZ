namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Truncated Pareto distribution.
/// </summary>
/// <param name="Scale">The scale (xm) of the distribution. Range: xm > 0.</param>
/// <param name="Shape">The shape (α) of the distribution. Range: α > 0.</param>
/// <param name="Truncation">The truncation (T) of the distribution. Range: T > xm.</param>
public record TruncatedParetoParams(double Scale, double Shape, double Truncation);