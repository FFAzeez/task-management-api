using AutoMapper;
using Moq;
using TeamTaskManagementAPI.Business.Helper;
using TeamTaskManagementAPI.Business.Services.Implementations.Tasks;
using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.Models;
using TeamTaskManagementAPI.Infrastructure.Persistence.Repositories;
using Xunit;

namespace TeamTaskManagementAPI.Test;

public class TaskServiceTests
    {
        private readonly Mock<IAsyncRepository<TaskModel>> _taskRepositoryMock;
        private readonly Mock<IAsyncRepository<TeamUser>> _teamUserRepositoryMock;
        private readonly TaskService _taskService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserService> _userServiceMock;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<IAsyncRepository<TaskModel>>();
            _teamUserRepositoryMock = new Mock<IAsyncRepository<TeamUser>>();
            _mapperMock = new Mock<IMapper>();
            _userServiceMock = new Mock<IUserService>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _teamUserRepositoryMock.Object,_mapperMock.Object,_userServiceMock.Object);
        }

        [Fact]
        public async Task CreateTaskAsync_ValidInput_ReturnsTaskDto()
        {
            // Arrange
            var teamId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var taskDto = new CreateTaskRequest()
            {
                Title = "Test Task",
                Description = "Description",
                DueDate = DateTime.UtcNow.AddDays(1)
            };
            _teamUserRepositoryMock.Setup(r => r.GetSingleAsync(_=>_.UserId ==userId && _.TeamId == teamId));
            _taskRepositoryMock.Setup(r => r.AddAsync(It.IsAny<TaskModel>())).Returns((Task<TaskModel>)Task.CompletedTask);

            // Act
            var result = await _taskService.CreateTaskAsync(teamId, taskDto);

            // Assert
            Assert.NotNull(result);
            _taskRepositoryMock.Verify(r => r.AddAsync(It.IsAny<TaskModel>()), Times.Once());
        }

        [Fact]
        public async Task CreateTaskAsync_Unauthorized_ThrowsException()
        {
            // Arrange
            var teamId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var taskDto = new CreateTaskRequest() { Title = "Test Task" };
            _teamUserRepositoryMock.Setup(r => r.GetSingleAsync(_=>_.UserId == userId && _.TeamId == teamId));

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _taskService.CreateTaskAsync(teamId, taskDto));
        }
    }