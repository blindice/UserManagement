using MediatR;
using UserManagement.Domain.DTO;

namespace UserManagement.Application.UseCases.Users.Commands
{
    public record CreateUserCommand(CreateUserDTO user): IRequest<int>
    {
    }
}
