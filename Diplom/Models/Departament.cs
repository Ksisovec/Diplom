using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class Departament
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Worker> Worker { get; set; }
    }
}
