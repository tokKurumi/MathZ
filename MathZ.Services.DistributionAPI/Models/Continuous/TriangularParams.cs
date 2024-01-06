namespace MathZ.Services.DistributionAPI.Models.Continuous;

/// <summary>
/// Represents the parameters for creating a Triangular distribution.
/// </summary>
/// <param name="Lower">Lower bound. Range: lower ≤ mode ≤ upper.</param>
/// <param name="Upper">Upper bound. Range: lower ≤ mode ≤ upper.</param>
/// <param name="Mode">Mode (most frequent value). Range: lower ≤ mode ≤ upper.</param>
public record TriangularParams(double Lower, double Upper, double Mode);