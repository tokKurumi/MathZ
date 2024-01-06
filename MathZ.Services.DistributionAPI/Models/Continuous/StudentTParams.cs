namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Student's t-distribution.
/// </summary>
/// <param name="Location">The location (μ) of the distribution.</param>
/// <param name="Scale">The scale (σ) of the distribution. Range: σ > 0.</param>
/// <param name="Freedom">The degrees of freedom (ν) for the distribution. Range: ν > 0.</param>
public record StudentTParams(double Location, double Scale, double Freedom);