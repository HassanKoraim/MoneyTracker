using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;
using MoneyTracker.Domain.Entities;
using MediatR;
using MoneyTracker.Application.Transactions.Queries.GetTransactionById;
using MoneyTracker.Application.Transactions.Queries.GetAllTransaction;
using MoneyTracker.Application.Transactions.Queries.GetAmount;
using MoneyTracker.Application.Transactions.Queries.GetTransactionsToExcel;
using MoneyTracker.Application.Transactions.Commands.CreateTransaction;
using MoneyTracker.Application.Transactions.Commands.DeleteTransactionById;
using MoneyTracker.Application.Transactions.Commands.UpdateTransaction;
using MoneyTracker.Application.DTOs.TransactionDTOs;

namespace MoneyTracker.API.Controllers
{
    [Route("api/TransactionApi")]
    [ApiController]
    public class TransactionAPIController : Controller
    {
        private readonly IMediator _mediater;
        public TransactionAPIController(IMediator mediator)
        {
            _mediater = mediator;
        }

        [HttpGet("GetTransactionById{id:int}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transactionDto = await _mediater.Send(new GetTransactionByIdQuery(id));
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
            var transactionDtos = await _mediater.Send(new GetAllTransactionQuery(null, sortBy, sortOrder));
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
            var totalIncome = await _mediater.Send( new GetAmountQuery(filter,transactionType));
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
            var transactionDto = await _mediater.Send(new CreateTransactionCommand(transactionCreateDto));
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
            var transactionDto = await _mediater.Send(new UpdateTransactionCommand(id, transactionUpdateDto));
            return Ok(transactionDto);
        }
        [HttpDelete("DeleteTransaction{id:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> DeleteTransaction(int id)
        {
            bool IsDelete = await _mediater.Send(new DeleteTransactionByIdCommand(id));
            return IsDelete;
        }

        [HttpGet("transactionsExcel/{sortBy?}/{sortOrder?}")]
        public async Task<IActionResult> TranstactionsExcel(string sortBy=null,string sortOrder = null)
            {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var transactionList = await _mediater.Send(new GetAllTransactionQuery(null, sortBy,sortOrder));
                //JsonSerializer.Deserialize<List<TransactionDto>>(transactions, options);

            MemoryStream memoryStream = await _mediater.Send(new GetTransactionsToExcelQuery(transactionList));
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "transactions.xlsx");
        }

    }
}
