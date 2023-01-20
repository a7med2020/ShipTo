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
                            worksheet.Cells[row, column].Value = prop.GetValue(rowData, null) ;

                        }
                        column++;
                    }
                    row++;
                }
                //Set the worksheet right-to-left
                worksheet.View.RightToLeft = true;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells[1, 1, row, column].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[1, 1, row, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, row, column].Style.WrapText = true;

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

        public MemoryStream CreateWellStyled1ExcelFileStream<T>(ExcelFileInfo<T> fileInfo, bool IsHaveFooter = false)
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
                int column = 0;

                foreach (var colName in fileInfo.ColumnNames)
                {
                    column++;
                    worksheet.Cells[1, column].Value = colName;
                }
                var header = worksheet.Cells[1, 1, 1, column];
                header.Style.Font.Bold = true;

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
                            worksheet.Cells[row, column].Value = prop.GetValue(rowData, null);

                        }
                        column++;
                    }
                    row++;
                }
                //Set the worksheet right-to-left
                worksheet.View.RightToLeft = true;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                var content = worksheet.Cells[1, 1, row-1, column-1]; // -1 because there is unnecessary ++ at the end of the loop
                content.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                content.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                content.Style.WrapText = true;

                content.Style.Fill.PatternType = ExcelFillStyle.Solid;
                content.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Bisque);

                content.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                content.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                content.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                content.Style.Border.Right.Style = ExcelBorderStyle.Thick;

                content.Style.Border.Top.Color.SetColor(System.Drawing.Color.White);
                content.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);
                content.Style.Border.Left.Color.SetColor(System.Drawing.Color.White);
                content.Style.Border.Right.Color.SetColor(System.Drawing.Color.White);

                header.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                header.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                header.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                header.Style.Border.Right.Style = ExcelBorderStyle.Thick;

                header.Style.Border.Top.Color.SetColor(System.Drawing.Color.White);
                header.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);
                header.Style.Border.Left.Color.SetColor(System.Drawing.Color.White);
                header.Style.Border.Right.Color.SetColor(System.Drawing.Color.White);

                header.Style.Fill.PatternType = ExcelFillStyle.Solid;
                header.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BurlyWood);

                var footer = worksheet.Cells[row-1, 1, row-1, column-1];
                footer.Style.Font.Bold = true;

                footer.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                footer.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                footer.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                footer.Style.Border.Right.Style = ExcelBorderStyle.Thick;

                footer.Style.Border.Top.Color.SetColor(System.Drawing.Color.White);
                footer.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.White);
                footer.Style.Border.Left.Color.SetColor(System.Drawing.Color.White);
                footer.Style.Border.Right.Color.SetColor(System.Drawing.Color.White);

                footer.Style.Fill.PatternType = ExcelFillStyle.Solid;
                footer.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BurlyWood);

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
        public void CreateWellStyled1ExcelFileAndSave<T>(ExcelFileInfo<T> fileInfo, bool IsHaveFooter = false)
        {
            var stream = CreateWellStyled1ExcelFileStream(fileInfo, IsHaveFooter);
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
