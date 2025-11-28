using Microsoft.AspNetCore.Mvc;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using MoneyTracker_API.ServiceContracts;
using System.Linq.Expressions;

namespace MoneyTracker_API.Controllers
{
    [Route("api/TransactionApi")]
    [ApiController]
    public class TransactionAPIController :Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionAPIController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transactionDto = await _transactionService.GetById(id);
            return Ok(transactionDto);
        }
        // Get All
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactionDtos = await _transactionService.GetAll();
            return Ok(transactionDtos);
        }
        [HttpGet("GetAmount")]
        public async Task<IActionResult> GetAmount(int? id, string transactionType = null)
        {
            Expression<Func<Transaction,bool>> filter = null;
            if (id != null)
            {
                filter = t => t.Id == id;
            }
            var totalIncome = await _transactionService.GetAmount(filter,transactionType);
            return Ok(totalIncome);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionCreateDto transactionCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactionDto = await _transactionService.Create(transactionCreateDto);
            return Ok(transactionDto);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionUpdateDto transactionUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactionDto = await _transactionService.Update(id, transactionUpdateDto);
            return Ok(transactionDto);
        }
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            bool IsDelete = await _transactionService.Delete(id);
            return IsDelete;
        }
    }
}
