using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.VMs
{
    public class ExcelFileInfo<T>
    {
        public string SheetName { get; set; }
        public string[] ColumnNames { get; set; }
        public List<T> RowDataList { get; set; }
        public string WorkbookTitle { get; set; }
        public string WorkbookAuthor { get; set; }
        public string WorkbookSubject { get; set; }
        public string FileName { get; set; }
        public string SaveFolderPath { get; set; }

    }
}
