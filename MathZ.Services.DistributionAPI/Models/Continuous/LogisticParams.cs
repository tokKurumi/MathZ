namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Logistic distribution.
/// </summary>
/// <param name="Mean">The mean (μ) of the logistic distribution.</param>
/// <param name="Scale">The scale (s) of the logistic distribution. Range: s > 0.</param>
public record LogisticParams(double Mean, double Scale);