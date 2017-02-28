using System;
using System.Data;
using System.IO;
using System.Text;
using Data61_REST.Models;

namespace Data61_REST.IntegrationLogic
{
    /// <summary>
    /// Represents a table of data from a dataset
    /// </summary>
    public class DataFile
    {
        readonly string _baseDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\ParseThese"));

        public Dataset Info { get; }
        public DataTable Data { get; set; }
        public string FullPath => $@"{_baseDirectory}\{Info.Path}";

        public DataFile(Dataset info, DataTable data)
        {
            Info = info;
            Data = data;
        }

        public DataTable SelectColumns(params string[] cols)
        {
            DataView view = new DataView(Data);
            return view.ToTable("Selected", false, cols);
        }

        public void Save()
        {
            using (var sw = new StreamWriter(FullPath, true))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Data.Columns.Count; i++)
                {
                    sb.Append(Data.Columns[i]);
                    if (i < Data.Columns.Count - 1)
                        sb.Append(",");
                }
                sw.WriteLine(sb.ToString());
                sb.Clear();
                foreach (DataRow dr in Data.Rows)
                {
                    for (int i = 0; i < Data.Columns.Count; i++)
                    {
                        sb.Append(dr[i]);

                        if (i < Data.Columns.Count - 1)
                            sb.Append(",");
                    }
                    sw.WriteLine(sb.ToString());
                    sb.Clear();
                }
            }
        }

    }
}
