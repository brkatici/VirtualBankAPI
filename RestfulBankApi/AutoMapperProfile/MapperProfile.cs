using AutoMapper;
using RestfulBankApi.DTOs;
using RestfulBankApi.Models;


namespace RestfulBankApi.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AccountCreationModel, Account>();
            CreateMap<AccountTransactionDTO, AccountTransaction>();
            CreateMap<TransferTransactionDTO, TransferTransaction>();
            CreateMap<SupportMessagesDto, SupportMessages>();
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<UserDto, User>();
            CreateMap<UpdateBalanceDto, AccountDto>();
            // Pet

        }
    }
}