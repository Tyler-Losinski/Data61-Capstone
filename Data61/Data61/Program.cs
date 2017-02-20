using System;
using Data61.Excel_Parser;
using System.Data;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Data61
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started!");

            //Uncomment if you want to test
            //JsonExampleTest();
            //ExcelExampleTest();


            //Keep the application from stoping so we can test
            Console.ReadKey();
        }

        /// <summary>
        /// Test how to pull the data from the json file
        /// </summary>
        public static void JsonExampleTest()
        {
            //Get the json file as a string
            string stringJson = JObject.Parse(File.ReadAllText(@"..\..\ParseThese\integration.json")).ToString();
            //Create a C# object from it
            JsonSchema jsonObject = JsonConvert.DeserializeObject<JsonSchema>(stringJson);

            //Example of how get the data from the json object
            foreach (Source data in jsonObject.Datasets.Source)
            {
                Console.WriteLine(data.Data.Name);
            }

        }

        /// <summary>
        /// Test how to get the data from the excel file
        /// </summary>
        public static void ExcelExampleTest()
        {
            //Gets the excel data
            ExcelParser excelData = new ExcelParser();

            //Prints every csv files's cell with the column header it's in
            foreach (DataTable dataTable in excelData.DataTables.Values)
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
        }
    }
}
