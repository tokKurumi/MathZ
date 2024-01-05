namespace MathZ.Services.DistributionAPI.Services;

using MathNet.Numerics.Distributions;
using MathZ.Services.DistributionAPI.Services.IServices;

public class ContinuousDistributionService : IContinuousDistributionService
{
    public double CDF(IContinuousDistribution continuousDistribution, double x)
    {
        return continuousDistribution.CumulativeDistribution(x);
    }

    public double PDF(IContinuousDistribution continuousDistribution, double x)
    {
        return continuousDistribution.Density(x);
    }

    public double PDFLn(IContinuousDistribution continuousDistribution, double x)
    {
        return continuousDistribution.DensityLn(x);
    }

    public IEnumerable<double> Samples(IContinuousDistribution continuousDistribution, int count)
    {
        return continuousDistribution.Samples().Take(count);
    }
}