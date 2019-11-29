using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class LessonMarks
    {
        public int ID { get; set; }
        public bool IsVisited { get; set; }

        public int LessonEventID { get; set; }
        public virtual LessonEvent LessonEvent { get; set; }

        public int WorkerID { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
