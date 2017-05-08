using System.IO;
using System.Net;
using System.Net.Http;

namespace Data61_REST.IntegrationLogic
{
    public class FileHttpResponseMessage : HttpResponseMessage
    {
        private readonly string _filePath;

        public FileHttpResponseMessage(string filePath)
        {
            _filePath = filePath;
        }

        public FileHttpResponseMessage(string filePath, HttpStatusCode code) : base(code)
        {
            _filePath = filePath;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Content.Dispose();

            File.Delete(_filePath);
        }
    }
}