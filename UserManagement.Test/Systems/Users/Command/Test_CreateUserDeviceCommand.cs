using FluentAssertions;
using IntegrationEvent;
using MediatR;
using Moq;
using UserManagement.Application.Interfaces;
using UserManagement.Application.UseCases.Users.Commands;
using UserManagement.Domain.DTO;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Exceptions;

namespace UserManagement.Test.Systems.Users.Command
{
    public  class Test_CreateUserDeviceCommand
    {
        //should return createdevicedto
        [Fact]
        public async Task CreateUserDeviceCommand_Should_ReturnCreatedDeviceDTO_WhenSuccess()
        {
            //arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockQueueService = new Mock<IServiceBusQueueService>();
            var command = new CreateUserDeviceCommand(new CreateUserDeviceDTO { UserId = 1, DeviceName = "Sample Device", });
            mockRepo.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            var sut = new CreateUserDeviceCommandHandler(mockRepo.Object, mockQueueService.Object);

            //act
            var result = await sut.Handle(command, default);

            //assert
            result.Should().BeOfType<CreateUserDeviceDTO>();
        }

        //should throw custom error if user is not found
        [Fact]
        public async Task CreateUserDeviceCommand_Should_ThrowUserNotFoundException_WhenUserNotFound()
        {
            //arrange
            var userList = new List<User>();
            var mockRepo = new Mock<IUserRepository>();
            var mockQueueService = new Mock<IServiceBusQueueService>();
            var command = new CreateUserDeviceCommand(new CreateUserDeviceDTO { UserId = 1, DeviceName = "Sample Device", });
            mockRepo.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => userList.FirstOrDefault(u => u.Id == id));
            var sut = new CreateUserDeviceCommandHandler(mockRepo.Object, mockQueueService.Object);
            var action = async() => await sut.Handle(command, default);

            //act


            //assert
            await action.Should().ThrowAsync<UserNotFoundException>();
        }

        //should use repo getuserbyid
        [Fact]
        public async Task CreateUserDeviceCommand_Should_UseRepoGetUserById_WhenSuccess()
        {
            //arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockQueueService = new Mock<IServiceBusQueueService>();
            var command = new CreateUserDeviceCommand(new CreateUserDeviceDTO { UserId = 1, DeviceName = "Sample Device", });
            mockRepo.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User { Id = 1, FirstName= "Ludwig Ivan", LastName = "Comiso"}).Verifiable();
            var sut = new CreateUserDeviceCommandHandler(mockRepo.Object, mockQueueService.Object);

            //act
            var result = await sut.Handle(command, default);


            //assert
            mockRepo.Verify();
        }

        //should use servicequeue send method
        [Fact]
        public async Task CreateUserDeviceCommand_Should_UseServiceQueueSendMethod_WhenSuccess()
        {
            //arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockServiceBusService = new Mock<IServiceBusQueueService>();
            var command = new CreateUserDeviceCommand(new CreateUserDeviceDTO { UserId = 1, DeviceName = "Sample Device", });
            mockServiceBusService.Setup(x => x.SendMessageToQueue(It.IsAny<CreateUserDeviceIntegrationEvent>(), It.IsAny<string>()))
                .Callback<CreateUserDeviceIntegrationEvent, string>((_, queueName) => queueName = "samplequeue").Returns(Task.CompletedTask).Verifiable();
            mockRepo.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User { Id = 1, FirstName = "Ludwig Ivan", LastName = "Comiso" });
            var sut = new CreateUserDeviceCommandHandler(mockRepo.Object, mockServiceBusService.Object);

            //act
            var result = await sut.Handle(command, default);


            //assert
            mockServiceBusService.Verify();
        }
    }




}
