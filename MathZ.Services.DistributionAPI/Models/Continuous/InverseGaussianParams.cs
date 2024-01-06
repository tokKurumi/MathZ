namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating an Inverse Gaussian distribution.
/// </summary>
/// <param name="Mu">The mean (μ) of the distribution. Range: μ > 0.</param>
/// <param name="Lambda">The shape (λ) of the distribution. Range: λ > 0.</param>
public record InverseGaussianParams(double Mu, double Lambda);