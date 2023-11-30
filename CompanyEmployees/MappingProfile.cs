using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace CompanyEmployees
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Hotel, HotelDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Lodger, LodgerDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<HotelForCreatonDto, Hotel>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<LodgerForUpdateDto, Lodger>().ReverseMap();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<HotelForUpdateDto, Hotel>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<UserForAuthenticationDto, User>();
        }
    }
}
