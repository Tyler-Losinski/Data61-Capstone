using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Http;
using Data61_REST.IntegrationLogic;
using Data61_REST.Models;

namespace Data61_REST.Controllers
{
    public class IntegrationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Test()
        {
            return Ok("Sup nerds");
        }

        public bool ValueExists(string columnName, int rowNum, DataFile sourceDataFile)
        {
            DataTable selected = sourceDataFile.SelectColumns(columnName);
            if(rowNum  > selected.Rows.Count)
                return false;

            var temp = selected.Rows[rowNum];

            if (temp[0].ToString().Equals(""))
               return false;

            return true;
        }

        public bool SetValue(string columnName, int rowNum, DataFile dataFile, int value)
        {
            DataTable selected = dataFile.SelectColumns(columnName);
            try
            {
                dataFile.Data.Rows[rowNum][dataFile.Data.Columns[columnName].Ordinal + 1] = value;
            }catch(Exception e)
            {
                throw e;
            }
            dataFile.Save();
            return false;
        }



        [HttpPost]
        public IHttpActionResult Integrate(Integration instructions)
        {
            // EXTRACT
            string baseDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ParseThese"));
            string downloadDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Downloads"));
            List<DataFile> sourceDataFiles = new List<DataFile>();
            List<DataFile> targetDataFiles = new List<DataFile>();

            // Iterate over sources
            foreach (var s in instructions.Sources)
            {
                if (!File.Exists($@"{baseDir}\{s.Path}")) continue;
                switch (s.Type.ToLower())
                {
                    case "csv":
                        sourceDataFiles.Add(DataFileFactory.ExtractCsv(s));
                        break;
                    // TODO: Parse other filetypes
                }
            }
            
            // Iterate over targets
            foreach (var s in instructions.Targets)
            {
                if (!File.Exists($@"{baseDir}\{s.Path}")) continue;
                switch (s.Type.ToLower())
                {
                    case "csv":
                        targetDataFiles.Add(DataFileFactory.ExtractCsv(s));
                        break;
                        // TODO: Parse other filetypes
                }
            }

            // TRANSFORM
            foreach (var df in sourceDataFiles)
            {
                foreach (var tf in targetDataFiles)
                {
                    var mappingList = instructions.Mappings.Where(m => m.Source.StartsWith(df.Info.Name) && m.Target.StartsWith(tf.Info.Name)).ToList();
                    if (mappingList.Count == 0) continue;

                    string[] cols = mappingList.Select(m => m.Source.Split('.')[1]).ToArray();
                    DataTable selected = df.SelectColumns(cols);

                    ValueExists("AreaName", 3, df);
                    foreach (var m in mappingList)
                        selected.Columns[m.Source.Split('.')[1]].ColumnName = m.Target.Split('.')[1];
                    SetValue("AreaName", 5, df,90000000);

                    //TODO: Support more than just COPY
                    tf.Data.Merge(selected);
                }
            }

            // LOAD
            foreach (var tf in targetDataFiles)
                tf.Save();

            // Return the target file(s)
            if (File.Exists($@"{downloadDir}\data.zip"))
                File.Delete($@"{downloadDir}\data.zip");
            using (ZipArchive zip = ZipFile.Open($@"{downloadDir}\data.zip", ZipArchiveMode.Create))
            {
                foreach (var tf in targetDataFiles)
                    zip.CreateEntryFromFile(tf.FullPath, tf.Info.Path);
            }

            return Ok(new Download() {Zip = @"downloads\data.zip"});

            //FileHttpResponseMessage result = new FileHttpResponseMessage("data.zip", HttpStatusCode.OK);
            //var stream = new FileStream($@"{baseDir}\data.zip", FileMode.Open);
            //result.Content = new StreamContent(stream);
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            //result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            //{
            //    FileName = "data.zip"
            //};
            //return result;
        }
    }
}
