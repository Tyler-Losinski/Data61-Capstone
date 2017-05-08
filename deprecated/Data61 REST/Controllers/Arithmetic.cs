using Data61_REST.IntegrationLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data61_REST.Controllers
{
    public class Arithmetic
    {

        public DataFile DataFile { get; set; }

        public Arithmetic(DataFile dataFile)
        {
            DataFile = dataFile;
        }

        /// <summary>
        /// Checks if a value exists
        /// </summary>
        /// <param name="columnName">Name of the column in question</param>
        /// <param name="rowNum">Row number for the specific cell we want</param>
        /// <param name="sourceDataFile">DataTable to look at</param>
        /// <returns></returns>
        public bool ValueExists(string columnName, int rowNum)
        {
            DataTable selected = DataFile.SelectColumns(columnName);
            if (rowNum > selected.Rows.Count)//make sure they are not trying to select a row outside of the datatable
                return false;

            var temp = selected.Rows[rowNum];

            if (temp[0].ToString().Equals(""))
                return false;

            return true;
        }

        /// <summary>
        /// Sets a specific cell's value
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowNum"></param>
        /// <param name="dataFile"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string columnName, int rowNum, int value)
        {
            DataTable selected = DataFile.SelectColumns(columnName);

            if (rowNum > selected.Rows.Count)
                return false;

            //Sets the specified cell to a new value
            DataFile.Data.Rows[rowNum][DataFile.Data.Columns[columnName].Ordinal] = value;
            DataFile.Save();

            return true;
        }

        /// <summary>
        /// Deletes a value from the specified cell
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowNum"></param>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        public bool DeleteValue(string columnName, int rowNum)
        {
            DataTable selected = DataFile.SelectColumns(columnName);

            if (rowNum > selected.Rows.Count)
                return false;

            //This cell can only be set to the datatype that this datatable
            DataFile.Data.Rows[rowNum][DataFile.Data.Columns[columnName].Ordinal] = 0;
            DataFile.Save();

            return true;
        }

        public decimal BasicArithmetic(string columnName, int rowNum, int value, string operation)
        {
            DataTable selected = DataFile.SelectColumns(columnName);

            if (rowNum > selected.Rows.Count)
                return 0;

            decimal currentValue = Convert.ToDecimal(DataFile.Data.Rows[rowNum][DataFile.Data.Columns[columnName].Ordinal]);
            try
            {
                switch (operation)
                {
                    case "ADD":
                        DataFile.Data.Rows[rowNum][DataFile.Data.Columns[columnName].Ordinal] = value + currentValue;
                        break;
                    case "SUBTRACT":
                        DataFile.Data.Rows[rowNum][DataFile.Data.Columns[columnName].Ordinal] = currentValue - value;
                        break;
                    case "MULTIPLY":
                        DataFile.Data.Rows[rowNum][DataFile.Data.Columns[columnName].Ordinal] = value * currentValue;
                        break;
                    case "DIVIDE":
                        DataFile.Data.Rows[rowNum][DataFile.Data.Columns[columnName].Ordinal] = currentValue / value;
                        break;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            DataFile.Save();

            return Convert.ToDecimal(DataFile.Data.Rows[rowNum][DataFile.Data.Columns[columnName].Ordinal]);
        }

        public decimal Average(string columnName)
        {
            DataTable selected = DataFile.SelectColumns(columnName);

            return SumColumn(columnName)/ (selected.Rows.Count + 1);
        }

        public decimal SumColumn(string columnName)
        {
            decimal total = 0;
            DataTable selected = DataFile.SelectColumns(columnName);

            foreach (DataRow row in selected.Rows)
            {
                total += Convert.ToDecimal(row[0]);
            }

            return total;
        }
    }
}