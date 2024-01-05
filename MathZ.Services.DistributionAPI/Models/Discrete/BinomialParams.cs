namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Binomial distribution.
/// </summary>
/// <param name="P">The success probability (p) in each trial. Range: 0 ≤ p ≤ 1.</param>
/// <param name="N">The number of trials (n). Range: n ≥ 0.</param>
public record BinomialParams(double P, int N);