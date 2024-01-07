namespace MathZ.Services.StatisticsAPI.Controllers;

using MathZ.Services.StatisticsAPI.Models.Dto;
using MathZ.Services.StatisticsAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

public class QuantileController(IQuantileService quantileService)
    : ControllerBase
{
    private readonly IQuantileService _quantileService = quantileService;

    /// <summary>
    /// Performs a full analysis on the provided data, calculating descriptive statistics including minimum, lower quantile (25th percentile), median, upper quantile (75th percentile), and maximum.
    /// </summary>
    /// <param name="data">The enumerable collection of double values representing the dataset for analysis.</param>
    /// <returns>
    /// An IActionResult containing the calculated descriptive statistics including minimum, lower quantile, median, upper quantile, and maximum.
    /// </returns>
    [HttpPost("FullAnalysis")]
    public IActionResult FullAnalysis([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_quantileService.FullAnalysis(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the tau-th quantile from the provided samples. The tau-th quantile is the data value where the cumulative distribution function crosses tau. Approximately median-unbiased regardless of the sample distribution (R8).
    /// </summary>
    /// <param name="data">The enumerable collection of double values representing the dataset for quantile estimation.</param>
    /// <param name="tau">Quantile selector, between 0.0 and 1.0 (inclusive).</param>
    /// <returns>
    /// An IActionResult containing the estimated tau-th quantile from the provided samples.
    /// </returns>
    [HttpPost("Quantile/{tau:double}")]
    public IActionResult Quantile([FromBody] IEnumerable<double> data, [FromRoute] double tau)
    {
        try
        {
            return Ok(_quantileService.Quantile(data, tau));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the tau-th quantile from the provided samples. The tau-th quantile is the data value where the cumulative distribution function crosses tau. The quantile definition can be specified to be compatible with an existing system.
    /// </summary>
    /// <param name="quantileCustomRequest">The QuantileCustomRequestDto containing the data, tau, and quantile definition.</param>
    /// <returns>
    /// An IActionResult containing the estimated tau-th quantile from the provided samples using the specified quantile definition.
    /// </returns>
    [HttpPost("QuantileCustom")]
    public IActionResult QuantileCustom([FromBody] QuantileCustomRequestDto quantileCustomRequest)
    {
        try
        {
            return Ok(_quantileService.QuantileCustom(
                quantileCustomRequest.Data,
                quantileCustomRequest.Tau,
                quantileCustomRequest.Definition));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the quantile tau from the provided samples. The tau-th quantile is the data value where the cumulative distribution function crosses tau. The quantile definition can be specified to be compatible with an existing system.
    /// </summary>
    /// <param name="quantileRankRequest">The QuantileRankRequestDto containing the data, quantile value, and rank definition.</param>
    /// <returns>
    /// An IActionResult containing the estimated quantile value from the provided samples using the specified rank definition.
    /// </returns>
    [HttpPost("QuantileRank")]
    public IActionResult QuantileRank([FromBody] QuantileRankRequestDto quantileRankRequest)
    {
        try
        {
            return Ok(_quantileService.QuantileRank(
                quantileRankRequest.Data,
                quantileRankRequest.X,
                quantileRankRequest.Definition));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}