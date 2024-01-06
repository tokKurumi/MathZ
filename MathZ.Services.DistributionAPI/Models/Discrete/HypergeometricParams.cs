namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Hypergeometric distribution.
/// </summary>
/// <param name="Population">The size of the population (N).</param>
/// <param name="Success">The number of successes within the population (K, M).</param>
/// <param name="Draws">The number of draws without replacement (n).</param>
public record HypergeometricParams(int Population, int Success, int Draws);