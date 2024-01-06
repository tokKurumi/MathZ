namespace MathZ.Services.DistributionAPI.Controllers.Continuous;

using AutoMapper;
using MathNet.Numerics.Distributions;
using MathZ.Services.DistributionAPI.Models.Continuous;
using MathZ.Services.DistributionAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/distribution/continuous/[controller]")]
[Authorize(Roles = "admin, distribution")]
public class ErlangController(
    IContinuousDistributionService distributionService,
    IMapper mapper)
    : ControllerBase
{
    private readonly IContinuousDistributionService _distributionService = distributionService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Computes the cumulative distribution (CDF) of the Erlang distribution at a given location (x), i.e., P(X ≤ x).
    /// </summary>
    /// <param name="erlangParams">The parameters for creating a Erlang distribution.</param>
    /// <param name="x">The location at which to compute the cumulative distribution function.</param>
    /// <returns>The cumulative distribution at the specified location (x).</returns>
    [HttpPost("CDF/{x:double}")]
    public IActionResult CDF([FromBody] ErlangParams erlangParams, [FromRoute] double x)
    {
        try
        {
            var distribution = _mapper.Map<Erlang>(erlangParams);

            return Ok(_distributionService.CDF(distribution, x));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Computes the probability density of the Erlang distribution (PDF) at a given location (x), i.e., ∂P(X ≤ x)/∂x.
    /// </summary>
    /// <param name="erlangParams">The parameters for creating a Erlang distribution.</param>
    /// <param name="x">The location at which to compute the density.</param>
    /// <returns>The density at the specified location (x).</returns>
    [HttpPost("PDF/{x:double}")]
    public IActionResult PDF([FromBody] ErlangParams erlangParams, [FromRoute] double x)
    {
        try
        {
            var distribution = _mapper.Map<Erlang>(erlangParams);

            return Ok(_distributionService.PDF(distribution, x));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Computes the log probability density of the Erlang distribution (lnPDF) at a given location (x), i.e., ln(∂P(X ≤ x)/∂x).
    /// </summary>
    /// <param name="erlangParams">The parameters for creating a Erlang distribution.</param>
    /// <param name="x">The location at which to compute the log density.</param>
    /// <returns>The log density at the specified location (x).</returns>
    [HttpPost("PDFLn/{x:double}")]
    public IActionResult PDFLn([FromBody] ErlangParams erlangParams, [FromRoute] double x)
    {
        try
        {
            var distribution = _mapper.Map<Erlang>(erlangParams);

            return Ok(_distributionService.PDFLn(distribution, x));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Draws a sequence of random samples from the Erlang distribution.
    /// </summary>
    /// <param name="erlangParams">The parameters for creating a Erlang distribution.</param>
    /// <param name="count">The number of samples to generate.</param>
    /// <returns>An infinite sequence of samples from the distribution.</returns>
    [HttpPost("Samples/{count:int}")]
    public IActionResult Samples([FromBody] ErlangParams erlangParams, [FromRoute] int count)
    {
        try
        {
            var distribution = _mapper.Map<Erlang>(erlangParams);

            return Ok(_distributionService.Samples(distribution, count));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}