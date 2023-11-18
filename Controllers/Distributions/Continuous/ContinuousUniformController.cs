namespace MathZ.Controllers.Distributions.Continuous
{
    using MathNet.Numerics.Distributions;
    using MathZ.DTOs.Distributions;
    using MathZ.DTOs.Distributions.Continuous;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("Distributions/Continuous/[controller]")]
    public class ContinuousUniformController : ControllerBase
    {
        private readonly ILogger<ContinuousUniformController> _logger;

        public ContinuousUniformController(ILogger<ContinuousUniformController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Samples")]
        public IActionResult Samples([FromQuery] ContinuousUniformParameter parameter, [FromQuery] int count)
        {
            return Ok(new SamplesResponse<ContinuousUniformParameter, double>(
                parameter,
                ContinuousUniform.Samples(new Random(), parameter.Lower, parameter.Upper).Take(count)));
        }

        [HttpGet("Density")]
        public IActionResult Density([FromQuery] ContinuousUniformParameter parameter, [FromQuery] double point)
        {
            return Ok(new DensityResponse<ContinuousUniformParameter>(
                parameter,
                point,
                ContinuousUniform.PDF(parameter.Lower, parameter.Upper, point)));
        }

        [HttpGet("LnDensity")]
        public IActionResult LnDensity([FromQuery] ContinuousUniformParameter parameter, [FromQuery] double point)
        {
            return Ok(new DensityResponse<ContinuousUniformParameter>(
                parameter,
                point,
                ContinuousUniform.PDFLn(parameter.Lower, parameter.Upper, point)));
        }

        [HttpGet("Cumulative")]
        public IActionResult Cumulative([FromQuery] ContinuousUniformParameter parameter, [FromQuery] double point)
        {
            return Ok(new DensityResponse<ContinuousUniformParameter>(
                parameter,
                point,
                ContinuousUniform.CDF(parameter.Lower, parameter.Upper, point)));
        }

        [HttpGet("InvCumulative")]
        public IActionResult InvCumulative([FromQuery] ContinuousUniformParameter parameter, [FromQuery] double point)
        {
            return Ok(new DensityResponse<ContinuousUniformParameter>(
                parameter,
                point,
                ContinuousUniform.InvCDF(parameter.Lower, parameter.Upper, point)));
        }
    }
}