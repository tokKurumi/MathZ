namespace MathZ.Services.DistributionAPI.Services.IServices;

using MathNet.Numerics.Distributions;

public interface IContinuousDistributionService
{
    double CDF(IContinuousDistribution continuousDistribution, double x);

    double PDF(IContinuousDistribution continuousDistribution, double x);

    double PDFLn(IContinuousDistribution continuousDistribution, double x);

    IEnumerable<double> Samples(IContinuousDistribution continuousDistribution, int count);
}