namespace MathZ.Services.StatisticsAPI.Services.IServices;

using MathNet.Numerics.Statistics;
using MathZ.Services.StatisticsAPI.Models;

public interface IQuantileService
{
    DescriptiveQuantileStatistics FullAnalysis(IEnumerable<double> data);

    double Quantile(IEnumerable<double> data, double tau);

    double QuantileCustom(IEnumerable<double> data, double tau, QuantileDefinition definition);

    double QuantileRank(IEnumerable<double> data, double x, RankDefinition definition);
}