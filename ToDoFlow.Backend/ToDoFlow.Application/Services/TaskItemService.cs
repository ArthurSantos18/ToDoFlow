using AutoMapper;
using System.ComponentModel.DataAnnotations;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Application.Services.Interfaces;
using ToDoFlow.Application.Services.Utils;
using ToDoFlow.Domain.Models;
using ToDoFlow.Domain.Models.Enums;
using ToDoFlow.Infrastructure.Repositories;
using ToDoFlow.Infrastructure.Repositories.Interfaces;

namespace ToDoFlow.Application.Services
{
    public class TaskItemService(ITaskItemRepository taskItemRepository, ICategoryRepository CategoryRepository, IMapper mapper) : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
        private readonly ICategoryRepository _categoryRepository = CategoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<TaskItemReadDto>> CreateTaskItemAsync(int userId, TaskItemCreateDto taskItemCreateDto)
        {
            ValidationHelper.ValidateId(userId, "User");
            ValidationHelper.ValidateId(taskItemCreateDto.CategoryId, "Category");
                    
            TaskItem taskItem = _mapper.Map<TaskItem>(taskItemCreateDto);

            Category category = await _categoryRepository.GetCategoryByIdAsync(taskItem.CategoryId);

            ValidationHelper.ValidateObject(category, "Category");

            ValidationHelper.ValidateId(category.UserId, "User");

            await _taskItemRepository.CreateTaskItemAsync(taskItem);

            TaskItemReadDto taskItemReadDto = _mapper.Map<TaskItemReadDto>(taskItem);

            return new ApiResponse<TaskItemReadDto>(taskItemReadDto, true, "Task created successfully", 201);

        }

        public async Task<ApiResponse<IEnumerable<TaskItemReadDto>>> GetTaskItemAsync()
        {        
            IEnumerable<TaskItem> taskItems = await _taskItemRepository.GetTaskItemAsync();
            IEnumerable<TaskItemReadDto> taskItemReadDtos = _mapper.Map<IEnumerable<TaskItemReadDto>>(taskItems);

            return new ApiResponse<IEnumerable<TaskItemReadDto>>(taskItemReadDtos, true, "Operation carried out successfully", 200);
        }

        public async Task<ApiResponse<IEnumerable<TaskItemReadDto>>> GetTaskItemByCategoryAsync(int categoryId, int userId)
        {

            ValidationHelper.ValidateId(categoryId, "Category");
            ValidationHelper.ValidateId(userId, "User");
            
            Category category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            ValidationHelper.ValidateObject(category, "Category");

            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to access tasks in this category");
            }

            IEnumerable<TaskItem> taskItems = await _taskItemRepository.GetTaskItemByCategoryAsync(categoryId); 
            IEnumerable<TaskItemReadDto> taskItemReadDtos = _mapper.Map<IEnumerable<TaskItemReadDto>>(taskItems);

            return new ApiResponse<IEnumerable<TaskItemReadDto>>(taskItemReadDtos, true, "Operation carried out successfully", 200);
        }

        public async Task<ApiResponse<IEnumerable<TaskItemReadDto>>> GetTaskItemByUserAsync(int userId)
        {
            ValidationHelper.ValidateId(userId, "User");

            IEnumerable<TaskItem> taskItems = await _taskItemRepository.GetTaskItemByUserAsync(userId);
            IEnumerable<TaskItemReadDto> taskItemReadDtos = _mapper.Map<IEnumerable<TaskItemReadDto>>(taskItems);

            return new ApiResponse<IEnumerable<TaskItemReadDto>>(taskItemReadDtos, true, "Operation carried out successfully", 200);
            
        }
            
        public async Task<ApiResponse<TaskItemReadDto>> GetTaskItemByIdAsync(int id, int userId)
        {
            ValidationHelper.ValidateId(id, "Task");
            ValidationHelper.ValidateId(userId, "User");

            TaskItem taskItem = await _taskItemRepository.GetTaskItemByIdAsync(id);

            ValidationHelper.ValidateObject(taskItem, "Task");

            Category category = await _categoryRepository.GetCategoryByIdAsync(taskItem.CategoryId);
                
            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to access this task");
            }

            TaskItemReadDto taskItemReadDto = _mapper.Map<TaskItemReadDto>(taskItem);

            return new ApiResponse<TaskItemReadDto>(taskItemReadDto, true, "Operation carried out successfully", 200);
        }

        public async Task<ApiResponse<TaskItemReadDto>> UpdateTaskItemAsync(int id, int userId, TaskItemUpdateDto taskItemUpdateDto)
        {
            ValidationHelper.ValidateId(id, "Task");
            ValidationHelper.ValidateId(userId, "User");

            TaskItem taskItem = await _taskItemRepository.GetTaskItemByIdAsync(id);

            ValidationHelper.ValidateObject(taskItem, "Task");

            Category category = await _categoryRepository.GetCategoryByIdAsync(taskItem.CategoryId);

            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this task");
            }

            _mapper.Map(taskItemUpdateDto, taskItem);

            if (taskItem.Status == Status.Complete)
            {
                taskItem.CompleteAt = DateTime.Now;
            }
            else
            {
                taskItem.CompleteAt = null;
            }

            await _taskItemRepository.UpdateTaskItemAsync(taskItem);
                
            TaskItemReadDto taskItemReadDto = _mapper.Map<TaskItemReadDto>(taskItem);

            return new ApiResponse<TaskItemReadDto>(taskItemReadDto, true, "Task edited successfully", 200);
        }

        public async Task<ApiResponse> DeleteTaskItemAsync(int id, int userId)
        {
            ValidationHelper.ValidateId(id, "Task");
            ValidationHelper.ValidateId(userId, "User");

            TaskItem taskItem = await _taskItemRepository.GetTaskItemByIdAsync(id);

            ValidationHelper.ValidateObject(taskItem, "Task");

            Category category = await _categoryRepository.GetCategoryByIdAsync(taskItem.CategoryId);

            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this task");
            }

            await _taskItemRepository.DeleteTaskItemAsync(id);
                
            return new ApiResponse(true, "Task deleted successfully", 200);
        }
    }
}
