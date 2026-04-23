using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;
using MoneyTracker.Application.ServiceContracts;
using MoneyTracker.Application.DTOs;
using MoneyTracker.Domain.Entities;

namespace MoneyTracker.API.Controllers
{
    [Route("api/TransactionApi")]
    [ApiController]
    public class TransactionAPIController : Controller
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
        [HttpGet("GetAll/{sortBy?}/{sortOrder?}")]
      //  [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTransactions(string? sortBy = null,string? sortOrder= null)
        {
            var transactionDtos = await _transactionService.GetAll(null, sortBy, sortOrder);
            return Ok(transactionDtos);
        }
        [HttpGet("GetAmount")]
 //       [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> Delete(int id)
        {
            bool IsDelete = await _transactionService.Delete(id);
            return IsDelete;
        }

        [HttpGet("transactionsExcel/{sortBy?}/{sortOrder?}")]
        public async Task<IActionResult> TranstactionsExcel(string sortBy=null,string sortOrder = null)
            {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var transactionList = await _transactionService.GetAll(null, sortBy,sortOrder);
                //JsonSerializer.Deserialize<List<TransactionDto>>(transactions, options);

            MemoryStream memoryStream = await _transactionService.GetTransactionsExcel(transactionList);
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "transactions.xlsx");
        }

    }
}
