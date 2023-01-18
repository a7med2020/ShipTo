using OfficeOpenXml;
using OfficeOpenXml.Style;
using ShipTo.Application.IServices;
using ShipTo.Core.VMs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ShipTo.Application.Services
{
    public class FileManagementService : IFileManagementService
    {
        #region Fields
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion Fields

        #region Constructors
        public FileManagementService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion Constructors
        public MemoryStream CreateSimpleExcelFileStream<T>(ExcelFileInfo<T> fileInfo)
        {
            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add(fileInfo.SheetName);
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 2;
                int row = startRow;
                int column = 1;

                foreach (var colName in fileInfo.ColumnNames)
                {
                    worksheet.Cells[1, column].Value = colName;
                    column++;
                }
                worksheet.Cells[1, 1, 1, column].Style.Font.Bold = true;


                //Populate rows with data
                foreach (var rowData in fileInfo.RowDataList)
                {
                    column = 1;

                    //Populate each row with data
                    foreach (var prop in rowData.GetType().GetProperties())
                    {
                        //jump out of a loop if Data columns more than the specified columns
                        if (column > fileInfo.ColumnNames.Count())
                            break;

                        if (prop.PropertyType.Name == "DateTime")// To preventing OADate (OLE Automation Date) and getting readable format 
                            worksheet.Cells[row, column].Value = Convert.ToString(prop.GetValue(rowData, null));
                        else
                        {
                            var a = prop.GetValue(rowData, null);
                            worksheet.Cells[row, column].Value = prop.GetValue(rowData, null) ?? "null";

                        }
                        column++;
                    }
                    row++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells[1, 1, row, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                // set some core property values
                xlPackage.Workbook.Properties.Title = fileInfo.WorkbookTitle;
                xlPackage.Workbook.Properties.Author = fileInfo.WorkbookAuthor;
                xlPackage.Workbook.Properties.Subject = fileInfo.WorkbookSubject;
                // save the new spreadsheet
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            return stream;
        }

        public void CreateSimpleExcelFileAndSave<T>(ExcelFileInfo<T> fileInfo)
        {
            var stream = CreateSimpleExcelFileStream(fileInfo);
            var environmentRootPath = _webHostEnvironment.ContentRootPath;
            //Create directory if not exists
            Directory.CreateDirectory(Path.Combine(environmentRootPath, fileInfo.SaveFolderPath));
            var fileSavePath = Path.Combine(environmentRootPath, fileInfo.SaveFolderPath, fileInfo.FileName);
            stream.Position = 0;
            FileStream file = new FileStream(fileSavePath, FileMode.Create, FileAccess.Write);
            stream.WriteTo(file);
            file.Close();
            stream.Close();
        }
    }
}
