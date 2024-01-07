namespace MathZ.Services.StatisticsAPI.Services;

using MathNet.Numerics.Statistics;
using MathZ.Services.StatisticsAPI.Models;
using MathZ.Services.StatisticsAPI.Services.IServices;

public class PopulationAnalysisService : IPopulationAnalysisService
{
    public double Covariance(IEnumerable<double> population1, IEnumerable<double> population2)
    {
        return population1.PopulationCovariance(population2);
    }

    public DescriptivePopulationStatistics FullAnalysis(IEnumerable<double> data)
    {
        return new DescriptivePopulationStatistics(
            data.LongCount(),
            data.Mean(),
            data.PopulationVariance(),
            data.PopulationStandardDeviation(),
            data.PopulationSkewness(),
            data.PopulationKurtosis(),
            data.Max(),
            data.Min());
    }

    public double Mean(IEnumerable<double> data)
    {
        return data.Mean();
    }

    public double PopulationVariance(IEnumerable<double> data)
    {
        return data.PopulationVariance();
    }

    public double PopulationStandardDeviation(IEnumerable<double> data)
    {
        return data.PopulationStandardDeviation();
    }

    public double PopulationSkewness(IEnumerable<double> data)
    {
        return data.PopulationSkewness();
    }

    public double PopulationKurtosis(IEnumerable<double> data)
    {
        return data.PopulationKurtosis();
    }
}