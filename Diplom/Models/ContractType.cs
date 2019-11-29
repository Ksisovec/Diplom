using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class ContractType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contract> Contract { get; set; }
    }
}
