using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Application.UseCases.Users.Commands;
using UserManagement.Application.UseCases.Users.Queries;
using UserManagement.Domain.DTO;
using UserManagement.Domain.Entities;

namespace UserManagement.Test.Systems.Users.Command
{
    public  class Test_CreateUserCommand
    {
        //return the id of created user
        [Fact]
        public async Task CreateUserCommand_Should_ReturnIdOfTheCreatedUser_WhenSuccess()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var user = new CreateUserDTO { FirstName = "Ludwig Ivan", LastName = "Comiso" };
            var newUser = new User{Id=1, FirstName = user.FirstName, LastName = user.LastName};

            mockUserRepository.Setup(x => x.CreateUserAsync(newUser)).ReturnsAsync(newUser.Id);
            var command = new CreateUserCommand(user);
            var handler = new CreateUserCommandHandler(mockUserRepository.Object);

            //act
            var result = await handler.Handle(command, default);


            //assert
            result.Should().Be(newUser.Id);

        }

        //use repo createUser method once
        [Fact]
        public async Task CreateUserCommand_Should_UseRepoCreateUserMethod_WhenSuccess()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var user = new CreateUserDTO { FirstName = "Ludwig Ivan", LastName = "Comiso" };
            var newUser = new User {FirstName = user.FirstName, LastName = user.LastName };

            mockUserRepository.Setup(x => x.CreateUserAsync(newUser)).ReturnsAsync(1);
            var command = new CreateUserCommand(user);
            var handler = new CreateUserCommandHandler(mockUserRepository.Object);

            //act
            var result = await handler.Handle(command, default);

            //assert
            mockUserRepository.Verify(x => x.CreateUserAsync(It.Is<User>(y => y == newUser)), Times.Once());
        }
    }
}
