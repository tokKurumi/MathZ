namespace MathZ.Services.DistributionAPI.Controllers.Continuous;

using AutoMapper;
using MathNet.Numerics.Distributions;
using MathZ.Services.DistributionAPI.Models.Continuous;
using MathZ.Services.DistributionAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/distribution/continuous/[controller]")]
[Authorize(Roles = "admin, distribution")]
public class LogNormalController(
    IContinuousDistributionService distributionService,
    IMapper mapper)
    : ControllerBase
{
    private readonly IContinuousDistributionService _distributionService = distributionService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Computes the cumulative distribution (CDF) of the LogNormal distribution at a given location (x), i.e., P(X ≤ x).
    /// </summary>
    /// <param name="logNormalParams">The parameters for creating a LogNormal distribution.</param>
    /// <param name="x">The location at which to compute the cumulative distribution function.</param>
    /// <returns>The cumulative distribution at the specified location (x).</returns>
    [HttpPost("CDF/{x:double}")]
    public IActionResult CDF([FromBody] LogNormalParams logNormalParams, [FromRoute] double x)
    {
        try
        {
            var distribution = _mapper.Map<LogNormal>(logNormalParams);

            return Ok(_distributionService.CDF(distribution, x));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Computes the probability density of the LogNormal distribution (PDF) at a given location (x), i.e., ∂P(X ≤ x)/∂x.
    /// </summary>
    /// <param name="logNormalParams">The parameters for creating a LogNormal distribution.</param>
    /// <param name="x">The location at which to compute the density.</param>
    /// <returns>The density at the specified location (x).</returns>
    [HttpPost("PDF/{x:double}")]
    public IActionResult PDF([FromBody] LogNormalParams logNormalParams, [FromRoute] double x)
    {
        try
        {
            var distribution = _mapper.Map<LogNormal>(logNormalParams);

            return Ok(_distributionService.PDF(distribution, x));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Computes the log probability density of the LogNormal distribution (lnPDF) at a given location (x), i.e., ln(∂P(X ≤ x)/∂x).
    /// </summary>
    /// <param name="logNormalParams">The parameters for creating a LogNormal distribution.</param>
    /// <param name="x">The location at which to compute the log density.</param>
    /// <returns>The log density at the specified location (x).</returns>
    [HttpPost("PDFLn/{x:double}")]
    public IActionResult PDFLn([FromBody] LogNormalParams logNormalParams, [FromRoute] double x)
    {
        try
        {
            var distribution = _mapper.Map<LogNormal>(logNormalParams);

            return Ok(_distributionService.PDFLn(distribution, x));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Draws a sequence of random samples from the LogNormal distribution.
    /// </summary>
    /// <param name="logNormalParams">The parameters for creating a LogNormal distribution.</param>
    /// <param name="count">The number of samples to generate.</param>
    /// <returns>An infinite sequence of samples from the distribution.</returns>
    [HttpPost("Samples/{count:int}")]
    public IActionResult Samples([FromBody] LogNormalParams logNormalParams, [FromRoute] int count)
    {
        try
        {
            var distribution = _mapper.Map<LogNormal>(logNormalParams);

            return Ok(_distributionService.Samples(distribution, count));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}