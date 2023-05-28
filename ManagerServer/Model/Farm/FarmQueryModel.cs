using ManagerServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Farm
{
    public class FarmQueryModel: PagingQueryModel
    {
        public int? Id { get; set; }
        public string? OwnerID { get; set; }
        public string? FarmName { get; set;}
        public string? FarmDescription { get; set; }
        public string? Avata { get; set; }
        public string? Adress { get; set; }
    }
}
