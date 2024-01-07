namespace MathZ.Services.StatisticsAPI.Models.Dto;

using System.ComponentModel;
using MathNet.Numerics.Statistics;

/// <summary>
/// Estimates the quantile tau from the provided samples using a specified rank definition.
/// </summary>
/// <param name="Data">The enumerable collection of double values representing the data sample sequence for quantile estimation.</param>
/// <param name="X">Quantile value.</param>
/// <param name="Definition">Rank definition, specifying how ties should be handled and what product/definition it should be consistent with.</param>
[DisplayName("QuantileRankRequest")]
public record QuantileRankRequestDto(
    IEnumerable<double> Data,
    double X,
    RankDefinition Definition);