namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Zipf distribution.
/// </summary>
/// <param name="S">The s parameter of the distribution.</param>
/// <param name="N">The n parameter of the distribution.</param>
public record ZipfParams(double S, int N);