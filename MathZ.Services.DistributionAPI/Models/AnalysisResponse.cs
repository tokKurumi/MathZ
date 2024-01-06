namespace MathZ.Services.DistributionAPI.Models;

/// <summary>
/// Represents the server response for the analysis of a random sample.
/// </summary>
/// <param name="Count">Count of elements in the sample.</param>
/// <param name="Mean">Mean (average) of the sample.</param>
/// <param name="Variance">Variance of the sample.</param>
/// <param name="StandardDeviation">Standard deviation of the sample.</param>
/// <param name="Skewness">Skewness of the sample.</param>
/// <param name="Kurtosis">Kurtosis of the sample.</param>
/// <param name="Maximum">Maximum value in the sample.</param>
/// <param name="Minimum">Minimum value in the sample.</param>
public record AnalysisResponse(
    int Count,
    double Mean,
    double Variance,
    double StandardDeviation,
    double Skewness,
    double Kurtosis,
    double Maximum,
    double Minimum);