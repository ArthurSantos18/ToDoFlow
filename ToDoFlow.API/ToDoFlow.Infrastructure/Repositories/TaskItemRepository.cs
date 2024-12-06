using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ToDoFlowContext _context;
        private readonly IMapper _mapper;

        public TaskItemRepository(ToDoFlowContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<TaskItemReadDto>>> CreateTaskItemAsync(TaskItemCreateDto TaskItemCreateDto)
        {
            try
            {
                TaskItem taskItem = _mapper.Map<TaskItem>(TaskItemCreateDto);
                await _context.AddAsync(taskItem);  
                await _context.SaveChangesAsync();

                List<TaskItem> taskItems = await _context.Tasks.ToListAsync();
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Tarefa adicionada com sucesso", 201);

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
                List<TaskItem> taskItems = await _context.Tasks.ToListAsync();
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Operação concluída", 200);
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
                TaskItem taskItem= await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                TaskItemReadDto taskItemReadDto = _mapper.Map<TaskItemReadDto>(taskItem);

                return new ApiResponse<TaskItemReadDto>(taskItemReadDto, true, "Operação concluída", 200);
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
                TaskItem taskItem = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskItemUpdateDto.Id);
                _mapper.Map(taskItemUpdateDto, taskItem);
                _context.Tasks.Update(taskItem);
                await _context.SaveChangesAsync();

                List<TaskItem> taskItems = await _context.Tasks.ToListAsync();
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Operação realizada com sucesso", 204);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<TaskItemReadDto>>> DeleteTaskItemAsync(int id)
        {
            try
            {
                TaskItem taskItem = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                _context.Tasks.Remove(taskItem);
                await _context.SaveChangesAsync();

                List<TaskItem> taskItems = await _context.Tasks.ToListAsync();
                List<TaskItemReadDto> taskItemReadDtos = _mapper.Map<List<TaskItemReadDto>>(taskItems);

                return new ApiResponse<List<TaskItemReadDto>>(taskItemReadDtos, true, "Tarefa deletada com sucesso", 204);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TaskItemReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
