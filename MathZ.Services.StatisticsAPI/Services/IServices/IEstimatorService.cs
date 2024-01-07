namespace MathZ.Services.StatisticsAPI.Services.IServices;

public interface IEstimatorService
{
    (double Mean, double StandardDeviation) MeanStandardDeviation(IEnumerable<double> data);

    (double Mean, double Variance) MeanVariance(IEnumerable<double> data);

    (double Skewness, double Kurtosis) PopulationSkewnessKurtosis(IEnumerable<double> population);

    (double Skewness, double Kurtosis) SkewnessKurtosis(IEnumerable<double> data);
}