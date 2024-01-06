namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Poisson distribution.
/// </summary>
/// <param name="Lambda">The lambda (λ) parameter of the Poisson distribution. Range: λ > 0.</param>
public record PoissonParams(double Lambda);