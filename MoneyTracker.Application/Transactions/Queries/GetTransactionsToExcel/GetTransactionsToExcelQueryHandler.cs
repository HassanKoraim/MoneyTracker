using MediatR;
using MoneyTracker.Application.DTOs;
using OfficeOpenXml;

namespace MoneyTracker.Application.Transactions.Queries.GetTransactionsToExcel
{
    public class GetTransactionsToExcelQueryHandler : IRequestHandler<GetTransactionsToExcelQuery, MemoryStream>
    {
        public async Task<MemoryStream> Handle(GetTransactionsToExcelQuery request, CancellationToken cancellationToken)
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
                foreach (var dto in request.transactionDtos)
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
    }
}
