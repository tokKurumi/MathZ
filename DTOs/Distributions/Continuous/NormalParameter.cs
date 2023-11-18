namespace MathZ.DTOs.Distributions.Continuous
{
    public class NormalParameter : DistributionParameter
    {
        public double Mean { get; set; }

        public double StdDev { get; set; }

        public override string? GetDistributionName() => "Normal";
    }
}