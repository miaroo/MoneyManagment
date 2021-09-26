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
    public class BankAccountController : BaseApiController
    {
        private readonly IBankAccountRepository _bankAccountRepostiory;
        private readonly IMapper _autoMapper;

        public BankAccountController(IBankAccountRepository bankAccountRepostiory, IMapper autoMapper)
        {
            _bankAccountRepostiory = bankAccountRepostiory;
            _autoMapper = autoMapper;
        }
        [HttpPost]
        public async Task<ActionResult<BankAccountDto>> CreateBankAccountAsync(CreateBankAccountDto createBankAccountDto)
        {
            var userId = User.GetUserId();
            var newAccount = new BankAccount
            {
                AppUserId = userId,
                Name = createBankAccountDto.Name,
                LastActive = DateTime.Today
            };
            await _bankAccountRepostiory.AddBankAccountAsync(newAccount);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccountDto>>> GetUserBankAccounts([FromQuery]UserParams userParams)
        {
            var userId = User.GetUserId();
            if (userParams.Pagination != true)
            {
                var accountsWithoutPagination = await _bankAccountRepostiory.GetBankAccountsAsync(userId);
                return Ok(_autoMapper.Map<IEnumerable<BankAccountDto>>(accountsWithoutPagination));

            }
            var accounts = await _bankAccountRepostiory.GetPaginatedBankAccountsAsync(userParams, userId);
            Response.AddPaginationHeader(accounts.CurrentPage, accounts.PageSize, accounts.TotalCount, accounts.TotalPages);

            return Ok(accounts);
        }

        [HttpGet("{bankAccountId}")]
        public async Task<ActionResult<BankAccountDto>> GetUserBankAccount(int bankAccountId)
        {
            var account = await _bankAccountRepostiory.GetBankAccountAsync(bankAccountId);
            return Ok(account);
        }
        [HttpPut]
        public async Task<ActionResult<BankAccountDto>> UpdateBankAccount (BankAccountDto bankAccountDto)
        {
            var userId = User.GetUserId();
            if (bankAccountDto.AppUserId != userId) return BadRequest("This account doesnt belong to this user");
            var account = new BankAccount
            {
                Id = bankAccountDto.Id,
                AppUserId = bankAccountDto.AppUserId,
                Name = bankAccountDto.Name,
                LastActive = bankAccountDto.LastActive
            };
            await _bankAccountRepostiory.UpdateBankAccountAsync(account);

            return Ok();
        }
        [HttpDelete("{deleteAccountId}")]
        public async Task<ActionResult> DeleteBankAccount (int deleteAccountId)
        {
            var userId = User.GetUserId();
            var account = await _bankAccountRepostiory.GetBankAccountAsync(deleteAccountId);
            if (account == null) return BadRequest("Couldn't find such bank account");
            if (account.AppUserId != userId) return BadRequest("This account doesnt belong to this user");
            await _bankAccountRepostiory.DeleteBankAccountAsync(account);

            return Ok();
        }
      
    }
}
