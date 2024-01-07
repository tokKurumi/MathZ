namespace MathZ.Services.StatisticsAPI.Services;

using MathNet.Numerics.Statistics;
using MathZ.Services.StatisticsAPI.Services.IServices;

public class EstimatorService : IEstimatorService
{
    public (double Mean, double Variance) MeanVariance(IEnumerable<double> data)
    {
        return data.MeanVariance();
    }

    public (double Mean, double StandardDeviation) MeanStandardDeviation(IEnumerable<double> data)
    {
        return data.MeanStandardDeviation();
    }

    public (double Skewness, double Kurtosis) SkewnessKurtosis(IEnumerable<double> data)
    {
        return data.SkewnessKurtosis();
    }

    public (double Skewness, double Kurtosis) PopulationSkewnessKurtosis(IEnumerable<double> population)
    {
        return population.PopulationSkewnessKurtosis();
    }
}