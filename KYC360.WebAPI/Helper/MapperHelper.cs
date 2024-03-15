using AutoMapper;
using KYC360.WebAPI.Model.DTOs.Address;
using KYC360.WebAPI.Model.DTOs.Date;
using KYC360.WebAPI.Model.DTOs.Entity;
using KYC360.WebAPI.Model.DTOs.Name;
using KYC360.WebAPI.Model.Entities;

namespace KYC360.WebAPI.Helper
{
    public class MapperHelper :Profile
    {
        public MapperHelper()
        {
            CreateMap<Entity,EntityRequestDTO>().ReverseMap();
            CreateMap<Entity,EntityResponseDTO>().ReverseMap();
            CreateMap<Entity,EntityUpdateDTO>().ReverseMap();

            CreateMap<Address,AddressResponseDTO>().ReverseMap();
            CreateMap<Address,AddressRequestDTO>().ReverseMap();

            CreateMap<Name, NameRequestDTO>().ReverseMap();
            CreateMap<Name, NameResponseDTO>().ReverseMap();

            CreateMap<Date,DateRequestDTO>().ReverseMap();
            CreateMap<Date,DateResponseDTO>().ReverseMap();

        }
    }
}
