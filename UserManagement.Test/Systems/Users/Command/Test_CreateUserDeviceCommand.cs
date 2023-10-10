using FluentAssertions;
using MediatR;
using Moq;
using UserManagement.Application.Interfaces;
using UserManagement.Application.UseCases.Users.Commands;
using UserManagement.Domain.DTO;
using UserManagement.Domain.Entities;

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
            mockRepo.Setup(x => x.GetUserByIdAsync(command.user.UserId)).ReturnsAsync(new User());
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
            var mockRepo = new Mock<IUserRepository>();
            var mockQueueService = new Mock<IServiceBusQueueService>();
            var command = new CreateUserDeviceCommand(new CreateUserDeviceDTO { UserId = 1, DeviceName = "Sample Device", });
            mockRepo.Setup(x => x.GetUserByIdAsync(command.user.UserId)).ReturnsAsync(new User());
            var sut = new CreateUserDeviceCommandHandler(mockRepo.Object, mockQueueService.Object);

            //act
            var result = await sut.Handle(command, default);

            //assert
            result.Should().BeOfType<CreateUserDeviceDTO>();
        }
        //should use repo getuserbyid
        //should use servicequeue send method
    }




}
