using IntegrationEvent;
using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.UseCases.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        readonly IUserRepository _repo;

        public CreateUserCommandHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                FirstName = request.user.FirstName,
                LastName = request.user.LastName
            };

            var result = await _repo.CreateUserAsync(newUser);

            return result;
        }
    }
}
