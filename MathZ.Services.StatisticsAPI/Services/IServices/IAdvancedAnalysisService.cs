namespace MathZ.Services.StatisticsAPI.Services.IServices;

using System.Numerics;
using MathNet.Numerics.Statistics;

public interface IAdvancedAnalysisService
{
    double EmpiricalCDF(IEnumerable<double> data, double x);

    double EmpiricalInvCDF(IEnumerable<double> data, double tau);

    double InterquartileRange(IEnumerable<double> data);

    double LowerQuartile(IEnumerable<double> data);

    Complex MaximumMagnitudePhase(IEnumerable<Complex> data);

    Complex MinimumMagnitudePhase(IEnumerable<Complex> data);

    IEnumerable<double> MovingAverage(IEnumerable<double> samples, int windowSize);

    double[] Ranks(IEnumerable<double> data, RankDefinition definition);

    double UpperQuartile(IEnumerable<double> data);
}