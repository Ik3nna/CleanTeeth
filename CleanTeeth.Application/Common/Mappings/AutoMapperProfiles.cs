using AutoMapper;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Common.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<DentalOffice, DentalOfficeDetailDTO>().ReverseMap();
    }
}
