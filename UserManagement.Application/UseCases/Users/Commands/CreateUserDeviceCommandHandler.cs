using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.DTO;
using UserManagement.Domain.Exceptions;

namespace UserManagement.Application.UseCases.Users.Commands
{
    public class CreateUserDeviceCommandHandler : IRequestHandler<CreateUserDeviceCommand, CreateUserDeviceDTO>
    {
        readonly IUserRepository _userRepository;
        readonly IServiceBusQueueService _serviceBusQueueService;

        public CreateUserDeviceCommandHandler(IUserRepository userRepository, IServiceBusQueueService serviceBusQueueService)
        {
            _userRepository = userRepository;
            _serviceBusQueueService = serviceBusQueueService;
        }

        public async Task<CreateUserDeviceDTO> Handle(CreateUserDeviceCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.user.UserId);

            if (user is null)
                throw new UserNotFoundException();

            await _serviceBusQueueService.SendMessageToQueue(request.user, "samplequeue");

            return request.user;
        }
    }
}
