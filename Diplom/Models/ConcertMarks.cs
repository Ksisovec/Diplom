using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class ConcertMarks
    {
        public int ID { get; set; }
        public int NumOfMarks { get; set; }

        public int ConcertEventID { get; set; }
        public virtual ConcertEvent ConcertEvent { get; set; }

        public int WorkerID { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
