using AutoMapper;
using CleanTeeth.Application.Appointments.Commands.CreateAppointment;
using CleanTeeth.Application.Appointments.Queries.GetAppointmentDetail;
using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeQuery;
using CleanTeeth.Application.Dentists.Commands.CreateDentist;
using CleanTeeth.Application.Dentists.Queries.GetDentist;
using CleanTeeth.Application.Dentists.Queries.GetDentistDetail;
using CleanTeeth.Application.Patients.Commands.CreatePatient;
using CleanTeeth.Application.Patients.Queries.GetPatient;
using CleanTeeth.Application.Patients.Queries.GetPatientDetail;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.ValueObjects;

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

        CreateMap<Patient, GetPatientDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ReverseMap();

        CreateMap<Patient, PatientDetailDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ReverseMap();

        // Dentists
        CreateMap<Dentist, DentistDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ReverseMap();

        CreateMap<Dentist, GetDentistDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ReverseMap();

        CreateMap<Dentist, DentistDetailDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ReverseMap();

        // Appointments
        CreateMap<Appointment, AppointmentDTO>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.TimeInterval.StartTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.TimeInterval.EndTime))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.TimeInterval, opt => opt.MapFrom(dto => new TimeInterval(dto.StartDate, dto.EndDate)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(dto => Enum.Parse<AppointmentStatus>(dto.Status)));

        CreateMap<Appointment, AppointmentDetailDTO>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.TimeInterval.StartTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.TimeInterval.EndTime))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.TimeInterval, opt => opt.MapFrom(dto => new TimeInterval(dto.StartDate, dto.EndDate)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(dto => Enum.Parse<AppointmentStatus>(dto.Status)));

        CreateMap<Patient, SimpleEntityDTO>().ReverseMap();
        CreateMap<Dentist, SimpleEntityDTO>().ReverseMap();
        CreateMap<DentalOffice, SimpleEntityDTO>().ReverseMap();
;    }
}
