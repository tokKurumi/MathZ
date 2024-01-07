namespace MathZ.Services.StatisticsAPI.Controllers;

using MathZ.Services.StatisticsAPI.Models.Dto;
using MathZ.Services.StatisticsAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "admin, statistics")]
[Route(@"api/statistics/[controller]")]
public class PopulationAnalysisController(IPopulationAnalysisService populationAnalysisService)
    : ControllerBase
{
    private readonly IPopulationAnalysisService _populationAnalysisService = populationAnalysisService;

    /// <summary>
    /// Evaluates the population covariance from the provided full populations.
    /// On a dataset of size N, it will use an N normalizer and would thus be biased if applied
    /// to a subset. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="covarianceRequest">A data transfer object containing two subsets of samples for covariance estimation.</param>
    /// <returns>The estimated population covariance between the two full populations.</returns>
    [HttpPost("Covariance")]
    public IActionResult Covariance([FromBody] CovarianceRequestDto covarianceRequest)
    {
        try
        {
            return Ok(_populationAnalysisService.Covariance(covarianceRequest.Samples1, covarianceRequest.Samples2));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Performs a full analysis on the provided data using the population analysis service.
    /// </summary>
    /// <param name="data">The enumerable collection of double values representing the data for analysis.</param>
    /// <returns>
    /// An IActionResult containing the result of the full analysis.
    /// If the analysis is successful, it returns an Ok result with the analysis outcome.
    /// If an exception occurs during the analysis, it returns a BadRequest result with the exception message.
    /// </returns>
    [HttpPost("FullAnalysis")]
    public IActionResult FullAnalysis([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_populationAnalysisService.FullAnalysis(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the sample mean, providing an estimate of the population mean, from the provided data using the population analysis service.
    /// </summary>
    /// <param name="data">The enumerable collection of double values representing the data for mean calculation.</param>
    /// <returns>
    /// An IActionResult containing the result of the mean calculation.
    /// If the calculation is successful, it returns an Ok result with the mean value.
    /// If the provided data is empty or contains any NaN entries, it returns NaN.
    /// If an exception occurs during the calculation, it returns a BadRequest result with the exception message.
    /// </returns>
    [HttpPost("Mean")]
    public IActionResult Mean([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_populationAnalysisService.Mean(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the population variance from the provided full population data using the population analysis service.
    /// </summary>
    /// <param name="data">The enumerable collection of double values representing the full population data for variance calculation.</param>
    /// <returns>
    /// An IActionResult containing the result of the population variance calculation.
    /// If the calculation is successful, it returns an Ok result with the population variance value.
    /// If the provided population data is empty or contains any NaN entries, it returns NaN.
    /// If an exception occurs during the calculation, it returns a BadRequest result with the exception message.
    /// </returns>
    [HttpPost("PopulationVariance")]
    public IActionResult PopulationVariance([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_populationAnalysisService.PopulationVariance(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the population standard deviation from the provided full population data using the population analysis service.
    /// </summary>
    /// <param name="data">The enumerable collection of double values representing the full population data for standard deviation calculation.</param>
    /// <returns>
    /// An IActionResult containing the result of the population standard deviation calculation.
    /// If the calculation is successful, it returns an Ok result with the population standard deviation value.
    /// If the provided population data is empty or contains any NaN entries, it returns NaN.
    /// If an exception occurs during the calculation, it returns a BadRequest result with the exception message.
    /// </returns>
    [HttpPost("PopulationStandardDeviation")]
    public IActionResult PopulationStandardDeviation([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_populationAnalysisService.PopulationStandardDeviation(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the population skewness from the provided full population data using the population analysis service.
    /// </summary>
    /// <param name="data">The enumerable collection of double values representing the full population data for skewness calculation.</param>
    /// <returns>
    /// An IActionResult containing the result of the population skewness calculation.
    /// If the calculation is successful, it returns an Ok result with the population skewness value.
    /// If the provided population data has less than two entries or contains any NaN entries, it returns NaN.
    /// If an exception occurs during the calculation, it returns a BadRequest result with the exception message.
    /// </returns>
    [HttpPost("PopulationSkewness")]
    public IActionResult PopulationSkewness([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_populationAnalysisService.PopulationSkewness(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Calculates the population kurtosis from the provided full population data using the population analysis service.
    /// </summary>
    /// <param name="data">The enumerable collection of double values representing the full population data for kurtosis calculation.</param>
    /// <returns>
    /// An IActionResult containing the result of the population kurtosis calculation.
    /// If the calculation is successful, it returns an Ok result with the population kurtosis value.
    /// If the provided population data has less than three entries or contains any NaN entries, it returns NaN.
    /// If an exception occurs during the calculation, it returns a BadRequest result with the exception message.
    /// </returns>
    [HttpPost("PopulationKurtosis")]
    public IActionResult PopulationKurtosis([FromBody] IEnumerable<double> data)
    {
        try
        {
            return Ok(_populationAnalysisService.PopulationKurtosis(data));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}