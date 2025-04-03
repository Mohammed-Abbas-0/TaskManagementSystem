using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Queries.Classess;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface;
using TaskManagementSystem.Domain.ValueObjects;

namespace TaskManagementSystem.Application.Queries.Handlers
{
    public class GetTasksQueryHandler:IRequestHandler<GetAllTasksQuery,List<Tasks>>
    {
        private readonly WhatsAppService _whatsAppService;
        private readonly ITaskRepository _taskRepository;
        private readonly IDistributedCache _distributedCache;
        public GetTasksQueryHandler(ITaskRepository taskRepository,IDistributedCache distributedCache, WhatsAppService whatsAppService)
        {
            _taskRepository = taskRepository;
            _distributedCache = distributedCache;
            _whatsAppService = whatsAppService;
        }

        public async Task<List<Tasks>> Handle(GetAllTasksQuery req,CancellationToken cancellationToken)
        {
            var cacheKey = $"tasks_{req.PageNumber}_{req.PageSize}";
            var cachedTasks = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedTasks))
                return  JsonSerializer.Deserialize<List<Tasks>>(cachedTasks);
            

            var tasks = await _taskRepository.GetAllTasksAsync(req.PageNumber, req.PageSize);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) 
            };

            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(tasks), cacheOptions, cancellationToken);
            _whatsAppService.SendWhatsAppMessage("+201024653996", "Test");

            return tasks.ToList();
        }
    }
}
