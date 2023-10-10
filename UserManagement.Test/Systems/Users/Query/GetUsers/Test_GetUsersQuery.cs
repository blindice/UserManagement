using FluentAssertions;
using Moq;
using UserManagement.Application.Interfaces;
using UserManagement.Application.UseCases.Users.Queries;
using UserManagement.Domain.Entities;

namespace UserManagement.Test.Systems.Users.Query.GetUsers
{
    public class Test_GetUsersQuery
    {
        [Fact]
        public async Task GetUsersQueryCommand_Should_ReturnUserList_WhenSuccess()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var users = new List<User>()
            {
                new User{Id = 1, FirstName = "Ludwig Ivan", LastName = "Comiso"}
            };

            mockUserRepository.Setup(x => x.GetUsersAsync()).ReturnsAsync(users);
            var command = new GetUsersQuery();
            var handler = new GetUsersQueryHandler(mockUserRepository.Object);

            //act
            var result = await handler.Handle(command, default);


            //assert
            result.Should().BeOfType(typeof(List<User>));
        }

        [Fact]
        public async Task GetUsersQueryCommand_Should_CallRepoGetUsers_WhenSuccess()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var users = new List<User>()
            {
                new User{Id = 1, FirstName = "Ludwig Ivan", LastName = "Comiso"}
            };

            mockUserRepository.Setup(x => x.GetUsersAsync()).ReturnsAsync(users);
            var command = new GetUsersQuery();
            var handler = new GetUsersQueryHandler(mockUserRepository.Object);

            //act
            var result = await handler.Handle(command, default);


            //assert
            mockUserRepository.Verify(x => x.GetUsersAsync(), Times.Once);
        }
    }


}
