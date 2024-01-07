namespace MathZ.Services.StatisticsAPI.Models.Dto;

using System.ComponentModel;

/// <summary>
/// Represents a data transfer object for requesting the covariance between two subsets of samples.
/// </summary>
/// <param name="Samples1">A subset 1 of samples, sampled from the full population.</param>
/// <param name="Samples2">A subset 2 of samples, sampled from the full population.</param>
[DisplayName("CovarianceRequest")]
public record CovarianceRequestDto(IEnumerable<double> Samples1, IEnumerable<double> Samples2);