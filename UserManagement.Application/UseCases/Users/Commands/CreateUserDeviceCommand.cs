using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.DTO;

namespace UserManagement.Application.UseCases.Users.Commands
{
    public record CreateUserDeviceCommand(CreateUserDeviceDTO user) : IRequest<CreateUserDeviceDTO>
    {
    }
}
