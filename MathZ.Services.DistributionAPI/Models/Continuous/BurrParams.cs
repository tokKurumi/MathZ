namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Burr Type XII distribution.
/// </summary>
/// <param name="A">The scale parameter a of the Burr distribution. Range: a > 0.</param>
/// <param name="C">The first shape parameter c of the Burr distribution. Range: c > 0.</param>
/// <param name="K">The second shape parameter k of the Burr distribution. Range: k > 0.</param>
public record BurrParams(double A, double C, double K);