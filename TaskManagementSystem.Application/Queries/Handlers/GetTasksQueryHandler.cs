using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetTasksQueryHandler> _logger;
        private readonly IConfiguration _configuration;
        public GetTasksQueryHandler(ITaskRepository taskRepository,IDistributedCache distributedCache, WhatsAppService whatsAppService, ILogger<GetTasksQueryHandler> logger, IConfiguration configuration)
        {
            _taskRepository = taskRepository;
            _distributedCache = distributedCache;
            _whatsAppService = whatsAppService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<Tasks>> Handle(GetAllTasksQuery req,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching tasks - Page: {PageNumber}, Size: {PageSize}", req.PageNumber, req.PageSize);

            var cacheKey = $"tasks_{req.PageNumber}_{req.PageSize}";
            var cachedTasks = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedTasks))
            {
                _logger.LogInformation("استرجاع المهام من ذاكرة التخزين المؤقت باستخدام المفتاح: {CacheKey}", cacheKey);

                return JsonSerializer.Deserialize<List<Tasks>>(cachedTasks);
            }

            _logger.LogInformation("لم يتم العثور على المهام في ذاكرة التخزين المؤقت. استرجاع من المستودع.");

            var tasks = await _taskRepository.GetAllTasksAsync(req.PageNumber, req.PageSize);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) 
            };

            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(tasks), cacheOptions, cancellationToken);
            _logger.LogInformation("تم تخزين المهام في ذاكرة التخزين المؤقت باستخدام المفتاح: {CacheKey}", cacheKey);
            string phoneNumber = _configuration["WhatsAppSettings:Phone"];
            _whatsAppService.SendWhatsAppMessage(phoneNumber, "Test");
            _logger.LogInformation("تم إرسال رسالة WhatsApp.");


            return tasks.ToList();
        }
    }
}
