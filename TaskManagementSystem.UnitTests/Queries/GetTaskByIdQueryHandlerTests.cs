using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Commands.Handlers;
using TaskManagementSystem.Application.Queries.Classess;
using TaskManagementSystem.Application.Queries.Handlers;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructures.Persistence;
using Xunit;

namespace TaskManagementSystem.UnitTests.Queries
{
    public class GetTaskByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnTask_WhenTaskExists()
        {
            // Arrange
            //var options = new DbContextOptionsBuilder<AppDbContext>()
            //                .UseInMemoryDatabase(databaseName:"TestDb")
            //                .Options;
            //using(var ctx = new AppDbContext(options))
            //{
            //    ctx.Tasks.Add(new Tasks { Id=2,FullName="Task1",Description="DESC",UserId="1" });
            //    await ctx.SaveChangesAsync();
            //}

            //using (var ctx = new AppDbContext(options))
            //{
            //    var handler = new GetTaskByIdQueryHandler(ctx);
            //    var query = new GetTaskByIdQuery { Id = 2 };
            //    // Act
            //    var result = await handler.Handle(query,CancellationToken.None);

            //    // Assert
            //    Assert.NotNull(result);
            //    Assert.Equal("Task1",result.FullName);
            //    Assert.Equal("desc".ToUpper(),result.Description);
            //}


        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenTaskNotExist()
        {
            //var options = new DbContextOptionsBuilder<AppDbContext>()
            //                .UseInMemoryDatabase(databaseName: "Test")
            //                .Options;
            //using (var ctx = new AppDbContext(options))
            //{
            //    ctx.Tasks.Add(new Tasks { Id = 10,FullName="Task10",Description="Desc For Task 10",UserId="2"});
            //    await ctx.SaveChangesAsync();

            //    var handler = new GetTaskByIdQueryHandler(ctx);
            //    var query = new GetTaskByIdQuery {  Id = 1 };
            //    //Act
            //    var result = await handler.Handle(query,CancellationToken.None);

            //    // Assert
            //    Assert.Null(result);
            //}

        }
    }
}
