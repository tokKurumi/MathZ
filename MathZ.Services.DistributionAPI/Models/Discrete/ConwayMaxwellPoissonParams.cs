namespace MathZ.Services.DistributionAPI.Models.Discrete;

/// <summary>
/// Represents the parameters for creating a Conway-Maxwell Poisson distribution.
/// </summary>
/// <param name="Lambda">The lambda (λ) parameter. Range: λ > 0.</param>
/// <param name="Nu">The rate of decay (ν) parameter. Range: ν ≥ 0.</param>
public record ConwayMaxwellPoissonParams(double Lambda, double Nu);