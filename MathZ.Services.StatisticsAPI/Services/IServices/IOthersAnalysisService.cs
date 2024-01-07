namespace MathZ.Services.StatisticsAPI.Services.IServices;

public interface IOthersAnalysisService
{
    double GainMean(IEnumerable<double> data);

    double GainStandardDeviation(IEnumerable<double> data);

    double GeometricMean(IEnumerable<double> data);

    double HarmonicMean(IEnumerable<double> data);

    double LossMean(IEnumerable<double> data);

    double LossStandardDeviation(IEnumerable<double> data);

    double OrderStatistic(IEnumerable<double> data, int order);

    double Percentile(IEnumerable<double> data, int p);

    double RootMeanSquare(IEnumerable<double> data);
}