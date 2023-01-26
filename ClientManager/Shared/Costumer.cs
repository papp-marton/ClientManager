using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Shared
{
    public class Costumer
    {
        public int Id { get; set; }
        public int IdNum { get; set; }
        public bool IsCompany { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }

    }
}
