using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class OperationRepository : IOperationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IBankAccountRepository _bankAccountRepository;

        public OperationRepository(DataContext context, IMapper mapper, IBankAccountRepository bankAccountRepository)
        {
            _context = context;
            _mapper = mapper;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task AddOperationAsync(Operation operation)
        {
            await _context.Operations.AddAsync(operation);
            await SetBankAccountLastActiveAsToday(operation.BankAccountId);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOperationAsync(Operation operation)
        {
            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
        }

        public async Task<Operation> GetOperationAsync(int operationId)
        {
            return await _context.Operations.SingleOrDefaultAsync(o => o.Id == operationId);
        }

        public async Task<IEnumerable<Operation>> GetOperationsAsync(int bankAccountId)
        {
            return await _context.Operations.Where(o => o.BankAccountId == bankAccountId)
                                                                          .ToListAsync();
        }

        public async Task<PagedList<OperationDto>> GetPaginatedOperationAsync(OperationParams operationParams)
        {
            var query = _context.Operations.Where(o => o.BankAccountId == operationParams.bankAccountId);
            var paginatedData = _mapper.ProjectTo<OperationDto>(query);

            return await PagedList<OperationDto>.CreateAsync(paginatedData, operationParams.PageNumber, operationParams.PageSize);
        }

        public async Task UpdateOperationAsync(Operation operation)
        {
            _context.Operations.Update(operation);
            await SetBankAccountLastActiveAsToday(operation.BankAccountId);
            await _context.SaveChangesAsync();
        }

        public async Task SetBankAccountLastActiveAsToday(int bankAccountId)
        {
            await _bankAccountRepository.UpdateLastActiveAsync(bankAccountId);
        }
    }
}
