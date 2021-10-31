using BCITechnicalTest.Models;
using ClosedXML.Excel;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BCITechnicalTest.Controllers
{
    public class KendaraanController : Controller
    {
        readonly KendaraanRepo repo = new KendaraanRepo();
        private IConverter _converter;
        public KendaraanController(IConverter converter)
        {
            _converter = converter;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetKendaraan()
        {
            var result = repo.GetKendaraan();
            return Json(result);
        }

        public IActionResult GetKendaraanById(int id)
        {
            var result = repo.Kendaraan(id);
            return Json(result);
        }

        public IActionResult SaveKendaraan(Kendaraan kendaraan)
        {
            string msg = "";
            var result = repo.SaveKendaraan(kendaraan);
            if(result == 0)
            {
                msg += "Something Wrong!!!";
            }
            else
            {
                msg += "Data Save Successfully";
            }

            return Json(msg);
        }

        public IActionResult Deletekendaraan(int id)
        {
            string msg = "";
            var result = repo.DeleteKendaraan(id);
            if(result == 0)
            {
                msg += "Something Wrong!!!";
            }
            else
            {
                msg += "Data Deleted Successfully";
            }

            return Json(msg);
        }


        public IActionResult ExportXlsx()
        {
            var fileFormat = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"Data-Kendaraan-{DateTime.Now:yyyyMMdd}.xlsx";

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = "No";
            worksheet.Cell(currentRow, 2).Value = "Nama Kendaraan";
            worksheet.Cell(currentRow, 3).Value = "Model Kendaraan";
            worksheet.Cell(currentRow, 4).Value = "Merek Kendaraan";
            worksheet.Cell(currentRow, 5).Value = "Transmisi Kendaraan";
            worksheet.Cell(currentRow, 6).Value = "Tahun Kendaraan";
            worksheet.Cell(currentRow, 7).Value = "Harga Kendaraan";

            int idx = 1;
            var result = repo.GetKendaraan();
            foreach (var data in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = idx++;
                worksheet.Cell(currentRow, 2).Value = data.Nama;
                worksheet.Cell(currentRow, 3).Value = data.Model;
                worksheet.Cell(currentRow, 4).Value = data.Merek;
                worksheet.Cell(currentRow, 5).Value = data.Transmisi;
                worksheet.Cell(currentRow, 6).Value = data.Tahun;
                worksheet.Cell(currentRow, 7).Value = data.Harga;
            }

            worksheet.Columns().AdjustToContents();
            

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, fileFormat, fileName);
        }

        public IActionResult ImportXlsx(IFormFile file)
        {
            var dt = new DataTable();
            var dir = Directory.GetCurrentDirectory();
            var doc = "\\wwwroot\\upload\\";
            //var fullPath = path + images;
            var path = dir + doc;
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
            var fullPath = path + fileName;

            //this method throw exception if you use same resource
            //file.CopyTo(new FileStream(Path.Combine(path + fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
            
            using(var fileStream = new FileStream(Path.Combine(path + fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite)){
                file.CopyTo(fileStream);
            }
            
            //read excel file data
            using (var workBook = new XLWorkbook(fullPath))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                
                bool firstRow = true;

                foreach (IXLRow row in workSheet.Rows())
                {
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        int i = 0;

                        //foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }
            }

            //delete null data table row
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][1] == DBNull.Value)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();

            //List<DataRow> kendaraans = dt.AsEnumerable().ToList(); //convert datatable to list
            var result = repo.SaveKendaraan(dt);

            System.IO.File.Delete(fullPath);

            if (result == 0)
            {
                return Json("Something Wrong!!!");
            }
            else
            {
                return Json("Data Kendaraan Berhasil disimpan!");
            }
        }

        public IActionResult ExportPDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"//,
                //Out = @"D:\PDFCreator\Employee_Report.pdf"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", $"{DateTime.Now:yyyyMMdd}-Data-Kendaraan.pdf");
        }
    }
}
