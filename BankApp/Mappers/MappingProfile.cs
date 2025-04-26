using AutoMapper;
using BankApp.DTOs;
using BankApp.Models;
using BankApp.Security;

namespace BankApp.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AccountRequest, Account>();
        CreateMap<Account, AccountRequest>();
        CreateMap<Client, ClientResponse>()
            .ForMember(
                dest => dest.Accounts,
                opt => opt.MapFrom(src => src.Accounts)
            );
        CreateMap<Account, AccountResponse>();
        CreateMap<Transaction, TransactionResponse>()
            .ForMember(
                dest => dest.FromAccountId,
                opt => opt.MapFrom(src => src.FromAccountId)
            )
            .ForMember(
                dest => dest.ToAccountId,
                opt => opt.MapFrom(src => src.ToAccountId)
            )
            .ForMember(
                dest => dest.Amount,
                opt => opt.MapFrom(src => src.Amount)
            )
            .ForMember(
                dest => dest.Currency,
                opt => opt.MapFrom(src => src.Currency)
            )
            .ForMember(
                dest => dest.TransactionDate,
                opt => opt.MapFrom(src => src.TransactionDate)
            );
        CreateMap<TransactionRequest, Transaction>()
            .ForMember(
                dest => dest.Amount,
                opt => opt.MapFrom(src => src.Amount)
            )
            .ForMember(
                dest => dest.Currency,
                opt => opt.MapFrom(src => src.Currency)
            )
            .ForMember(
                dest => dest.FromAccountId,
                opt => opt.MapFrom(src => src.FromAccountId)
            )
            .ForMember(
                dest => dest.ToAccountId,
                opt => opt.MapFrom(src => src.ToAccountId)
            )
            .ForMember(
                dest => dest.TransactionDate,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            );
        CreateMap<RegisterRequest, Client>()
            .ForMember(
                dest => dest.Password,
                opt => opt.MapFrom(src => PasswordHasher.Hash(src.Password))
            );
    }
}