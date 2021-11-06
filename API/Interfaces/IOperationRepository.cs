using API.DTOs;
using API.Entities;
using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IOperationRepository
    {
        Task<int> AddOperationAsync(Operation operation);
        Task<IEnumerable<Operation>> GetOperationsAsync(int bankAccountId);
        Task UpdateOperationAsync(Operation operation);
        Task<Operation> GetOperationAsync(int bankAccountId);
        Task DeleteOperationAsync(Operation operation);
        Task<PagedList<OperationDto>> GetPaginatedOperationAsync(OperationParams operationParams);
    }
}
