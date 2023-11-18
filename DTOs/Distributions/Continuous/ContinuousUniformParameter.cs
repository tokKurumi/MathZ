namespace MathZ.DTOs.Distributions.Continuous
{
    public class ContinuousUniformParameter : DistributionParameter
    {
        public double Lower { get; set; }

        public double Upper { get; set; }

        public override string? GetDistributionName() => "ContinuousUniform";
    }
}