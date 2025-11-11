//using AutoMapper;
//using MODELS;
//using MODELS.Entities;
//using MODELS.ViewModels;

//namespace SERVICES.Mapping
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            CreateMap<UserDetail, MyProfileViewModel>()

//           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
//           .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
//           .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
//           .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.CreatedOn.ToString("yyyy-MM-dd"))).ReverseMap()
//           .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); ;

//            CreateMap<LabReportDetail, LabReportDetailViewModel>().ReverseMap();

//            CreateMap<StudentViewModel, StudentData>();
//            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudentId))
//            //    .ReverseMap()
//            //    .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id));

//            CreateMap<EmployeeViewModel, EmployeeData>();

//            CreateMap<RegistrationViewModel, Registration>();
//            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.employeeId))
//            //.ReverseMap()
//            //.ForMember(dest => dest.employeeId, opt => opt.MapFrom(src => src.Id));

//            // ✅ GYM IMAGE MAPPING - same logic style as Post mapping
//            CreateMap<GymImageMapping, GymImageMappingViewModel>()
//                .ReverseMap()
//                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

//            CreateMap<GymImageMapping, GymImageMappingViewModel>()
//                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
//                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
//                    new List<string> { src.Images ?? string.Empty }.Where(s => !string.IsNullOrEmpty(s)).ToList()))
//                .ForMember(dest => dest.ImagesName, opt => opt.MapFrom(src =>
//                    new List<string> { src.ImagesName ?? string.Empty }.Where(s => !string.IsNullOrEmpty(s)).ToList()))
//                .ReverseMap()
//                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
//        }

//    }
//    }

using AutoMapper;
using MODELS;
using MODELS.Entities;
using MODELS.ViewModels;
using System.Linq;

namespace SERVICES.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Example user mappings (fine)
            CreateMap<UserDetail, MyProfileViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
                .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
                .ForMember(dest => dest.RegistrationDate,
                    opt => opt.MapFrom(src => src.CreatedOn.ToString("yyyy-MM-dd")))
                .ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<LabReportDetail, LabReportDetailViewModel>().ReverseMap();
            CreateMap<StudentViewModel, StudentData>();
            CreateMap<EmployeeViewModel, EmployeeData>();
            CreateMap<RegistrationViewModel, Registration>();

            // ✅ FIXED: Only one mapping for GymImageMapping
            CreateMap<GymImageMapping, GymImageMappingViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    new List<string> { src.Images ?? string.Empty }.Where(s => !string.IsNullOrEmpty(s)).ToList()))
                .ForMember(dest => dest.ImagesName, opt => opt.MapFrom(src =>
                    new List<string> { src.ImagesName ?? string.Empty }.Where(s => !string.IsNullOrEmpty(s)).ToList()))
                .ReverseMap()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.Images != null && src.Images.Any() ? src.Images.FirstOrDefault() : null))
                .ForMember(dest => dest.ImagesName, opt => opt.MapFrom(src =>
                    src.ImagesName != null && src.ImagesName.Any() ? src.ImagesName.FirstOrDefault() : null))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

