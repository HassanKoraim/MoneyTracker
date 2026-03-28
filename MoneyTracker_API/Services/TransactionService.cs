using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;
using MoneyTracker_API.RepositoryContracts;
using MoneyTracker_API.ServiceContracts;
using OfficeOpenXml;
using System.Linq.Expressions;

namespace MoneyTracker_API.Services
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _repo;
        private IMapper _mapper;
        public TransactionService(ITransactionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<TransactionDto> GetById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            Transaction? transaction = await _repo.Get(t => t.Id == id);
            if(transaction == null)
            {
                throw new ArgumentNullException("The Transaction Not Found");
            }
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return transactionDto;
        }
        public async Task<List<TransactionDto>> GetAll(Expression<Func<Transaction, bool>> predicate = null, string? sortBy = null, string? sortOrder = null)
        {
            var transactionList = await _repo.GetTransactions(predicate, "Category,PaymentMethod");
            if (transactionList == null)
            {
                return new List<TransactionDto>();
            }
            var transactionDtoList = _mapper.Map<List<TransactionDto>>(transactionList);
            if(sortBy != null && sortOrder != null)
            {
                transactionDtoList = Sort(sortBy, sortOrder, transactionDtoList);
            }
            // Don't sort by default, let the caller decide if they want to sort or not
            //else
            //{
            //    transactionDtoList = Sort(nameof(Transaction.TransactionDate), "desc", transactionDtoList);
            //}
            return transactionDtoList;
        }
        public async Task<TransactionDto> Create(TransactionCreateDto transactionCreateDto)
        {
            if(transactionCreateDto == null)
            {
                throw new ArgumentNullException(nameof(transactionCreateDto));
            }
            if(transactionCreateDto.Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            if(transactionCreateDto.CategoryId <= 0)
            {
                throw new ArgumentException("CategoryId must be greater than zero");
            }
            if (transactionCreateDto.PaymentMethodId <= 0)
            {
                throw new ArgumentException("PaymentMethodId must be greater than zero");
            }
            Transaction transaction = _mapper.Map<Transaction>(transactionCreateDto);
            var transactionCreated = await _repo.Create(transaction);
            return _mapper.Map<TransactionDto>(transactionCreated);
        }

        public async Task<bool> Delete(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            var transaction = await _repo.Get(t => t.Id == id);
            if (transaction == null)
            {
                throw new ArgumentNullException("We can't found the Transaction");
            }
            return await _repo.Delete(transaction);
        }
        public async Task<TransactionDto> Update(int id, TransactionUpdateDto transactionUpdateDto)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id can't be less than or equal Zero");
            }
            Transaction? transaction = await _repo.Get(t => t.Id == id);
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            Transaction transactionFromDto = _mapper.Map<Transaction>(transactionUpdateDto);
            Transaction transactionUpdated = await _repo.Update(id,transactionFromDto);
            TransactionDto transactionDto = _mapper.Map<TransactionDto>(transactionUpdated);
            return transactionDto;
        }

        public async Task<decimal> GetAmount(Expression<Func<Transaction,bool>> filter = null , string transactionType = null)
        {
             return await _repo.GetAmount(filter, transactionType);
        }
        //public async Task<MemoryStream> GetTransactionsExcel(List<TransactionDto> transactionDtos)
        //{
        //    //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    // Use this for EPPlus 8+
        //    ExcelPackage.License.SetLicenseValue(OfficeOpenXml.LicenseContext.NonCommercial);
        //    MemoryStream memoryStream = new MemoryStream();
        //    using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
        //    {
        //        ExcelWorksheet worksheet =
        //            excelPackage.Workbook.Worksheets.Add("TransactionSheet");
        //        worksheet.Cells["A1"].Value = "Id";
        //        worksheet.Cells["B1"].Value = "Amount";
        //        worksheet.Cells["C1"].Value = "Description";
        //        worksheet.Cells["D1"].Value = "Transaction Date";
        //        worksheet.Cells["E1"].Value = "Category Name";
        //        worksheet.Cells["F1"].Value = "Payment Method Name";
        //        worksheet.Cells["G1"].Value = "Recurrence Type";
        //      //  worksheet.Cells["H1"].Value = "Receive News Letters";
        //        using (ExcelRange headerCells = worksheet.Cells["A1:G1"])
        //        {
        //            headerCells.Style.Fill.PatternType =
        //                OfficeOpenXml.Style.ExcelFillStyle.Solid;
        //            headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        //            headerCells.Style.Font.Bold = true;
        //        }
        //        int row = 2;
        //        /*List<PersonResponse> persons = 
        //            _db.Persons.Include("Country").Select(temp => temp.ToPersonResponse()).ToList();*/
        //        foreach (TransactionDto transactionDto in transactionDtos)
        //        {
        //            // worksheet.Cells[$"A{row}"].Value = person.PersonName;
        //            // Cells[numberOfRow, NumberOfColumn]
        //            worksheet.Cells[row, 1].Value = transactionDto.Id;
        //            worksheet.Cells[row, 2].Value = transactionDto.Amount;
        //            if (!transactionDto.Description.IsNullOrEmpty())
        //                worksheet.Cells[row, 3].Value = transactionDto.Description;
        //            else
        //                worksheet.Cells[row, 3].Value = "";
        //            worksheet.Cells[row, 3].Value = transactionDto.TransactionDate.ToString("yyyy-MM-dd");
        //            worksheet.Cells[row, 5].Value = transactionDto.CategoryName;
        //            worksheet.Cells[row, 6].Value = transactionDto.PaymentMethodName;
        //            worksheet.Cells[row, 7].Value = transactionDto.RecurrenceType;
        //        //    worksheet.Cells[row, 8].Value = transactionDto.ReceiveNewsLetter;
        //            row++;
        //        }

        //        worksheet.Cells[$"A1:H{row}"].AutoFitColumns();
        //        await excelPackage.SaveAsync();
        //        memoryStream.Position = 0;
        //        return memoryStream;
        //    }
        //}

        public async Task<MemoryStream> GetTransactionsExcel(List<TransactionDto> transactionDtos)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Hassan");
            var memoryStream = new MemoryStream();

            // 3. Create package (Stay with the empty constructor)
            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("TransactionSheet");

                // Headers
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Amount";
                worksheet.Cells["C1"].Value = "Description";
                worksheet.Cells["D1"].Value = "Transaction Date";
                worksheet.Cells["E1"].Value = "Category Name";
                worksheet.Cells["F1"].Value = "Payment Method Name";
                worksheet.Cells["G1"].Value = "RecurrenceType";
                worksheet.Cells["H1"].Value = "RecurrenceEndDate";


                // 4. Loop through your data to fill the rows
                int row = 2;
                foreach (var dto in transactionDtos)
                {
                    worksheet.Cells[row, 1].Value = dto.Id;
                    worksheet.Cells[row, 2].Value = dto.Amount;
                    worksheet.Cells[row, 3].Value = dto.Description ?? "";
                    worksheet.Cells[row, 4].Value = dto.TransactionDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 5].Value = dto.CategoryName;
                    worksheet.Cells[row, 6].Value = dto.PaymentMethodName;
                    worksheet.Cells[row, 7].Value = dto.RecurrenceType;
                    worksheet.Cells[row, 8].Value = dto.RecurrenceEndDate;
                    row++;
                }
                worksheet.Cells.AutoFitColumns();
                excelPackage.SaveAs(memoryStream);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        public List<TransactionDto> Sort(string sortBy, string sortOrder, List<TransactionDto> transactions)
        {
            // 1. Validation: If list is null or empty, return as is
            if (transactions == null || !transactions.Any()) return transactions;

            // 2. Normalize inputs to avoid case-sensitivity issues
           // string sort = sortBy?.ToLower();
            string order = sortOrder?.ToLower();

            // 3. Return the sorted list
            return (sortBy, order) switch
            {
                (nameof(Transaction.Amount), "asc") => transactions.OrderBy(t => t.Amount).ToList(),
                (nameof(Transaction.Amount), "desc") => transactions.OrderByDescending(t => t.Amount).ToList(),
                (nameof(Transaction.TransactionDate), "asc") => transactions.OrderBy(t => t.TransactionDate).ToList(),
                (nameof(Transaction.TransactionDate), "desc") => transactions.OrderByDescending(t => t.TransactionDate).ToList(),
                _ => transactions // Default: No sorting
            };
        }
    }
}
