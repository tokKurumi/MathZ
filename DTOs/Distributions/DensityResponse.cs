namespace MathZ.DTOs.Distributions
{
    public class DensityResponse<T>
        where T : DistributionParameter
    {
        public DensityResponse(T? distributionParameter, double point, double density)
        {
            DistributionName = distributionParameter?.GetDistributionName() ?? "Unknown distribution";

            DistributionParameter = distributionParameter;
            Point = point;
            Density = density;
        }

        public string? DistributionName { get; set; }

        public T? DistributionParameter { get; set; }

        public double Point { get; set; }

        public double Density { get; set; }
    }
}