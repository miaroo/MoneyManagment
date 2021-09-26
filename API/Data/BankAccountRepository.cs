using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BankAccountRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddBankAccountAsync(BankAccount bankAccount)
        {
            await _context.BankAccounts.AddAsync(bankAccount);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBankAccountAsync(BankAccount bankAccount)
        {
            _context.Remove(bankAccount);
            await _context.SaveChangesAsync();
        }

        public async Task<BankAccount> GetBankAccountAsync(int bankAccountId)
        {
            return await _context.BankAccounts
                .SingleOrDefaultAsync(c => c.Id == bankAccountId);
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync(int userId)
        {
            return await _context.BankAccounts
                .Where(b => b.AppUserId == userId).ToListAsync();
        }

        public async Task<PagedList<BankAccountDto>> GetPaginatedBankAccountsAsync(UserParams userParams, int appUserId)
        {
            var query = _context.BankAccounts
                .Where(b => b.AppUserId == appUserId);

            query = userParams.OrderBy switch
            {
                "date" => query.OrderByDescending(u => u.LastActive),
                _ => query.OrderByDescending(u => u.Name)
            };

            var paginatedData = _mapper.ProjectTo<BankAccountDto>(query);

            return await PagedList<BankAccountDto>.CreateAsync(paginatedData, userParams.PageNumber, userParams.PageSize);
        }

        public async Task UpdateBankAccountAsync(BankAccount bankAccount)
        {
            _context.BankAccounts.Update(bankAccount);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLastActiveAsync(int bankAccountId)
        {
            var bankAccount = await _context.BankAccounts
                .SingleOrDefaultAsync(c => c.Id == bankAccountId);
            bankAccount.LastActive = DateTime.Today;
            await UpdateBankAccountAsync(bankAccount);
        }
    }
}
