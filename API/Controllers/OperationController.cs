using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class OperationController : BaseApiController
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IMapper _mapper;

        public OperationController(IOperationRepository operationRepository, IMapper mapper)
        {
            _operationRepository = operationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationDto>>> GetUserOperations([FromQuery] OperationParams operationParams)
        {
            if (operationParams.Pagination != true)
            {
                var operationsWithoutPagination = await _operationRepository.GetOperationsAsync(operationParams.bankAccountId);
                return Ok(_mapper.Map<IEnumerable<BankAccountDto>>(operationsWithoutPagination));

            }
            var operations = await _operationRepository.GetPaginatedOperationAsync(operationParams);
            Response.AddPaginationHeader(operations.CurrentPage, operations.PageSize, operations.TotalCount, operations.TotalPages);

            return Ok(operations);
        }

        [HttpPost]
        public async Task<ActionResult<OperationDto>> CreateOperation(CreateOperationDto createOperationDto)
        {

            var newOperation = new Operation
            {
                 Description = createOperationDto.Description,
                 Amount = createOperationDto.Amount,
                 Date = createOperationDto.Date,
                 Name = createOperationDto.Name,
                 CategoryId = createOperationDto.CategoryId,
                 BankAccountId = createOperationDto.BankAccountId
            };
            await _operationRepository.AddOperationAsync(newOperation);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<OperationDto>> UpdateOperation(OperationDto operationDto)
        {
            var operation = new Operation
            {
                Id = operationDto.Id,
                Description = operationDto.Description,
                Amount = operationDto.Amount,
                Date = operationDto.Date,
                Name = operationDto.Name,
                CategoryId = operationDto.CategoryId,
                BankAccountId = operationDto.BankAccountId
            };
            await _operationRepository.UpdateOperationAsync(operation);

            return Ok();
        }

        [HttpDelete("{deleteOperationId}")]
        public async Task<ActionResult> DeleteCategory(int deleteCategoryId)
        {
            var category = await _operationRepository.
                GetOperationAsync(deleteCategoryId);
            if (category == null) return BadRequest("Couldnt find your category");
            await _operationRepository.DeleteOperationAsync(category);
            return Ok();
        }
    }
}

