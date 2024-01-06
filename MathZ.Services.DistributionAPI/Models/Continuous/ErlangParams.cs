namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating an Erlang distribution.
/// </summary>
/// <param name="Shape">The shape (k) of the Erlang distribution. Range: k ≥ 0.</param>
/// <param name="Rate">The rate or inverse scale (λ) of the Erlang distribution. Range: λ ≥ 0.</param>
public record ErlangParams(int Shape, double Rate);