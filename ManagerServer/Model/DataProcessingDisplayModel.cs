using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class DataProcessingDisplayModel
    {
        public string? Topic { get; set; }
        public string? Payload { get; set; }
        public DateTime? RetrieveAt { get; set; }
        public string? Mode { get; set; }
    }
}
