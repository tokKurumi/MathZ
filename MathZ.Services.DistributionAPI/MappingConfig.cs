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
        });
    }
}