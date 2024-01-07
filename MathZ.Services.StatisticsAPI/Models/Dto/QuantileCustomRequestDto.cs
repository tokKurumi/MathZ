namespace MathZ.Services.StatisticsAPI.Models.Dto;

using System.ComponentModel;
using MathNet.Numerics.Statistics;

/// <summary>
/// Estimates the tau-th quantile from the provided samples using a custom quantile definition.
/// </summary>
/// <param name="Data">The enumerable collection of double values representing the data sample sequence for quantile estimation.</param>
/// <param name="Tau">Quantile selector, a value between 0.0 and 1.0 (inclusive).</param>
/// <param name="Definition">Quantile definition, specifying the method for calculating the quantile to be consistent with an existing system.</param>
[DisplayName("QuantileCustomRequest")]
public record QuantileCustomRequestDto(
    IEnumerable<double> Data,
    double Tau,
    QuantileDefinition Definition);