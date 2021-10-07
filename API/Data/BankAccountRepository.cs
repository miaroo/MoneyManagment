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
using System.Collections;

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

        public async Task<IEnumerable<BankAccountDto>> GetBankAccountsAsync(int userId)
        {

            var query =  await _context.BankAccounts
                .Where(b => b.AppUserId == userId)
                .Select(x => new BankAccountDto
                {
                 Id = x.Id,
                 AppUserId = x.AppUserId,
                 Name = x.Name,
                 LastActive = x.LastActive,
                 Operations = x.Operations.Select(c => new OperationDto {
                    Id = c.Id,
                    Description = c.Description,
                    Amount = c.Amount,
                    Date = c.Date,
                    Name = c.Name,
                    CategoryId = c.CategoryId,
                    BankAccountId = c.BankAccountId,
                 }).OrderByDescending(o => o.Id).Take(3),
                 })
                .ToListAsync();

            return query;
        }

        public async Task<PagedList<BankAccountDto>> GetPaginatedBankAccountsAsync(UserParams userParams, int appUserId)
        {
            //var query = _context.BankAccounts
            //    .Where(b => b.AppUserId == appUserId);

            //query = userParams.OrderBy switch
            //{
            //    "date" => query.OrderByDescending(u => u.LastActive),
            //    _ => query.OrderByDescending(u => u.Name)
            //};
            var query = _context.BankAccounts
                .Where(b => b.AppUserId == appUserId)
                .Select(x => new BankAccountDto
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    Name = x.Name,
                    LastActive = x.LastActive,
                    Operations = x.Operations.Select(c => new OperationDto
                    {
                        Id = c.Id,
                        Description = c.Description,
                        Amount = c.Amount,
                        Date = c.Date,
                        Name = c.Name,
                        CategoryId = c.CategoryId,
                        BankAccountId = c.BankAccountId,
                    }).OrderByDescending(o => o.Id).Take(3),
                });

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
