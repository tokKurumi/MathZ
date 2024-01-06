namespace MathZ.Services.DistributionAPI.Controllers;

using MathZ.Services.DistributionAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/distribution/[controller]")]
[Authorize(Roles = "admin, distribution")]
public class AnalysisController(
    IAnalysisService analysisService)
    : ControllerBase
{
    private readonly IAnalysisService _analysisService;

    /// <summary>
    /// Analyzes a set of numeric samples and returns statistical information.
    /// </summary>
    /// <param name="samples">A collection of numeric samples to be analyzed.</param>
    /// <returns>An IActionResult containing the analysis response with statistical information.</returns>
    [HttpPost]
    public IActionResult Analyze([FromBody] IEnumerable<double> samples)
    {
        return Ok(analysisService.Analyze(samples));
    }
}