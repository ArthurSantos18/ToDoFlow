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
            CreateMap<Category, CategoryReadDto>().ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));
            CreateMap<CategoryUpdateDto, Category>();

            // UserMap
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReadDto>().ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
            CreateMap<UserUpdateDto, User>();
        }
    }
}
