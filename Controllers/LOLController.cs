using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NON.EXE.Data;
using NON.EXE.Models;
using OfficeOpenXml;
using OpenXmlPowerTools;
using System.Data;
using System.Drawing;
using System.Text;

namespace NON.EXE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LOLController : ControllerBase
    {
        private readonly DataContext _context;


        public LOLController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<LOL>>> Get()
        {
            return Ok(await _context.LOLs.ToListAsync());
        }
        [HttpPost("UploadFile")]
        public async Task<ActionResult<List<LOL>>> UploadFile(IFormFile files)
        {
            var uploadResult = new List<LOL>();
            using (var stream = new MemoryStream())
            {
                await files.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;
                    StringBuilder sb = new StringBuilder();

                    for (int row = 1; row <= rowCount; row++)
                    {
                        var cac = new LOL
                        {
                            Team = worksheet.Cells[row, 2].Value.ToString(),
                            Top = worksheet.Cells[row, 3].Value.ToString(),
                            Jungle = worksheet.Cells[row, 4].Value.ToString(),
                            Mid = worksheet.Cells[row, 5].Value.ToString(),
                            Adc = worksheet.Cells[row, 6].Value.ToString(),
                            Sp = worksheet.Cells[row, 7].Value.ToString()
                        };
                        uploadResult.Add(cac);
                    }
                }
            }
            await _context.LOLs.AddRangeAsync(uploadResult);
            await _context.SaveChangesAsync();

            return Ok(await _context.LOLs.ToListAsync());
        }
        [HttpGet("ExportToExcel")]
        public async Task<IActionResult> ExportEmployeesToExcel()
        {
            try
            {
                List<LOL> employees = await _context.LOLs.ToListAsync();
                FileStreamResult fr = ExportToExcel.CreateExcelFile.StreamExcelDocument
                                     (employees, "LOL.xlsx");
                return fr;
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
