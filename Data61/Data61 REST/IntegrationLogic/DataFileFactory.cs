using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using Data61_REST.Models;

namespace Data61_REST.IntegrationLogic
{
    public static class DataFileFactory
    {
        private static readonly string BaseDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ParseThese"));

        public static DataFile ExtractCsv(Dataset info)
        {
            //Query the data like it's an sql table
            string sql = $@"SELECT * FROM [{info.Path}]";
            //Open a connection to the csv file
            using (var connection = new OleDbConnection($@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={BaseDirectory};Extended Properties=""Text;HDR=Yes"""))
            using (var command = new OleDbCommand(sql, connection))
            using (var adapter = new OleDbDataAdapter(command))
            {
                var dataTable = new DataTable {Locale = CultureInfo.CurrentCulture};
                adapter.Fill(dataTable);
                //Store the csv file as a dataTable and save it to a dictionary of Datatables
                return new DataFile(info, dataTable);
            }
        }

        //TODO: Parse other datatypes

    }
}