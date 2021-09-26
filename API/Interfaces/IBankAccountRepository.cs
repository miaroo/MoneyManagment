using API.Entities;
using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IBankAccountRepository
    {
        Task AddBankAccountAsync(BankAccount bankAccount);
        Task<IEnumerable<BankAccount>> GetBankAccountsAsync(int userId);
        Task UpdateBankAccountAsync(BankAccount bankAccount);
        Task<BankAccount> GetBankAccountAsync(int bankAccountId);
        Task DeleteBankAccountAsync(BankAccount bankAccount);
        Task<PagedList<BankAccountDto>> GetPaginatedBankAccountsAsync(UserParams userParams, int appUserId);
        Task UpdateLastActiveAsync(int bankAccountId);
    }
}
