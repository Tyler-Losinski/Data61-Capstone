using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data61.Excel_Parser;
using System.Data;

namespace Data61
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Started!");
            ExcelParser excelData = new ExcelParser();

            //Prints every csv files's cell with the column header it's in
            foreach (DataTable dataTable in excelData.DataTables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Console.Write("Item: ");
                        Console.Write(column.ColumnName);
                        Console.Write(" ");
                        Console.WriteLine(row[column]);
                    }
                }
            }

            //Keep the application from stoping so we can test
            Console.ReadKey();
        }
    }
}
