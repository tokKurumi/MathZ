namespace MathZ.Services.StatisticsAPI.Models;

/// <summary>
/// Represents descriptive statistics including minimum, lower quantile, median, upper quantile, and maximum.
/// </summary>
/// <param name="Minimum">The minimum value.</param>
/// <param name="LowerQuantile">The lower quantile (25th percentile).</param>
/// <param name="Median">The median.</param>
/// <param name="UpperQuantile">The upper quantile (75th percentile).</param>
/// <param name="Max">The maximum value.</param>
public record DescriptiveQuantileStatistics(double Minimum, double LowerQuantile, double Median, double UpperQuantile, double Max);