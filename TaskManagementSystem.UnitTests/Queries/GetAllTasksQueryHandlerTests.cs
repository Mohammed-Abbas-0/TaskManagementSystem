using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Queries.Classess;
using TaskManagementSystem.Application.Queries.Handlers;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructures.Persistence;
using Xunit;

namespace TaskManagementSystem.UnitTests.Queries
{
 

    public class GetAllTasksQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnListOfTasks()
        {
            // 🛠️ Arrange - تجهيز البيانات المطلوبة
        //    var tasks = new List<Tasks>
        //{
        //    new Tasks { Id = 1, FullName = "Task 1", Description = "Description 1", UserId = "1" },
        //    new Tasks { Id = 2, FullName = "Task 2", Description = "Description 2", UserId = "2" }
        //}.AsQueryable(); // تحويل القائمة لـ IQueryable علشان نقدر نعمل Mock لـ DbSet

            // 📌 إنشاء Mock للـ DbSet<Tasks>
            //var mockDbSet = new Mock<DbSet<Tasks>>();
            //mockDbSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(tasks.Provider);
            //mockDbSet.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(tasks.Expression);
            //mockDbSet.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(tasks.ElementType);
            //mockDbSet.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(tasks.GetEnumerator());

            // 🚀 دعم الـ Asynchronous Queries
            
            //var mockAsyncEnumerable = tasks.ToAsyncEnumerable().GetAsyncEnumerator();
            //mockDbSet.As<IAsyncEnumerable<Tasks>>()
            //    .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            //    .Returns(mockAsyncEnumerable);
            //// 📌 إنشاء Mock لـ DbContext
            //var options = new DbContextOptionsBuilder<AppDbContext>()
            //    .UseInMemoryDatabase(databaseName: "TestDatabase")
            //    .Options;
            //var mockDbContext = new Mock<AppDbContext>(options);
            //mockDbContext.Setup(c => c.Tasks).Returns(mockDbSet.Object);



            /*

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new AppDbContext(options);
            var tasks = new List<Tasks>{
                new Tasks { Id = 1, FullName = "Task 1", Description = "Description 1", UserId = "1" },
                new Tasks { Id = 2, FullName = "Task 2", Description = "Description 2", UserId = "2" }
            };
            context.Tasks.AddRange(tasks);
            context.SaveChanges();


            var mockCache = new Mock<IDistributedCache>();
            var cacheKey = $"tasks_{1}_{10}";
            var cachedTasks = System.Text.Json.JsonSerializer.Serialize(tasks);

            // 🎯 إنشاء الـ Query Handler
            var handler = new GetTasksQueryHandler(context,mockCache.Object);
            var query = new GetAllTasksQuery();

            // 🚀 Act - تنفيذ العملية المطلوبة
            var result = await handler.Handle(query, CancellationToken.None);

            // ✅ Assert - التحقق من النتيجة
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // المفروض يرجع عدد المهام = 2
            */
        }

    }

}

