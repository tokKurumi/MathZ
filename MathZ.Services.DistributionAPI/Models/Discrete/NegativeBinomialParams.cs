namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Negative Binomial distribution.
/// </summary>
/// <param name="R">The number of successes (r) required to stop the experiment. Range: r ≥ 0.</param>
/// <param name="P">The probability (p) of a trial resulting in success. Range: 0 ≤ p ≤ 1.</param>
public record NegativeBinomialParams(double R, double P);