namespace MathZ.Services.DistributionAPI.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using MathZ.Services.DistributionAPI.Models;
using MathZ.Services.DistributionAPI.Services.IServices;

public class AnalysisService : IAnalysisService
{
    public Task<AnalysisResponse> Analyze(IEnumerable<double> samples)
    {
        var data = new List<double>(samples);

        var mean = data.Count > 0 ? data.Average() : 0;
        var variance = data.Count > 1 ? data.Sum(x => Math.Pow(x - mean, 2)) / (data.Count - 1) : 0;
        var standardDeviation = Math.Sqrt(variance);
        var skewness = data.Count > 2 ? data.Sum(x => Math.Pow(x - mean, 3)) / (data.Count * Math.Pow(standardDeviation, 3)) : 0;
        var kurtosis = data.Count > 3 ? (data.Sum(x => Math.Pow(x - mean, 4)) / (data.Count * Math.Pow(standardDeviation, 4))) - 3 : 0;
        var maximum = data.Count > 0 ? data.Max() : 0;
        var minimum = data.Count > 0 ? data.Min() : 0;

        return Task.FromResult(new AnalysisResponse(
            data.Count,
            mean,
            variance,
            standardDeviation,
            skewness,
            kurtosis,
            maximum,
            minimum));
    }
}