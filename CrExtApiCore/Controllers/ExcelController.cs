using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CrExtApiCore.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    [Route("api/excel")]
    public class ExcelController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private CrExtContext _context;

        public ExcelController(IHostingEnvironment hostingEnvironment, CrExtContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [DisableRequestSizeLimit]
        [HttpPost("Import")]
        public  IActionResult CreateImportUrl()
        {
            var file = Request.Form.Files[0];
            string folderName = "ExcelFiles";
            string webRootPath = _hostingEnvironment.WebRootPath;
            var teamId = Convert.ToInt16(Request.Form["teamId"]);
            List<Customers> customers = new List<Customers>();
            string newPath = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }

                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);

                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        var newC = new Customers();

                        newC.AccountName = row.GetCell(0).ToString();
                        newC.NUBAN_NUMBER = row.GetCell(1).ToString();
                        newC.PhoneNumber = row.GetCell(2).ToString();
                        newC.OutstandingBalance = row.GetCell(3).ToString();
                        newC.BalanceIssued = row.GetCell(4).ToString();
                        newC.Interest = row.GetCell(5).ToString();
                        newC.Recommendation = row.GetCell(6).ToString();
                        newC.TeamId = teamId;
                        customers.Add(newC);
                       
                        //customers.Add(new Customers
                        //{
                        //    AccountName = row.GetCell(1).ToString(),
                        //    NUBAN_NUMBER = row.GetCell(2).ToString(),
                        //    PhoneNumber = row.GetCell(3).ToString(),
                        //    OutstandingBalance = Convert.ToDecimal(row.GetCell(4)),
                        //    BalanceIssued = Convert.ToDecimal(row.GetCell(5)),
                        //    Interest = row.GetCell(6).ToString(),
                        //    Recommendation = row.GetCell(7).ToString(),
                        //    TeamId = teamId

                        //});

                    }
                    if(customers != null)
                    {
                     _context.Customers.AddRange(customers);
                        _context.SaveChanges();
                        return Ok(customers);
                    }
               
                 
                }
             
            }

            return BadRequest("Something went wrong");
        }

     
    }
}