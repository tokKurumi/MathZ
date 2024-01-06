namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Categorical distribution.
/// </summary>
/// <param name="ProbabilityMass">An array of nonnegative ratios. This array does not need to be normalized as it is often impossible using floating point arithmetic.</param>
public record CategoricalParams(double[] ProbabilityMass);