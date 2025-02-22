using AutoMapper;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Domain.Models.Enums;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services.Interface;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.Services.Services
{
    public class TaskItemService(ITaskItemRepository taskItemRepository, IMapper mapper) : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<List<TaskItemReadDto>>> CreateTaskItemAsync(TaskItemCreateDto taskItemCreateDto)
        {
            try
            {
                TaskItem taskItem = _mapper.Map<TaskItem>(taskItemCreateDto);
                await _taskItemRepository.CreateTaskItemAsync(taskItem);

                List<TaskItem> taskItems = await _taskItemRepository.ReadTaskItemByCategoryAsync(taskItem.CategoryId);
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Task created successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemAsync()
        {
            try
            {
                List<TaskItem> taskItems = await _taskItemRepository.ReadTaskItemAsync();
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemByCategoryAsync(int categoryId)
        {
            try
            {
                List<TaskItem> taskItems = await _taskItemRepository.ReadTaskItemByCategoryAsync(categoryId); 
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemByUserAsync(int userId)
        {
            try
            {
                List<TaskItem> taskItems = await _taskItemRepository.ReadTaskItemByUserAsync(userId);
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }
            
        public async Task<ApiResponse<TaskItemReadDto>> ReadTaskItemByIdAsync(int id)
        {
            try
            {
                TaskItem taskItem = await _taskItemRepository.ReadTaskItemByIdAsync(id);
                TaskItemReadDto taskItemReadDto = _mapper.Map<TaskItemReadDto>(taskItem);

                return new ApiResponse<TaskItemReadDto>(taskItemReadDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TaskItemReadDto>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<TaskItemReadDto>>> UpdateTaskItemAsync(int id, TaskItemUpdateDto taskItemUpdateDto)
        {
            try
            {
                TaskItem taskItem = await _taskItemRepository.ReadTaskItemByIdAsync(id);
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

                List<TaskItem> taskItems = await _taskItemRepository.ReadTaskItemByCategoryAsync(taskItem.CategoryId);
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Task edited successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }
        public async Task<ApiResponse<List<TaskItemReadDto>>> DeleteTaskItemAsync(int id, int categoryId)
        {
            try
            {
                List<TaskItem> taskItems = await _taskItemRepository.DeleteTaskItemAsync(id, categoryId);
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Task deleted successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.InnerException}", 500);
            }
        }
    }
}
