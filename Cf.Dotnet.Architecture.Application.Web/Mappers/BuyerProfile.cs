using AutoMapper;
using Cf.Dotnet.Architecture.Application.Models;
using Cf.Dotnet.Architecture.Domain.Entities;

namespace Cf.Dotnet.Architecture.Application.Mappers;

public class BuyerProfile : Profile
{
    public BuyerProfile()
    {
        CreateMap<Buyer, BuyerModel>();
    }
}