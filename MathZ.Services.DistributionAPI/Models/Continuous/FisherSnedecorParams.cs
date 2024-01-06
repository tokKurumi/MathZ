namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Fisher-Snedecor distribution.
/// </summary>
/// <param name="D1">The first degree of freedom (d1) of the distribution. Range: d1 > 0.</param>
/// <param name="D2">The second degree of freedom (d2) of the distribution. Range: d2 > 0.</param>
public record FisherSnedecorParams(double D1, double D2);