namespace MathZ.Services.DistributionAPI.Services;

using MathNet.Numerics.Distributions;
using MathZ.Services.DistributionAPI.Services.IServices;

public class DiscreteDistributionService : IDiscreteDistributionService
{
    public double CDF(IDiscreteDistribution discreteDistribution, double x)
    {
        return discreteDistribution.CumulativeDistribution(x);
    }

    public double PMF(IDiscreteDistribution discreteDistribution, int k)
    {
        return discreteDistribution.Probability(k);
    }

    public double PMFLn(IDiscreteDistribution discreteDistribution, int k)
    {
        return discreteDistribution.ProbabilityLn(k);
    }

    public IEnumerable<int> Samples(IDiscreteDistribution discreteDistribution, int count)
    {
        return discreteDistribution.Samples().Take(count);
    }
}