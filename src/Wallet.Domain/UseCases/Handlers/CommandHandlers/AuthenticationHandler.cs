using MediatR;
using Wallet.Domain.Helpers;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Commands.Requests;
using Wallet.Domain.UseCases.Commands.Responses;
using Wallet.Domain.UseCases.Common.Handlers;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Handlers.CommandHandlers;

public class AuthenticationHandler : BaseHandler, IRequestHandler<AuthCommand, ResponseData<AuthResponse>>
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    
    public AuthenticationHandler(ITokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<ResponseData<AuthResponse>> Handle(AuthCommand command, CancellationToken cancellationToken)
    {
        var response = new ResponseData<AuthResponse>();

        if (command.IsInvalid)
            return response.AddNotifications(command);

        if (!await _userRepository.HasUserWithAsync(command.Document, command.Password))
            return response.AddNotification("Número de documento ou senha estão incorretos");

        var user = await _userRepository.GetAsync(command.Document, command.Password);
                
        return response.With(new AuthResponse
        {
            UserId = user.Id,
            UserToken = _tokenGenerator.Generate(user, Utils.TokenKey)
        });
    }
}