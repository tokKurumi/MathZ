namespace MathZ.Services.StatisticsAPI.Controllers;

using MathZ.Services.StatisticsAPI.Models.Dto;
using MathZ.Services.StatisticsAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "admin, statistics")]
[Route(@"api/statistics/[controller]")]
public class BasicAnalysisController(IBasicAnalysisService basicAnalysisService)
    : ControllerBase
{
    private readonly IBasicAnalysisService _basicAnalysisService = basicAnalysisService;

    /// <summary>
    /// Estimates the unbiased population covariance from the provided samples. On a dataset of size N, it will use an N-1 normalizer (Bessel's correction). Returns NaN if data has less than two entries or if any entry is NaN.
    /// </summary>
    /// <param name="covarianceRequest">A data transfer object containing two subsets of samples for covariance estimation.</param>
    /// <returns>The estimated covariance between the two sets of samples.</returns>
    [HttpPost("Covariance")]
    public IActionResult Covariance([FromBody] CovarianceRequestDto covarianceRequest)
    {
        try
        {
            return Ok(_basicAnalysisService.Covariance(covarianceRequest.Samples1, covarianceRequest.Samples2));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Performs a full statistical analysis on the provided data using the DescriptiveStatistics class.
    /// </summary>
    /// <param name="data">The sample data.</param>
    /// <returns>An object containing various descriptive statistics of the provided data.</returns>
    [HttpPost("FullAnalysis")]
    public IActionResult FullAnalysis([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.FullAnalysis(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the mean (average) of the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The mean of the provided data.</returns>
    [HttpPost("Mean")]
    public IActionResult Mean([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.Mean(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the variance of the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The variance of the provided data.</returns>
    [HttpPost("Variance")]
    public IActionResult Variance([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.Variance(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the standard deviation of the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The standard deviation of the provided data.</returns>
    [HttpPost("StandardDeviation")]
    public IActionResult StandardDeviation([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.StandardDeviation(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the skewness of the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The skewness of the provided data.</returns>
    [HttpPost("Skewness")]
    public IActionResult Skewness([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.Skewness(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the kurtosis of the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The kurtosis of the provided data.</returns>
    [HttpPost("Kurtosis")]
    public IActionResult Kurtosis([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.Kurtosis(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Finds the maximum value in the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The maximum value in the provided data.</returns>
    [HttpPost("Maximum")]
    public IActionResult Maximum([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.Maximum(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Finds the maximum absolute value in the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The maximum absolute value in the provided data.</returns>
    [HttpPost("MaximumAbsolute")]
    public IActionResult MaximumAbsolute([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.MaximumAbsolute(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the median of the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The median of the provided data.</returns>
    [HttpPost("Median")]
    public IActionResult Median([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.Median(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Finds the minimum value in the provided data.
    /// </summary>
    /// <param name="data">The sequence of data samples.</param>
    /// <returns>The minimum value in the provided data.</returns>
    [HttpPost("Minimum")]
    public IActionResult Minimum([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_basicAnalysisService.Minimum(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}