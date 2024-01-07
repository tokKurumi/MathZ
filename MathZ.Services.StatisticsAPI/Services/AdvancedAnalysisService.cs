namespace MathZ.Services.StatisticsAPI.Services;

using System.Numerics;
using MathNet.Numerics.Statistics;
using MathZ.Services.StatisticsAPI.Services.IServices;

public class AdvancedAnalysisService : IAdvancedAnalysisService
{
    public double EmpiricalCDF(IEnumerable<double> data, double x)
    {
        return data.EmpiricalCDF(x);
    }

    public double EmpiricalInvCDF(IEnumerable<double> data, double tau)
    {
        return data.EmpiricalInvCDF(tau);
    }

    public Complex MaximumMagnitudePhase(IEnumerable<Complex> data)
    {
        return data.MaximumMagnitudePhase();
    }

    public Complex MinimumMagnitudePhase(IEnumerable<Complex> data)
    {
        return data.MinimumMagnitudePhase();
    }

    public IEnumerable<double> MovingAverage(IEnumerable<double> samples, int windowSize)
    {
        return samples.MovingAverage(windowSize);
    }

    public double[] Ranks(IEnumerable<double> data, RankDefinition definition)
    {
        return data.Ranks(definition);
    }

    public double InterquartileRange(IEnumerable<double> data)
    {
        return data.InterquartileRange();
    }

    public double LowerQuartile(IEnumerable<double> data)
    {
        return data.LowerQuartile();
    }

    public double UpperQuartile(IEnumerable<double> data)
    {
        return data.UpperQuartile();
    }
}