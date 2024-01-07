namespace MathZ.Services.StatisticsAPI.Models;

/// <summary>
/// Represents descriptive statistics for a population.
/// </summary>
/// <param name="Count">Count of elements in the population.</param>
/// <param name="Mean">Mean (average) of the population.</param>
/// <param name="Variance">Variance of the population.</param>
/// <param name="StandardDeviation">Standard deviation of the population.</param>
/// <param name="Skewness">Skewness of the population.</param>
/// <param name="Kurtosis">Kurtosis of the population.</param>
/// <param name="Maximum">Maximum value in the population.</param>
/// <param name="Minimum">Minimum value in the population.</param>
public record DescriptivePopulationStatistics(
    long Count,
    double Mean,
    double Variance,
    double StandardDeviation,
    double Skewness,
    double Kurtosis,
    double Maximum,
    double Minimum);