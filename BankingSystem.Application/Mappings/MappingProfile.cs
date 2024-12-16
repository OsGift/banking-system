using AutoMapper;
using BankingSystem.Application.DTOs;
using BankingSystem.Application.DTOs.BankingSystem.Application.DTOs;
using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping for Account -> AccountDto
            CreateMap<Account, AccountDto>();

            // Mapping for Transaction -> TransactionDto
            CreateMap<Transaction, TransactionDto>();

            // Mapping for DTO -> Entity
            CreateMap<AccountDto, Account>();
        }
    }
}
