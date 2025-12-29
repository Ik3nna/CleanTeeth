using AutoMapper;
using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeQuery;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Common.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<DentalOffice, DentalOfficeDetailDTO>().ReverseMap();
        CreateMap<DentalOffice, DentalOfficeDTO>().ReverseMap();
        CreateMap<DentalOffice, GetDentalOfficesDTO>().ReverseMap()
;    }
}
