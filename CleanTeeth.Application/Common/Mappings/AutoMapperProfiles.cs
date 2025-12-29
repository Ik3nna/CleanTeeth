using AutoMapper;
using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeQuery;
using CleanTeeth.Application.Patients.Commands.CreatePatient;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Common.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<DentalOffice, DentalOfficeDetailDTO>().ReverseMap();
        CreateMap<DentalOffice, DentalOfficeDTO>().ReverseMap();
        CreateMap<DentalOffice, GetDentalOfficesDTO>().ReverseMap();
        
        // Patients
        CreateMap<Patient, PatientDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ReverseMap();
;    }
}
