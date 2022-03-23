using Wallet.Domain.Entities.User;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;

namespace Wallet.Infra;

public class Session : ISession

{
    private readonly IUserRepository _userRepository;
    private static Guid _userId;
    public User User => _userRepository.GetById(_userId).Result; 
        
    
    public Session(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Load(Guid userId)
    {
        _userId = userId;
    }

}
