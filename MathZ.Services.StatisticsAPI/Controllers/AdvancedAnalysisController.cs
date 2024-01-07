namespace MathZ.Services.StatisticsAPI.Controllers;

using System.Numerics;
using MathNet.Numerics.Statistics;
using MathZ.Services.StatisticsAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "admin, statistics")]
[Route(@"api/statistics/[controller]")]
public class AdvancedAnalysisController(IAdvancedAnalysisService advancedAnalysisService)
    : ControllerBase
{
    private readonly IAdvancedAnalysisService _advancedAnalysisService = advancedAnalysisService;

    /// <summary>
    /// Calculates the empirical cumulative distribution function (CDF) at a specific value from the provided data sample.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <param name="x">The value at which to estimate the CDF.</param>
    /// <returns>The estimated CDF value at the given x.</returns>
    [HttpPost("EmpiricalCDF/{x:double}")]
    public IActionResult EmpiricalCDF([FromBody] IEnumerable<double> data, [FromRoute] double x)
    {
        try
        {
            return Ok(_advancedAnalysisService.EmpiricalCDF(data, x));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the empirical inverse cumulative distribution function (InvCDF) at a specific quantile from the provided data sample.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <param name="tau">Quantile selector, between 0.0 and 1.0 (inclusive).</param>
    /// <returns>The estimated inverse CDF value at the given quantile tau.</returns>
    [HttpPost("EmpiricalInvCDF/{tau:double}")]
    public IActionResult EmpiricalInvCDF([FromBody] IEnumerable<double> data, [FromRoute] double tau)
    {
        try
        {
            return Ok(_advancedAnalysisService.EmpiricalInvCDF(data, tau));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves the maximum magnitude and phase value from the provided complex number data.
    /// </summary>
    /// <param name="data">The sequence of complex numbers.</param>
    /// <returns>The maximum magnitude and phase value in the provided complex number data.</returns>
    [HttpPost("MaximumMagnitudePhase")]
    public IActionResult MaximumMagnitudePhase([FromBody] IEnumerable<Complex> data)
    {
        try
        {
            return Ok(_advancedAnalysisService.MaximumMagnitudePhase(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves the minimum magnitude and phase value from the provided complex number data.
    /// </summary>
    /// <param name="data">The sequence of complex numbers.</param>
    /// <returns>The minimum magnitude and phase value in the provided complex number data.</returns>
    [HttpPost("MinimumMagnitudePhase")]
    public IActionResult MinimumMagnitudePhase([FromBody] IEnumerable<Complex> data)
    {
        try
        {
            return Ok(_advancedAnalysisService.MinimumMagnitudePhase(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the moving average over a specified window size for a sequence of samples.
    /// </summary>
    /// <param name="samples">The sequence of samples.</param>
    /// <param name="windowSize">The number of last samples to consider for the moving average.</param>
    /// <returns>The sequence of moving average values.</returns>
    [HttpPost("MovingAverage/{windowSize:int}")]
    public IActionResult MovingAverage([FromBody] IEnumerable<double> samples, [FromRoute] int windowSize)
    {
        try
        {
            return Ok(_advancedAnalysisService.MovingAverage(samples, windowSize));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the rank of each entry in the provided data samples. The rank definition can be specified to be compatible with an existing system.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <param name="definition">Rank definition, specifying how ties should be handled and what product/definition it should be consistent with.</param>
    /// <returns>An array containing the rank of each entry in the data samples.</returns>
    [HttpPost("Ranks")]
    public IActionResult Ranks([FromBody] IEnumerable<double> data, [FromRoute] RankDefinition definition)
    {
        try
        {
            return Ok(_advancedAnalysisService.Ranks(data, definition));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the inter-quartile range from the provided data samples. Approximately median-unbiased regardless of the sample distribution (R8).
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The estimated inter-quartile range of the provided data samples.</returns>
    [HttpPost("InterquartileRange")]
    public IActionResult InterquartileRange([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_advancedAnalysisService.InterquartileRange(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the first quartile value from the provided data samples. Approximately median-unbiased regardless of the sample distribution (R8).
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The estimated lower quartile value of the provided data samples.</returns>
    [HttpPost("LowerQuartile")]
    public IActionResult LowerQuartile([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_advancedAnalysisService.LowerQuartile(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Estimates the third quartile value from the provided data samples. Approximately median-unbiased regardless of the sample distribution (R8).
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The estimated upper quartile value of the provided data samples.</returns>
    [HttpPost("UpperQuartile")]
    public IActionResult UpperQuartile([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_advancedAnalysisService.UpperQuartile(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}