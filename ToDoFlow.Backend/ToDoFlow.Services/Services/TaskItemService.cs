using AutoMapper;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services.Interface;

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

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Tarefa criada com sucesso", 200);
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

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Operação realizada com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<TaskItemReadDto>> ReadTaskItemAsync(int id)
        {
            try
            {
                TaskItem taskItem = await _taskItemRepository.ReadTaskItemAsync(id);
                TaskItemReadDto taskItemReadDto = _mapper.Map<TaskItemReadDto>(taskItem);

                return new ApiResponse<TaskItemReadDto>(taskItemReadDto, true, "Operação realizada com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TaskItemReadDto>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<TaskItemReadDto>>> UpdateTaskItemAsync(TaskItemUpdateDto taskItemUpdateDto)
        {
            try
            {
                TaskItem taskItem = await _taskItemRepository.ReadTaskItemAsync(taskItemUpdateDto.Id);
                _mapper.Map(taskItemUpdateDto, taskItem);
                await _taskItemRepository.UpdateTaskItemAsync(taskItem);

                List<TaskItem> taskItems = await _taskItemRepository.ReadTaskItemByCategoryAsync(taskItem.CategoryId);
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Tarefa editada com sucesso", 200);
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

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Operação realizada com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
