using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data61_REST.Models
{
    public class Integration
    {
        public Dataset[] Sources { get; set; }
        public Dataset[] Targets { get; set; }
        public Mapping[] Mappings { get; set; }
    }
}