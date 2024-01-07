namespace MathZ.Services.StatisticsAPI.Controllers;

using MathZ.Services.StatisticsAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "admin, statistics")]
[Route(@"api/statistics/[controller]")]
public class OthersAnalysisController(IOthersAnalysisService othersAnalysisService)
    : ControllerBase
{
    private readonly IOthersAnalysisService _othersAnalysisService = othersAnalysisService;

    /// <summary>
    /// Evaluates the geometric mean. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The data to calculate the geometric mean of.</param>
    /// <returns>The geometric mean of the sample.</returns>
    [HttpPost("GeometricMean")]
    public IActionResult GeometricMean([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_othersAnalysisService.GeometricMean(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Evaluates the harmonic mean. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The data to calculate the harmonic mean of.</param>
    /// <returns>The harmonic mean of the sample.</returns>
    [HttpPost("HarmonicMean")]
    public IActionResult HarmonicMean([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_othersAnalysisService.HarmonicMean(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Evaluates the root mean square (RMS) also known as quadratic mean. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The data to calculate the RMS of.</param>
    /// <returns>The root mean square (RMS) of the sample.</returns>
    [HttpPost("RootMeanSquare")]
    public IActionResult RootMeanSquare([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_othersAnalysisService.RootMeanSquare(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Average Gain or Gain Mean. This is a simple average (arithmetic mean) of the periods with a gain.
    /// It is calculated by summing the returns for gain periods and then dividing the total by the number of gain periods.
    /// </summary>
    /// <param name="data">The data representing gains and losses.</param>
    /// <returns>The average gain or gain mean.</returns>
    [HttpPost("GainMean")]
    public IActionResult GainMean([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_othersAnalysisService.GainMean(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Average Loss or LossMean. This is a simple average (arithmetic mean) of the periods with a loss.
    /// It is calculated by summing the returns for loss periods and then dividing the total by the number of loss periods.
    /// </summary>
    /// <param name="data">The data representing gains and losses.</param>
    /// <returns>The average loss or loss mean.</returns>
    [HttpPost("LossMean")]
    public IActionResult LossMean([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_othersAnalysisService.LossMean(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculation is similar to Standard Deviation, except it calculates an average (mean) return only for periods with a gain.
    /// It measures the variation of only the gain periods around the gain mean, indicating the volatility of upside performance.
    /// © Copyright 1996, 1999 Gary L. Gastineau. First Edition. © 1992 Swiss Bank Corporation.
    /// </summary>
    /// <param name="data">The data representing gains and losses.</param>
    /// <returns>The standard deviation of gains.</returns>
    [HttpPost("GainStandardDeviation")]
    public IActionResult GainStandardDeviation([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_othersAnalysisService.GainStandardDeviation(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Similar to standard deviation, except this statistic calculates an average (mean) return for only the periods with a loss.
    /// It measures the variation of only the losing periods around this loss mean, indicating the volatility of downside performance.
    /// </summary>
    /// <param name="data">The data representing gains and losses.</param>
    /// <returns>The standard deviation of losses.</returns>
    [HttpPost("LossStandardDeviation")]
    public IActionResult LossStandardDeviation([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_othersAnalysisService.LossStandardDeviation(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Returns the order statistic (order 1..N) from the provided samples.
    /// </summary>
    /// <param name="data">The data sample sequence.</param>
    /// <param name="order">One-based order of the statistic, must be between 1 and N (inclusive).</param>
    /// <returns>The order statistic at the specified order.</returns>
    [HttpPost("OrderStatistic/{order:int}")]
    public IActionResult OrderStatistic([FromBody] IEnumerable<double> data, [FromRoute] int order)
    {
        try
        {
            return Ok(_othersAnalysisService.OrderStatistic(data, order));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the p-Percentile value from the provided samples.
    /// If a non-integer Percentile is needed, use Quantile instead.
    /// Approximately median-unbiased regardless of the sample distribution (R8).
    /// </summary>
    /// <param name="data">The data sample sequence.</param>
    /// <param name="p">Percentile selector, between 0 and 100 (inclusive).</param>
    /// <returns>The estimated p-Percentile value from the samples.</returns>
    [HttpPost("Percentile/{p:int}")]
    public IActionResult Percentile([FromBody] IEnumerable<double> data, [FromRoute] int p)
    {
        try
        {
            return Ok(_othersAnalysisService.Percentile(data, p));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}