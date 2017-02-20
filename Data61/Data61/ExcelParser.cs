using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Data.OleDb;
using System.Data;
using System.Globalization;

namespace Data61.Excel_Parser
{
    class ExcelParser
    {

        private List<DataTable> _dataTables = new List<DataTable>();

        public List<DataTable> DataTables
        {
            get { return _dataTables; }
            set { DataTables = _dataTables; }
        }

        public ExcelParser()
        {
            string[] filesNames = LoadFiles();
            PullData(filesNames);
        }

        /// <summary>
        /// Gets all the files from the ParseThese folder
        /// </summary>
        /// <returns></returns>
        private string[] LoadFiles()
        {
            //Get the folder with the files in it
            string directoryPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\ParseThese"));

            DirectoryInfo d = new DirectoryInfo(directoryPath);
            FileInfo[] Files = d.GetFiles("*.csv"); //Getting CSV files

            string[] filesNames = new string[Files.Length];
            int i = 0;
            foreach (FileInfo file in Files)
            {
                filesNames[i] = file.Directory.ToString() + "\\" +file.Name;
                i++;
            }

            return filesNames;
        }

        /// <summary>
        /// Puts the files into a DataTable and stores it in the DataTables List
        /// </summary>
        /// <param name="Files"></param>
        private void PullData(string[] Files)
        {
            //Loop through all the files
            for (int i = 0; i < Files.Length; i++)
            {
                string pathOnly = Path.GetDirectoryName(Files[i]);
                string fileName = Path.GetFileName(Files[i]);
                //Query the data like it's an sql table
                string sql = @"SELECT * FROM [" + fileName + "]";
                //Open a connection to the csv file
                using (OleDbConnection connection = new OleDbConnection(
                          @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                          ";Extended Properties=\"Text;HDR=Yes\""))
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = CultureInfo.CurrentCulture;
                    adapter.Fill(dataTable);
                    //Store the csv file as a dataTable and save it to a list of Datatables
                    DataTables.Add(dataTable);

                }

            }
        }
        
    }
}
