namespace MathZ.Controllers.Distributions.Continuous
{
    using MathNet.Numerics.Distributions;
    using MathZ.DTOs.Distributions;
    using MathZ.DTOs.Distributions.Continuous;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("Distributions/Continuous/[controller]")]
    public class NormalController : ControllerBase
    {
        private readonly ILogger<NormalController> _logger;

        public NormalController(ILogger<NormalController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Samples")]
        public IActionResult Samples([FromQuery] NormalParameter parameter, [FromQuery] int count)
        {
            return Ok(new SamplesResponse<NormalParameter, double>(
                parameter,
                Normal.Samples(new Random(), parameter.Mean, parameter.StdDev).Take(count)));
        }

        [HttpGet("Density")]
        public IActionResult Density([FromQuery] NormalParameter parameter, [FromQuery] double point)
        {
            return Ok(new DensityResponse<NormalParameter>(
                parameter,
                point,
                Normal.PDF(parameter.Mean, parameter.StdDev, point)));
        }

        [HttpGet("LnDensity")]
        public IActionResult LnDensity([FromQuery] NormalParameter parameter, [FromQuery] double point)
        {
            return Ok(new DensityResponse<NormalParameter>(
                parameter,
                point,
                Normal.PDFLn(parameter.Mean, parameter.StdDev, point)));
        }

        [HttpGet("Cumulative")]
        public IActionResult Cumulative([FromQuery] NormalParameter parameter, [FromQuery] double point)
        {
            return Ok(new DensityResponse<NormalParameter>(
                parameter,
                point,
                Normal.CDF(parameter.Mean, parameter.StdDev, point)));
        }

        [HttpGet("InvCumulative")]
        public IActionResult InvCumulative([FromQuery] NormalParameter parameter, [FromQuery] double point)
        {
            return Ok(new DensityResponse<NormalParameter>(
                parameter,
                point,
                Normal.InvCDF(parameter.Mean, parameter.StdDev, point)));
        }
    }
}