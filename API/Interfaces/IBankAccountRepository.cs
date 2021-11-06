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
        Task<int> AddBankAccountAsync(BankAccount bankAccount);
        Task<IEnumerable<BankAccountDto>> GetBankAccountsAsync(int userId);
        Task UpdateBankAccountAsync(BankAccount bankAccount);
        Task<BankAccount> GetBankAccountAsync(int bankAccountId);
        Task DeleteBankAccountAsync(BankAccount bankAccount);
        Task<PagedList<BankAccountDto>> GetPaginatedBankAccountsAsync(BankAccountParams bankAccountParams, int appUserId);
        Task UpdateLastActiveAsync(int bankAccountId);
    }
}
