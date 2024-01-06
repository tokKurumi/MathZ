namespace MathZ.Services.DistributionAPI;

using AutoMapper;
using MathNet.Numerics.Distributions;
using MathZ.Services.DistributionAPI.Models.Continuous;
using MathZ.Services.DistributionAPI.Models.Discrete;

public static class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<BernoulliParams, Bernoulli>()
                .ConstructUsing(converter => new Bernoulli(
                    converter.P));

            config.CreateMap<BetaParams, Beta>()
                .ConstructUsing(converter => new Beta(
                    converter.A, converter.B));

            config.CreateMap<BetaBinomialParams, BetaBinomial>()
                .ConstructUsing(converter => new BetaBinomial(
                    converter.N,
                    converter.A,
                    converter.B));

            config.CreateMap<BetaScaledParams, BetaScaled>()
                .ConstructUsing(converter => new BetaScaled(
                    converter.A,
                    converter.B,
                    converter.Location,
                    converter.Scale));

            config.CreateMap<BinomialParams, Binomial>()
                .ConstructUsing(converter => new Binomial(
                    converter.P,
                    converter.N));

            config.CreateMap<BurrParams, Burr>()
                .ConstructUsing(converter => new Burr(
                    converter.A,
                    converter.C,
                    converter.K,
                    default));

            config.CreateMap<CategoricalParams, Categorical>()
                .ConstructUsing(converter => new Categorical(
                    converter.ProbabilityMass));

            config.CreateMap<CauchyParams, Cauchy>()
                .ConstructUsing(converter => new Cauchy(
                    converter.Location,
                    converter.Scale));

            config.CreateMap<ChiParams, Chi>()
                .ConstructUsing(converter => new Chi(
                    converter.Freedom));

            config.CreateMap<ChiSquaredParams, ChiSquared>()
                .ConstructUsing(converter => new ChiSquared(
                    converter.Freedom));

            config.CreateMap<ContinuousUniformParams, ContinuousUniform>()
                .ConstructUsing(converter => new ContinuousUniform(
                    converter.Lower,
                    converter.Upper));

            config.CreateMap<ConwayMaxwellPoissonParams, ConwayMaxwellPoisson>()
                .ConstructUsing(converter => new ConwayMaxwellPoisson(
                    converter.Lambda,
                    converter.Nu));

            config.CreateMap<DiscreteUniformParams, DiscreteUniform>()
                .ConstructUsing(converter => new DiscreteUniform(
                    converter.Lower,
                    converter.Upper));

            config.CreateMap<ErlangParams, Erlang>()
                .ConstructUsing(converter => new Erlang(
                    converter.Shape,
                    converter.Rate));

            config.CreateMap<ExponentialParams, Exponential>()
                .ConstructUsing(converter => new Exponential(
                    converter.Rate));

            config.CreateMap<FisherSnedecorParams, FisherSnedecor>()
                .ConstructUsing(converter => new FisherSnedecor(
                    converter.D1,
                    converter.D2));

            config.CreateMap<GammaParams, Gamma>()
                .ConstructUsing(converter => new Gamma(
                    converter.Shape,
                    converter.Rate));

            config.CreateMap<GeometricParams, Geometric>()
                .ConstructUsing(converter => new Geometric(
                    converter.P));

            config.CreateMap<HypergeometricParams, Hypergeometric>()
                .ConstructUsing(converter => new Hypergeometric(
                    converter.Population,
                    converter.Success,
                    converter.Draws));

            config.CreateMap<InverseGammaParams, InverseGamma>()
                .ConstructUsing(converter => new InverseGamma(
                    converter.Shape,
                    converter.Scale));

            config.CreateMap<InverseGaussianParams, InverseGaussian>()
                .ConstructUsing(converter => new InverseGaussian(
                    converter.Mu,
                    converter.Lambda,
                    default));

            config.CreateMap<LaplaceParams, Laplace>()
                .ConstructUsing(converter => new Laplace(
                    converter.Location,
                    converter.Scale));

            config.CreateMap<LogisticParams, Logistic>()
                .ConstructUsing(converter => new Logistic(
                    converter.Mean,
                    converter.Scale));

            config.CreateMap<LogNormalParams, LogNormal>()
                .ConstructUsing(converter => new LogNormal(
                    converter.Mu,
                    converter.Sigma));

            config.CreateMap<NegativeBinomialParams, NegativeBinomial>()
                .ConvertUsing(converter => new NegativeBinomial(
                    converter.R,
                    converter.P));

            config.CreateMap<NormalParams, Normal>()
                .ConvertUsing(converter => new Normal(
                    converter.Mean,
                    converter.Stddev));

            config.CreateMap<ParetoParams, Pareto>()
                .ConvertUsing(converter => new Pareto(
                    converter.Scale,
                    converter.Shape));

            config.CreateMap<PoissonParams, Poisson>()
                .ConvertUsing(converter => new Poisson(
                    converter.Lambda));

            config.CreateMap<RayleighParams, Rayleigh>()
                .ConvertUsing(converter => new Rayleigh(
                    converter.Scale));

            config.CreateMap<SkewedGeneralizedErrorParams, SkewedGeneralizedError>()
                .ConvertUsing(converter => new SkewedGeneralizedError(
                    converter.Location,
                    converter.Scale,
                    converter.Skew,
                    converter.P));

            config.CreateMap<SkewedGeneralizedTParams, SkewedGeneralizedT>()
                .ConvertUsing(converter => new SkewedGeneralizedT(
                    converter.Location,
                    converter.Scale,
                    converter.Skew,
                    converter.P,
                    converter.Q));

            config.CreateMap<StableParams, Stable>()
                .ConstructUsing(converter => new Stable(
                    converter.Alpha,
                    converter.Beta,
                    converter.Scale,
                    converter.Location));

            config.CreateMap<StudentTParams, StudentT>()
                .ConstructUsing(converter => new StudentT(
                    converter.Location,
                    converter.Scale,
                    converter.Freedom));

            config.CreateMap<TriangularParams, Triangular>()
                .ConstructUsing(converter => new Triangular(
                    converter.Lower,
                    converter.Upper,
                    converter.Mode));

            config.CreateMap<TruncatedParetoParams, TruncatedPareto>()
                .ConstructUsing(converter => new TruncatedPareto(
                    converter.Scale,
                    converter.Shape,
                    converter.Truncation,
                    default));

            config.CreateMap<WeibullParams, Weibull>()
                .ConstructUsing(converter => new Weibull(
                    converter.Shape,
                    converter.Scale));

            config.CreateMap<ZipfParams, Zipf>()
                .ConstructUsing(converter => new Zipf(
                    converter.S,
                    converter.N));
        });
    }
}