using System.Data;
using OfficeOpenXml;
namespace NguyenThiKhambth2.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string strPath)
        {
            FileInfo fi = new FileInfo(strPath);
            ExcelPackage excelPackage = new ExcelPackage(fi);
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
            if (worksheet.Dimension == null)
            {
                return dt;
            }
            List<string> columnNames = new List<string>();
            int currentColumn = 1;
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();
                if(cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_"+ currentColumn);
                    dt.Columns.Add("Header_"+ currentColumn);
                    currentColumn++;
                }
                columnNames.Add( columnName);
                int occurrences = columnNames.Count(x => x.Equals(columnNames));
                if (occurrences>1)
                {
                    columnName =columnName +"_" + occurrences;
                }
                // add the column to the database
                dt.Columns.Add(columnName);
                currentColumn++;
            }
            for (int i= 2; i<= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                foreach (var cell in row)
                {
                    newRow[ cell.Start.Column -1]= cell.Text;
                }
                dt.Rows.Add(newRow);
            }
            return dt;

            }
        }
    }
