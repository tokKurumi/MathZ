namespace MathZ.Services.StatisticsAPI.Services;

using MathNet.Numerics.Financial;
using MathNet.Numerics.Statistics;
using MathZ.Services.StatisticsAPI.Services.IServices;

public class OthersAnalysisService : IOthersAnalysisService
{
    public double GeometricMean(IEnumerable<double> data)
    {
        return data.GeometricMean();
    }

    public double HarmonicMean(IEnumerable<double> data)
    {
        return data.HarmonicMean();
    }

    public double RootMeanSquare(IEnumerable<double> data)
    {
        return data.RootMeanSquare();
    }

    public double GainMean(IEnumerable<double> data)
    {
        return data.GainMean();
    }

    public double LossMean(IEnumerable<double> data)
    {
        return data.LossMean();
    }

    public double GainStandardDeviation(IEnumerable<double> data)
    {
        return data.GainStandardDeviation();
    }

    public double LossStandardDeviation(IEnumerable<double> data)
    {
        return data.LossStandardDeviation();
    }

    public double OrderStatistic(IEnumerable<double> data, int order)
    {
        return Statistics.OrderStatistic(data, order);
    }

    public double Percentile(IEnumerable<double> data, int p)
    {
        return data.Percentile(p);
    }
}