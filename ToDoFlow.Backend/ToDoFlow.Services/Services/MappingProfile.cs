using AutoMapper;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // TaskItem Map
            CreateMap<TaskItemCreateDto, TaskItem>();
            CreateMap<TaskItem, TaskItemReadDto>();
            CreateMap<TaskItemUpdateDto, TaskItem>();

            // CategoryMap
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryUpdateDto, Category>();

            // UserMap
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReadDto>();
            CreateMap<UserUpdateDto, User>();

            //AccountAuthMap
            CreateMap<RegisterRequestDto, User>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            //UserRefreshTokenMap
            CreateMap<UserRefreshToken, UserRefreshTokenReadDto>();
        }
    }
}
