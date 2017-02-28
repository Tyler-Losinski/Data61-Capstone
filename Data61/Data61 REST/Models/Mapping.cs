using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data61_REST.Models
{
    public class Mapping
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string Operation { get; set; } // TODO: This will become more complex in the future
    }
}