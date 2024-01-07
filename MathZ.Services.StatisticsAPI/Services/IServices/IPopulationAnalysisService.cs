namespace MathZ.Services.StatisticsAPI.Services.IServices;

using MathZ.Services.StatisticsAPI.Models;

public interface IPopulationAnalysisService
{
    double Covariance(IEnumerable<double> population1, IEnumerable<double> population2);

    DescriptivePopulationStatistics FullAnalysis(IEnumerable<double> data);

    double Mean(IEnumerable<double> data);

    double PopulationKurtosis(IEnumerable<double> data);

    double PopulationSkewness(IEnumerable<double> data);

    double PopulationStandardDeviation(IEnumerable<double> data);

    double PopulationVariance(IEnumerable<double> data);
}