namespace MathZ.Services.StatisticsAPI.Services.IServices;

using MathNet.Numerics.Statistics;

public interface IBasicAnalysisService
{
    double Covariance(IEnumerable<double> samples1, IEnumerable<double> samples2);

    double Entropy(IEnumerable<double> data);

    DescriptiveStatistics FullAnalysis(IEnumerable<double> data);

    double Kurtosis(IEnumerable<double> data);

    double Maximum(IEnumerable<double> data);

    double MaximumAbsolute(IEnumerable<double> data);

    double Mean(IEnumerable<double> data);

    double Median(IEnumerable<double> data);

    double Minimum(IEnumerable<double> data);

    double MinimumAbsolute(IEnumerable<double> data);

    double Skewness(IEnumerable<double> data);

    double StandardDeviation(IEnumerable<double> data);

    double Variance(IEnumerable<double> data);
}