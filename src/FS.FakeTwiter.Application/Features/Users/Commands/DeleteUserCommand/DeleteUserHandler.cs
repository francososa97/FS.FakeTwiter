using MediatR;

namespace FS.FakeTwiter.Application.Features.Users.Commands.DeleteUserCommand
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ApiResponse>
    {
        private readonly IUserService _userService;
        public DeleteUserHandler(IUserService userService) => _userService = userService;

        public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.DeleteAsync(request.Id);
            return result > 0
                ? new ApiResponse<string>("Usuario eliminado exitosamente.")
                : new ApiResponse<string>("No se pudo eliminar el usuario.") { Success = false };
        }
    }
}
