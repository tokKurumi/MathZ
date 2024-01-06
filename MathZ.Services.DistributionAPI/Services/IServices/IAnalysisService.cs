namespace MathZ.Services.DistributionAPI.Services.IServices;

using MathZ.Services.DistributionAPI.Models;

public interface IAnalysisService
{
    Task<AnalysisResponse> Analyze(IEnumerable<double> samples);
}