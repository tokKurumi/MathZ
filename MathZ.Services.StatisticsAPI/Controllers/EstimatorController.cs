namespace MathZ.Services.StatisticsAPI.Controllers;

using MathZ.Services.StatisticsAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "admin, statistics")]
[Route(@"api/statistics/[controller]")]
public class EstimatorController(IEstimatorService estimatorService)
    : ControllerBase
{
    private readonly IEstimatorService _estimatorService = estimatorService;

    /// <summary>
    /// Estimates the sample mean and the unbiased population variance from the provided samples. On a dataset of size N, it will use an N-1 normalizer (Bessel's correction). Returns NaN for mean if data is empty or if any entry is NaN and NaN for variance if data has less than two entries or if any entry is NaN.
    /// </summary>
    /// <param name="data">The data to calculate the mean and variance of.</param>
    /// <returns>The mean of the sample and the unbiased population variance.</returns>
    [HttpPost("MeanVariance")]
    public IActionResult MeanVariance([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_estimatorService.MeanVariance(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the sample mean and the unbiased population standard deviation from the provided samples. On a dataset of size N, it will use an N-1 normalizer (Bessel's correction). Returns NaN for mean if data is empty or if any entry is NaN and NaN for standard deviation if data has less than two entries or if any entry is NaN.
    /// </summary>
    /// <param name="data">The data to calculate the mean and standard deviation of.</param>
    /// <returns>The mean of the sample and the unbiased population standard deviation.</returns>
    [HttpPost("MeanStandardDeviation")]
    public IActionResult MeanStandardDeviation([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_estimatorService.MeanStandardDeviation(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the unbiased population skewness and kurtosis from the provided samples in a single pass. Uses a normalizer (Bessel's correction; type 2).
    /// </summary>
    /// <param name="data">The data to calculate the skewness and kurtosis of.</param>
    /// <returns>The unbiased population skewness and kurtosis.</returns>
    [HttpPost("SkewnessKurtosis")]
    public IActionResult SkewnessKurtosis([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_estimatorService.SkewnessKurtosis(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Evaluates the skewness and kurtosis from the full population. Does not use a normalizer and would thus be biased if applied to a subset (type 1).
    /// </summary>
    /// <param name="data">The full population data to calculate the skewness and kurtosis of.</param>
    /// <returns>The skewness and kurtosis from the full population.</returns>
    [HttpPost("PopulationSkewnessKurtosis")]
    public IActionResult PopulationSkewnessKurtosis([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_estimatorService.PopulationSkewnessKurtosis(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}