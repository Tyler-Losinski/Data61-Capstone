using System;
using System.Data.OleDb;

namespace Data61
{
    /// <summary>
    /// Represents a connection to a CSV file
    /// </summary>
    class DataFile
    {
        readonly string _baseDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\ParseThese"));
        private readonly OleDbConnection _odbConnection;
        private OleDbCommand _odbCommand;
        private OleDbDataReader _odbReader;

        public string Name { get; }
        public string Path { get; }

        public DataFile(string name, string path)
        {
            Name = name;
            Path = path;
            try
            {
                _odbConnection =
                    new OleDbConnection(
                        $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={_baseDirectory};Extended properties=""Text;HDR=Yes""");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public void SetQuery(string sql)
        {
            _odbConnection.Open();
            _odbCommand = new OleDbCommand(sql, _odbConnection);
            _odbReader = _odbCommand.ExecuteReader();
        }

        public bool Next()
        {
            if (_odbReader.Read()) return true;
            _odbReader.Close();
            _odbConnection.Close();
            return false;
        }

        public string GetString(int ordinal)
        {
            return _odbReader.GetValue(ordinal).ToString();
        }

        public int Push(string sql)
        {
            _odbConnection.Open();
            OleDbCommand insertCommand = new OleDbCommand(sql, _odbConnection);
            int result = insertCommand.ExecuteNonQuery();
            _odbConnection.Close();
            return result;
        }

    }
}
