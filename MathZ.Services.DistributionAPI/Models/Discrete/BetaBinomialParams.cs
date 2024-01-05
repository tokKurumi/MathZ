namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a BetaBinomial distribution.
/// </summary>
/// <param name="N">The number of Bernoulli trials (n). n is a positive integer.</param>
/// <param name="A">Shape parameter alpha of the Beta distribution. Range: a > 0.</param>
/// <param name="B">Shape parameter beta of the Beta distribution. Range: b > 0.</param>
public record BetaBinomialParams(int N, double A, double B);