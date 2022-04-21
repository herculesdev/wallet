﻿using Wallet.Domain.Entities.Base;
using Wallet.Domain.Entities.User;
using Wallet.Domain.UseCases.Queries.Requests;
using Wallet.Domain.ValueObjects;

namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IUserRepository : IRepository<User>
{
    Task<bool> HasUserWithAsync(Guid id);
    Task<bool> HasUserWithAsync(DocumentNumber document, Password password);
    Task<bool> HasUserWithAsync(DocumentNumber document);
    Task<bool> HasUserWithEmailAsync(string email);
    Task<User> GetAsync(DocumentNumber document, Password password);
    Task<PagedResult<User>> GetAsync(GetAllUserQuery criteria);
}