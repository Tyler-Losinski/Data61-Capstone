using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Data61
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Started!");

            //Read in JSON file
            string stringJson = JObject.Parse(File.ReadAllText(@"..\..\ParseThese\integration.json")).ToString();
            //string stringJson = JObject.Parse(File.ReadAllText(@"..\..\ParseThese\integration2.json")).ToString(); // Try using this other integration file!
            JsonSchema transform = JsonConvert.DeserializeObject<JsonSchema>(stringJson);

            //Create DataFile objects for source(s) and target
            List<DataFile> sourceDataFiles = transform.Datasets.Source.Select(s => new DataFile(transform.Datasets.Source[0].Data.Name, transform.Datasets.Source[0].Data.Path)).ToList();
            DataFile targetDataFile = new DataFile(transform.Datasets.Target[0].Data.Name, transform.Datasets.Target[0].Data.Path);

            // Iterate over the source files
            foreach (DataFile df in sourceDataFiles)
            {
                // Store all schema transformations in an ordered Dictionary
                var schemaTranformations = new OrderedDictionary();
                foreach (var m in transform.Mappings)
                    if (m.Source.StartsWith(df.Name))
                        schemaTranformations.Add(m.Target.Substring(targetDataFile.Name.Length + 1), m.Source.Substring(df.Name.Length + 1));

                // Convert the keys and values to arrays so we can work with them
                var keys = new string[schemaTranformations.Count];
                schemaTranformations.Keys.CopyTo(keys, 0);
                var values = new string[schemaTranformations.Count];
                schemaTranformations.Values.CopyTo(values, 0);

                // Set the select query to only pick up the columns we care about
                string sql = $"SELECT {string.Join(",", values)} FROM [{df.Path}]";
                df.SetQuery(sql);

                // Read out a single row from the source dataset at a time, transform the data, and push it to the target
                while (df.Next())
                {
                    var queryValues = new string[schemaTranformations.Count];
                    for (int i = 0; i < schemaTranformations.Count; i++)
                        queryValues[i] = df.GetString(i).Replace("'", "''");

                    string sql2 = $"INSERT INTO [{targetDataFile.Path}] ({string.Join(",", keys)}) VALUES ('{string.Join("','", queryValues)}')";
                    targetDataFile.Push(sql2);
                }
            }

            Console.WriteLine("Done!");

            //Keep the application from stoping so we can test
            Console.ReadKey();
        }
    }
}
