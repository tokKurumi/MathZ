namespace MathZ.DTOs.Distributions
{
    public class SamplesResponse<TDistribution, TSample>
        where TDistribution : DistributionParameter
    {
        public SamplesResponse(TDistribution? distributionParameter, IEnumerable<TSample>? samples)
        {
            DistributionName = distributionParameter?.GetDistributionName() ?? "Unknown distribution";
            DistributionParameter = distributionParameter;
            Samples = samples;
        }

        public string? DistributionName { get; set; }

        public TDistribution? DistributionParameter { get; set; }

        public IEnumerable<TSample>? Samples { get; set; }
    }
}