using Wallet.Domain.Entities.User;

namespace Wallet.Domain.Interfaces;

public interface ISession
{
    User User { get; }
}