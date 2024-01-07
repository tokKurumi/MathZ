namespace MathZ.Services.StatisticsAPI.Services;

using MathNet.Numerics.Statistics;
using MathZ.Services.StatisticsAPI.Services.IServices;

public class BasicAnalysisService : IBasicAnalysisService
{
    public double Covariance(IEnumerable<double> samples1, IEnumerable<double> samples2)
    {
        return samples1.Covariance(samples2);
    }

    public DescriptiveStatistics FullAnalysis(IEnumerable<double> data)
    {
        return new DescriptiveStatistics(data);
    }

    public double Mean(IEnumerable<double> data)
    {
        return data.Mean();
    }

    public double Variance(IEnumerable<double> data)
    {
        return data.Variance();
    }

    public double StandardDeviation(IEnumerable<double> data)
    {
        return data.StandardDeviation();
    }

    public double Skewness(IEnumerable<double> data)
    {
        return data.Skewness();
    }

    public double Kurtosis(IEnumerable<double> data)
    {
        return data.Kurtosis();
    }

    public double Maximum(IEnumerable<double> data)
    {
        return data.Maximum();
    }

    public double MaximumAbsolute(IEnumerable<double> data)
    {
        return data.MaximumAbsolute();
    }

    public double Median(IEnumerable<double> data)
    {
        return data.Median();
    }

    public double Minimum(IEnumerable<double> data)
    {
        return data.Minimum();
    }

    public double MinimumAbsolute(IEnumerable<double> data)
    {
        return data.MinimumAbsolute();
    }

    public double Entropy(IEnumerable<double> data)
    {
        return Statistics.Entropy(data);
    }
}