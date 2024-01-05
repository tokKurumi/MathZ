namespace MathZ.Services.DistributionAPI.Services.IServices;

using MathNet.Numerics.Distributions;

public interface IDiscreteDistributionService
{
    double CDF(IDiscreteDistribution discreteDistribution, double x);

    double PMF(IDiscreteDistribution discreteDistribution, int k);

    double PMFLn(IDiscreteDistribution discreteDistribution, int k);

    IEnumerable<int> Samples(IDiscreteDistribution discreteDistribution, int count);
}