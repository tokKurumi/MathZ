namespace MathZ.Services.StatisticsAPI.Services;

using MathNet.Numerics.Statistics;
using MathZ.Services.StatisticsAPI.Models;
using MathZ.Services.StatisticsAPI.Services.IServices;

public class QuantileService : IQuantileService
{
    public DescriptiveQuantileStatistics FullAnalysis(IEnumerable<double> data)
    {
        var stats = Statistics.FiveNumberSummary(data);

        return new DescriptiveQuantileStatistics(
            stats[0],
            stats[1],
            stats[2],
            stats[3],
            stats[4]);
    }

    public double Quantile(IEnumerable<double> data, double tau)
    {
        return data.Quantile(tau);
    }

    public double QuantileCustom(IEnumerable<double> data, double tau, QuantileDefinition definition)
    {
        return data.QuantileCustom(tau, definition);
    }

    public double QuantileRank(IEnumerable<double> data, double x, RankDefinition definition)
    {
        return data.QuantileRank(x, definition);
    }
}