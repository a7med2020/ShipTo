using ShipTo.Core.VMs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.IServices
{
    public interface IFileManagementService
    {
        MemoryStream CreateSimpleExcelFileStream<T>(ExcelFileInfo<T> fileInfo);
        void CreateSimpleExcelFileAndSave<T>(ExcelFileInfo<T> fileInfo);
    }
}
