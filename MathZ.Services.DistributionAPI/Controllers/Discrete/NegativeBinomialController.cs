namespace MathZ.Services.DistributionAPI.Controllers.Discrete;

using AutoMapper;
using MathNet.Numerics.Distributions;
using MathZ.Services.DistributionAPI.Models.Discrete;
using MathZ.Services.DistributionAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/distribution/discrete/[controller]")]
[Authorize(Roles = "admin, distribution")]
public class NegativeBinomialController(
    IDiscreteDistributionService distributionService,
    IMapper mapper)
    : ControllerBase
{
    private readonly IDiscreteDistributionService _distributionService = distributionService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Computes the cumulative distribution (CDF) of the NegativeBinomial distribution at a given location (x), i.e., P(X ≤ x).
    /// </summary>
    /// <param name="negativeBinomialParams">The parameters for creating a NegativeBinomial distribution.</param>
    /// <param name="x">The location at which to compute the cumulative distribution function.</param>
    /// <returns>The cumulative distribution at the specified location (x).</returns>
    [HttpPost("CDF/{x:double}")]
    public IActionResult CDF([FromBody] NegativeBinomialParams negativeBinomialParams, [FromRoute] double x)
    {
        try
        {
            var distribution = _mapper.Map<NegativeBinomial>(negativeBinomialParams);

            return Ok(_distributionService.CDF(distribution, x));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Computes the probability mass (PMF) at a specified location (k), i.e., P(X = k).
    /// </summary>
    /// <param name="negativeBinomialParams">The parameters for creating a NegativeBinomial distribution.</param>
    /// <param name="k">The location in the domain where we want to evaluate the probability mass function.</param>
    /// <returns>The probability mass at the specified location (k).</returns>
    [HttpPost("PMF/{k:int}")]
    public IActionResult PMF([FromBody] NegativeBinomialParams negativeBinomialParams, [FromRoute] int k)
    {
        try
        {
            var distribution = _mapper.Map<NegativeBinomial>(negativeBinomialParams);

            return Ok(_distributionService.PMF(distribution, k));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Computes the log probability mass (lnPMF) at a specified location (k), i.e., ln(P(X = k)).
    /// </summary>
    /// <param name="negativeBinomialParams">The parameters for creating a NegativeBinomial distribution.</param>
    /// <param name="k">The location in the domain where we want to evaluate the log probability mass function.</param>
    /// <returns>The log probability mass at the specified location (k).</returns>
    [HttpPost("PMFLn/{k:int}")]
    public IActionResult PMFLn([FromBody] NegativeBinomialParams negativeBinomialParams, [FromRoute] int k)
    {
        try
        {
            var distribution = _mapper.Map<NegativeBinomial>(negativeBinomialParams);

            return Ok(_distributionService.PMFLn(distribution, k));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Draws a sequence of random samples from the NegativeBinomial distribution.
    /// </summary>
    /// <param name="negativeBinomialParams">The parameters for creating a NegativeBinomial distribution.</param>
    /// <param name="count">The number of samples to generate.</param>
    /// <returns>An infinite sequence of samples from the distribution.</returns>
    [HttpPost("Samples/{count:int}")]
    public IActionResult Samples([FromBody] NegativeBinomialParams negativeBinomialParams, [FromRoute] int count)
    {
        try
        {
            var distribution = _mapper.Map<NegativeBinomial>(negativeBinomialParams);

            return Ok(_distributionService.Samples(distribution, count));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}