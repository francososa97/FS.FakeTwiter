using FS.FakeTwiter.Application.Interfaces.Users;
using FS.FakeTwiter.Application.Mappers;
using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.DeleteUserCommand
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUserService _userService;
        public DeleteUserHandler(IUserService userService) => _userService = userService;

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _userService.DeleteAsync(request.Id);
            //To do devlover el dto
            return response;
        }
    }
}
